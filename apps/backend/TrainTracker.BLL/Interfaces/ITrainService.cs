using TrainTracker.DTO;

namespace TrainTracker.BLL.Interfaces;

public interface ITrainService
{
     Task<BaseResponseModel<IEnumerable<TrainDto>>> GetAllTrainsAsync();
     Task<BaseResponseModel<TrainDto>> GetTrainByIdAsync(long id);
     Task<BaseResponseModel<TrainDto>> AddTrainAsync(TrainDto train);
     
     //Удалить все данные
     Task ClearAllTrainsAsync();
     
     //Деактивировать таблицы(вместо удаления)
     Task<BaseResponseModel<TrainDto>> DeactivateTrainByIdAsync(long id);
     
     //Изменить время задержки поезда
     Task<bool> ChangeDelayTimeAsync(long id, int delayTime);

}