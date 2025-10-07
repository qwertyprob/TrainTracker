using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services.BackgroundServices;

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
                    else
                    {
                        return;
                    }
                    

                }
                
                    
                
            }
            

        }
    } 
}