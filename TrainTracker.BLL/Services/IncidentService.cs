using TrainTracker.BLL.Interfaces;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class IncidentService :IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ITrainRepository _trainRepository;


    public IncidentService(IIncidentRepository incidentRepository, ITrainRepository trainRepository)
    {
        _incidentRepository = incidentRepository;
        _trainRepository = trainRepository;

    }
    public async Task<BaseResponseModel<List<IncidentDto>>> GetAllIncidentsAsync()
    {
        var entities = await _incidentRepository.GetAllAsync();

        if (entities == null || !entities.Any())
        {
            return new BaseResponseModel<List<IncidentDto>>
            {
                StatusCode = 404,
                Message = "No incidents found",
                Data = new List<IncidentDto>()
            };
        }

        var dtos = entities.Select(e => new IncidentDto
        {
            Username = e.Username,
            Reason = e.Reason,
            Comment = e.Comment
        }).ToList();

        return new BaseResponseModel<List<IncidentDto>>
        {
            StatusCode = 200,
            Message = "Incidents fetched successfully",
            Data = dtos
        };
    }


    public async Task<BaseResponseModel<IncidentDto>> AddIncidentAsync(IncidentDto incidentDto, long trainId)
    {
        try
        {
            var train = await _trainRepository.GetByIdAsync(trainId);

            if (train == null)
                return new BaseResponseModel<IncidentDto>
                {
                    StatusCode = 404,
                    Message = "Train not found",
                    Data = null
                };

            if (train.Incidents.Any(i => i.Username == incidentDto.Username))
                return new BaseResponseModel<IncidentDto>
                {
                    StatusCode = 400,
                    Message = $"User '{incidentDto.Username}' has already reported an incident for this train",
                    Data = null
                };

            var incident = await _incidentRepository.AddAsync(incidentDto, trainId);

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
                Message = "Unexpected error: " + ex.Message,
                Data = null
            };
        }
    }

}