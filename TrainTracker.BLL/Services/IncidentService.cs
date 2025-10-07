using TrainTracker.BLL.Interfaces;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class IncidentService :IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ITrainService _trainService;
    public IncidentService(IIncidentRepository incidentRepository,ITrainService trainService)
    {
        _incidentRepository = incidentRepository;
        _trainService = trainService;

    }
    public async Task<BaseResponseModel<List<IncidentDto>>> GetAllIncidentsAsync(long trainId)
    {
        try
        {
            var entities = await _incidentRepository.GetAllByTrainAsync(trainId);

            if (entities == null || !entities.Any())
            {
                return new BaseResponseModel<List<IncidentDto>>
                {
                    StatusCode = 404, //это не ошибка, все под контролем
                    Message = "No incidents found!",
                    Data = new List<IncidentDto>()
                };
            }

            //Mapping 
            var dtos = entities!.Select(e => new IncidentDto
            {
                Username = e.Username,
                Reason = e.Reason,
                Comment = e.Comment,
                CreatedAt = e.CreatedAt
                
            }).OrderBy(x=>x.CreatedAt)
                .ToList();

            return new BaseResponseModel<List<IncidentDto>>
            {
                StatusCode = 200,
                Message = "Incidents fetched successfully!",
                Data = dtos
            };
        }
        catch (Exception ex)
        {
            return new BaseResponseModel<List<IncidentDto>>
            {
                StatusCode = 500,
                Message = $"Incidents error: {ex.Message}",
                Data = null
            };
        }
    }
    public async Task<BaseResponseModel<IncidentDto>> AddIncidentAsync(IncidentDto incidentDto, long trainId)
    {
        try
        {
            var train = await _trainService.GetTrainByIdAsync(trainId);

            if (train.Data == null)
                return new BaseResponseModel<IncidentDto>
                {
                    StatusCode = 404,
                    Message = "Train not found",
                    Data = null
                };
            

            //Добавление 
            await _incidentRepository.AddAsync(incidentDto, trainId);
            
            //Увеличиваем время после добавления инцидента на 5 минут
            await _trainService.ChangeDelayTimeAsync(trainId,train.Data.DelayTime + 5);

            return new BaseResponseModel<IncidentDto>
            {
                StatusCode = 200,
                Message = "Incident was added successfully!",
                Data = incidentDto
            };
        }
        
        catch (Exception ex)
        {
            return new BaseResponseModel<IncidentDto>
            {
                StatusCode = 500,
                Message = "Server incident error: " + ex.Message,
                Data = null
            };
        }
    }

}