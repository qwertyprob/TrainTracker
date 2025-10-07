using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class TrainSimulationBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory; // Нужен для Scoped DI Сервисов

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
                var incidentService = scope.ServiceProvider.GetRequiredService<IIncidentService>();
                
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
                    }


                    if (train.DelayTime == 10 && knownTrainIds.Contains(train.Id))
                    {
                        var existingIncidents = await incidentService.GetAllIncidentsAsync(train.Id);
                        var alreadyExists = existingIncidents.Data?.Any(i => i.Reason == "SomeReason") ?? false;

                        if (!alreadyExists)
                        {
                            //Добавление данных для просмотра инцидентов на ui
                            await incidentService.AddIncidentAsync(new IncidentDto
                            {
                                Username = $"Test{train.Number}",
                                Reason = "SomeReason",
                                Comment = "SomeComment",
                            }, train.Id);
                        }
                    }

                    

                    // await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
                
                    
                
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        }
    } 
}