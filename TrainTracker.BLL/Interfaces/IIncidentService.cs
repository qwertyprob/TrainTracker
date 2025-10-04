using TrainTracker.DTO;

namespace TrainTracker.BLL.Interfaces;

public interface IIncidentService
{
    Task<BaseResponseModel<List<IncidentDto>>> GetAllIncidentsAsync(long trainId);
    Task<BaseResponseModel<IncidentDto>> AddIncidentAsync(IncidentDto incidentDto, long trainId);
    
}