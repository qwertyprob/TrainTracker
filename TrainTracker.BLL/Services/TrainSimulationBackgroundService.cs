using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using TrainTracker.BLL.Interfaces;

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

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var trainJsonService = scope.ServiceProvider.GetRequiredService<TrainJsonParser>();
                var trainService = scope.ServiceProvider.GetRequiredService<ITrainService>();

                var trains = await trainJsonService.ParseAsync();

                trains = trains.OrderBy(t => t.DelayTime)
                    .ToList(); // сортировка нужна и здесь потому что в dto есть маппинг времени для симуляции данных

                // Удаление данных в БД, что бы при запуске они появлялись снова
                await trainService.ClearAllTrainsAsync();

                foreach (var train in trains)
                {
                    //Закончили симуляцию
                    if (knownTrainIds.Contains(train.Id))
                    {
                        Console.WriteLine("End service!");
                        return; 
                    }

                    
                    knownTrainIds.Add(train.Id);

                    await trainService.AddTrainAsync(train);

                    // Задержка между выводом каждого поезда
                    await Task.Delay(3000, stoppingToken);
                }
            }
            
        }
    } 
}