
using TrainTracker.BLL.Interfaces;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class TrainService :ITrainService
{
    private readonly ITrainRepository _trainRepository;
    
    private static bool isInitialized = false;

    public TrainService(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;

    }

    public async Task<BaseResponseModel<IEnumerable<TrainDto>>> GetAllTrainsAsync()
    {
        var trainEntities = await _trainRepository.GetAllAsync();
        
        
        if (!trainEntities.Any())
        {
            return new BaseResponseModel<IEnumerable<TrainDto>>()
            {
                StatusCode = 404,
                Message = "Trains are not found!",
                Data = null
            };
        }
        
        //Mapping TrainEntity to TrainDto
        var mappedTrains = trainEntities
            .Select(t => new TrainDto
            {
                RawId = t.Id.ToString(), // Id нет сеттера
                Name = t.Name,
                RawNumber = t.Number.ToString(), // Number нет сеттера
                RawDelayTime = t.DelayTime,
                NextStation = t.NextStation is not null 
                    ? new StationDto
                    {
                        RawId = t.NextStation.Id.ToString(),
                        Title = t.NextStation.StationTitle
                    }
                    : null,
                Incidents = t.Incidents?
                    .Select(i => new IncidentDto
                    {
                        Id = i.Id,
                        Reason = i.Reason,
                        Comment = i.Comment
                    })
                    .ToList()
                
            }).OrderBy(x=>x.DelayTime);
        
        

        return new BaseResponseModel<IEnumerable<TrainDto>>()
        {
            StatusCode = 200,
            Message = "Trains fetched successfully",
            Data = mappedTrains
        };
        
    }

    public async Task AddTrainAsync(TrainDto train)
    {

        await _trainRepository.AddAsync(train);
    }


    public async Task ClearAllTrainsAsync()
    {
        //Удаляем все данные вначале 
        if (!isInitialized)
        {
            await _trainRepository.ClearAllAsync();
            isInitialized = true;
        }
    }


    
}