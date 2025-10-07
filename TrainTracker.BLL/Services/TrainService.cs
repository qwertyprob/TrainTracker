
using TrainTracker.BLL.Interfaces;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class TrainService :ITrainService
{
    private readonly ITrainRepository _trainRepository;
    private static bool _isInitialized;
    public TrainService(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;

    }
    public async Task<BaseResponseModel<IEnumerable<TrainDto>>> GetAllTrainsAsync()
    {
        try
        {
            var trainEntities = await _trainRepository.GetAllAsync();

            if (trainEntities == null ||trainEntities!.Count() != 0)
            {
                return new BaseResponseModel<IEnumerable<TrainDto>>()
                {
                    StatusCode = 400,
                    Message = "List of trains is empty!",
                    Data = new List<TrainDto>()
                };
            }

            //Mapping TrainEntity to TrainDto
            var mappedTrains = trainEntities
                .Select(t => new TrainDto
                {
                    RawId = t.Id.ToString(), // Id нет сеттера
                    Name = t.Name,
                    RawNumber = t.Number.ToString(), // Number нет сеттера
                    DelayTime = t.DelayTime,
                    CreatedAt = t.CreatedAt,
                    LastDelayUpdateAt = t.LastDelayUpdateAt,
                    NextStation = new StationDto
                    {
                        RawId = t.NextStation!.Id.ToString(),
                        Title = t.NextStation.StationTitle
                    },
                    Incidents = t.Incidents?
                        .Select(i => new IncidentDto
                        {
                            Id = i.Id,
                            Reason = i.Reason,
                            Comment = i.Comment
                        })
                        .ToList()

                }).OrderBy(x => x.DelayTime);

            return new BaseResponseModel<IEnumerable<TrainDto>>()
            {
                StatusCode = 200,
                Message = "Trains fetched successfully",
                Data = mappedTrains
            };
        }
        catch (Exception e)
        {
            return new BaseResponseModel<IEnumerable<TrainDto>>()
            {
                StatusCode = 500,
                Message = $"Unexpected error:{e.Message}",
                Data = new List<TrainDto>()
            };
        }
        
        
    }
    public async Task<BaseResponseModel<TrainDto>> GetTrainByIdAsync(long id)
    {
        var trainEntity = await _trainRepository.GetByIdAsync(id);

        if (trainEntity == null)
        {
            return new BaseResponseModel<TrainDto>()
            {
                StatusCode = 404,
                Message = "Train doesn't exist!",
                Data = new TrainDto()
            };
        }
        
        //Mapping 

        var trainDto = new TrainDto()
        {
            RawId = trainEntity.Id.ToString(),
            Name = trainEntity.Name,
            RawNumber = trainEntity.Number.ToString(),
            DelayTime = trainEntity.DelayTime,
            LastDelayUpdateAt = trainEntity.LastDelayUpdateAt,
            NextStation = new StationDto()
            {
                RawId = trainEntity.NextStation!.Id.ToString(),
                Title = trainEntity.NextStation.StationTitle
            },
            Incidents = trainEntity.Incidents?
            .Select(i => new IncidentDto
            {
                Id = i.Id,
                Reason = i.Reason,
                Comment = i.Comment
            })
            .OrderBy(x=>x.CreatedAt)
            .ToList()
        };

        return new BaseResponseModel<TrainDto>()
        {
            StatusCode = 200,
            Message = "Train found successfully!",
            Data = trainDto
        };
    }
    public async Task<BaseResponseModel<TrainDto>> AddTrainAsync(TrainDto train)
    {
        try
        {
            if (train == null)
            {
                return new BaseResponseModel<TrainDto>
                {
                    StatusCode = 400,
                    Message = "Train data is null!",
                    Data = null
                };
            }
    
            await _trainRepository.AddAsync(train);
    
            return new BaseResponseModel<TrainDto>
            {
                StatusCode = 200,
                Message = "Train added successfully!",
                Data = train
            };
        }
        catch (Exception ex)
        {
            return new BaseResponseModel<TrainDto>
            {
                StatusCode = 500,
                Message = $"Error adding train: {ex.Message}",
                Data = null
            };
        }
    }
    
    //Меняем время задержки
    public async Task ChangeDelayTimeAsync(long id, int delayTime)
    {
        var train = await this.GetTrainByIdAsync(id);

        if (train.Data == null)
        {
            return;
        }

        await _trainRepository.ChangeDelayTimeAsync(id, delayTime);

    }
    
    //Деактивируем данные, а не удаляем
    public async Task<BaseResponseModel<TrainDto>> DeactivateTrainByIdAsync(long id)
    {
        try
        {
            var trainById = await _trainRepository.GetByIdAsync(id);

            if (trainById == null)
            {
                return new BaseResponseModel<TrainDto>()
                {
                    StatusCode = 400,
                    Message = "Train doesn't exist!",
                    Data = null
                };
            }

            await _trainRepository.DeactivateByIdAsync(id);
            return new BaseResponseModel<TrainDto>()
            {
                StatusCode = 200,
                Message = "Train successfully deleted!",
                Data = null
            };
        }
        catch (Exception e)
        {
            return new BaseResponseModel<TrainDto>()
            {
                StatusCode = 500,
                Message = $"Unexpected error:{e.Message}",
                Data = null
            };
        }
    }
    
    //Удаление данных, так как это данные из json
    public async Task ClearAllTrainsAsync()
    {
        //Удаляем все данные вначале 
        if (!_isInitialized)
        {
            await _trainRepository.ClearAllAsync();
            _isInitialized = true;
        }
    }


    
}