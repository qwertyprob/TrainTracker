using System.Globalization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DAL.Entities;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services.BackgroundServices;

public class TrainSimulationBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory; // Нужен для Scoped DI Сервисов

    private bool IsIncAdded = false;

    public TrainSimulationBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        await this.RunAsync(stoppingToken);
            
    }

    public async Task RunAsync(CancellationToken stoppingToken)
    {
        var knownTrainIds = new HashSet<long>();

        using (var scope = _scopeFactory.CreateScope())
        {
               
            var trainService = scope.ServiceProvider.GetRequiredService<ITrainService>();
                
            await trainService.ClearAllTrainsAsync();
        }

        while (!stoppingToken.IsCancellationRequested)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                //Scoped сервисы добавлять лучше именно так
                var trainService = scope.ServiceProvider.GetRequiredService<ITrainService>();
                var incService = scope.ServiceProvider.GetRequiredService<IIncidentService>();

                var trainJsonService = scope.ServiceProvider.GetRequiredService<TrainJsonParser>();
                var trains = await trainJsonService.ParseAsync();

                trains = trains.OrderBy(t => t.DelayTime)
                    .ToList(); // сортировка нужна и здесь потому что в dto есть маппинг времени для симуляции данных
                // Удаление данных в БД, что бы при запуске они появлялись снова
                
                
                foreach (var train in trains)
                {
                    if (!knownTrainIds.Contains(train.Id))
                    {
                        knownTrainIds.Add(train.Id);
                        await trainService.AddTrainAsync(train);
                        
                        if (knownTrainIds.Count == 17)
                        {

                            await AddIncidents(incService);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Refreshed!");
                            Console.ForegroundColor = ConsoleColor.White;
                            break; 
                        }
                    }
                    

                    
                    

                }
                
                    
                
            }
            

        }
    }


    private async Task AddIncidents(IIncidentService incService)
    {
        var incidents = new List<IncidentDto>()
        {
            new IncidentDto()
            {
                Reason = "Delay",
                Username = "Erik from left station",
                Comment = "Delayed because of storm",
                TrainId = 688387
            },
            new IncidentDto()
            {
                Reason = "Break",
                Username = "Lucy",
                Comment = "Some problems with engine",
                TrainId = 688351
            }
        };

        foreach (var item in incidents)
        {
            await incService.AddIncidentAsync(item, item.TrainId);
        }
    }
}