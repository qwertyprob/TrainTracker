using Microsoft.EntityFrameworkCore;
using TrainTracker.DAL.Entities;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.DAL.Repositories;

public class IncidentRepository: IIncidentRepository

{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IncidentEntity?> GetByIdAsync(int id)
    {
        return await _context.Incidents
            .FirstOrDefaultAsync(x => x.Id== id);
    }
    
    

    public async Task<IEnumerable<IncidentEntity>> GetAllByTrainAsync(long trainId)
    {
        var train = await _context.Trains
            .Include(i => i.Incidents)
            .FirstOrDefaultAsync(x => x.Id == trainId);

        if (train == null)
            return Enumerable.Empty<IncidentEntity>();

        return train.Incidents ?? Enumerable.Empty<IncidentEntity>();
    }



    public async Task<IncidentEntity> AddAsync(IncidentDto incident,long trainId)
    {
        var incidentEntity = new IncidentEntity()
        {
            TrainId = trainId,
            Username = incident.Username,
            Reason = incident.Reason,
            Comment = incident.Comment,
        };

        await _context.Incidents.AddAsync(incidentEntity);
        
        await _context.SaveChangesAsync();

        return incidentEntity;

    }

    public async Task DeleteAsync(int id)
    {
        var incident = await this.GetByIdAsync(id);
        if (incident == null) 
            return;

        _context.Incidents.Remove(incident);
        await _context.SaveChangesAsync();
    }

}