using TrainTracker.DAL.Entities;
using TrainTracker.DTO;

namespace TrainTracker.DAL.Interfaces;

public interface ITrainRepository
{
     Task<TrainEntity?> GetByIdAsync(long id);
     Task<IEnumerable<TrainEntity>> GetAllAsync();
     Task AddAsync(TrainDto train);
     
     //Для удаления симуляционных данных
     Task ClearAllAsync();
     
     //Деактивация поездов из бд(что б не удалять на совсем)
     Task DeactivateByIdAsync(long id);
     
     //Метод для смены задержки времени поезда
     Task ChangeDelayTimeAsync(long id, int delayTime);


}