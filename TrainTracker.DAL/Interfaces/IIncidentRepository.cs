using TrainTracker.DAL.Entities;
using TrainTracker.DTO;

namespace TrainTracker.DAL.Interfaces;

public interface IIncidentRepository
{
    Task<IncidentEntity?> GetByIdAsync(int id);
    
    Task<IEnumerable<IncidentEntity>> GetAllByTrainAsync(long trainId);
    Task<IncidentEntity> AddAsync(IncidentDto incident , long trainId);
    Task DeleteAsync(int id);
}