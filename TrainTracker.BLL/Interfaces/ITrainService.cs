using TrainTracker.DTO;

namespace TrainTracker.BLL.Interfaces;

public interface ITrainService
{
     Task<BaseResponseModel<IEnumerable<TrainDto>>> GetAllTrainsAsync();
     Task<BaseResponseModel<TrainDto>> GetTrainByIdAsync(int id);
     Task AddTrainAsync(TrainDto train);
     Task ClearAllTrainsAsync();
}