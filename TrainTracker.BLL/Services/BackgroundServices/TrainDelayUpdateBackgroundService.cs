using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services.BackgroundServices;

public class TrainDelayUpdateBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private static bool _initialized;

    public TrainDelayUpdateBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //Scope DI сервисы
            using var scope = _scopeFactory.CreateScope();
            var trainService = scope.ServiceProvider.GetRequiredService<ITrainService>();
    
            //Для первого поезда, который прибыл задержка 15 секунд, что бы мы на него успели
            if (!_initialized)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                _initialized = true;
                
            }
            
            //Берем данные из бд
            var trains = await trainService.GetAllTrainsAsync();
            
            //Если в trains что то есть, значит с ним можно работать
            if (trains!.Data.Any() == true)
            {
                foreach (var train in trains.Data)
                {
                    var lastUpdate = train.LastDelayUpdateAt ?? train.CreatedAt;
                    var secondsPassed = (DateTime.UtcNow - lastUpdate).TotalSeconds;

                    //Если задержка больше 0 и минута прошла
                    if (train.DelayTime > 0 && secondsPassed >= 60)
                    {
                        await trainService.ChangeDelayTimeAsync(train.Id, train.DelayTime - 1);
                    }
                    //Если задержка равна нулю, значит поезд прибыл(удаляем его)
                    if (train.DelayTime == 0)
                    {
                        await trainService.DeactivateTrainByIdAsync(train.Id);
                    }
                }
            }
            
            //Ждем 15 секунд и заново проходимся 
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);


        }
    }

}