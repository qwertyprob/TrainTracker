using TrainTracker.DTO;

namespace TrainTracker.BLL.Interfaces;

public interface ITrainService
{
     Task<BaseResponseModel<IEnumerable<TrainDto>>> GetAllTrainsAsync();
}