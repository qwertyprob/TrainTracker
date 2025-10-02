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
        return await _context.Incidents.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<IncidentEntity?>> GetAllAsync()
    {
        return await _context.Incidents.ToListAsync();
    }


    public async Task AddAsync(IncidentDto incident,long trainId)
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