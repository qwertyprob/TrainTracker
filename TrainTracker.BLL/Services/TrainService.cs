using Newtonsoft.Json;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class TrainService :ITrainService
{
    private readonly ITrainRepository _trainRepository;
    private JsonRequestModel _request;
    private IEnumerable<TrainDto> _trains = new List<TrainDto>();

    public TrainService(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;

    }
    public async Task<BaseResponseModel<IEnumerable<TrainDto>>> GetAllTrainsAsync()
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "App_Data", "task.json");
        
        if (File.Exists(filePath))
        {
            var json = await File.ReadAllTextAsync(filePath);
    
            _request = JsonConvert.DeserializeObject<JsonRequestModel>(json) ?? new JsonRequestModel();
            
            _trains = _request?.Data.Select(d =>
            {
                var train = d.Train;
                //Костыль для отображения имени 
                train.Name = d.Name;
                return train;
                
            }).ToList() ?? new List<TrainDto>();
        }
        else
        {
            _trains = new List<TrainDto>();
        }
        
        if (_request?.Data?.Count() == 0 || _trains?.Count() == 0)
        {
            return new BaseResponseModel<IEnumerable<TrainDto>>()
            {
                StatusCode = 404,
                Message = "Trains are not found!",
                Data =_trains
            };
        }

        var response = new BaseResponseModel<IEnumerable<TrainDto>>
        {
            StatusCode = 200,
            Message = "Trains fetched successfully",
            Data = _trains
        };

        return response;
        
    }



    
}