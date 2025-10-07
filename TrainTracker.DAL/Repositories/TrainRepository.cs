using Microsoft.EntityFrameworkCore;
using TrainTracker.DAL.Entities;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.DAL.Repositories;

public class TrainRepository : ITrainRepository
{
    private readonly AppDbContext _context;
    public TrainRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<TrainEntity?> GetByIdAsync(long id)
    {
        return await _context.Trains
            .Include(station => station.NextStation)
            .Include(incidents => incidents.Incidents)
            .FirstOrDefaultAsync(train => train.Id == id);

    }
    public async Task<IEnumerable<TrainEntity>> GetAllAsync()
    {
        return await _context.Trains
            .Include(t => t.NextStation)
            .Include(t => t.Incidents)
            .Where(x=>x.IsActive == true)
            .ToListAsync();
    }
    public async Task AddAsync(TrainDto train)
    {
        var trainEntity = new TrainEntity()
        {
            Id = train.Id,
            Name = train.Name,
            Number = train.Number,
            DelayTime = train.DelayTime,
            NextStation = new StationEntity()
            {
                StationTitle = train.NextStation!.Title
            },
            
            CreatedAt = DateTime.UtcNow,
            LastDelayUpdateAt = DateTime.UtcNow
            
            
        };
        await _context.Trains.AddAsync(trainEntity);
        await _context.SaveChangesAsync();
        
    }
    public async Task ClearAllAsync()
    {
        var incidents = await _context.Incidents.ToListAsync();
        _context.Incidents.RemoveRange(incidents);

        var trains = await _context.Trains.ToListAsync();
        _context.Trains.RemoveRange(trains);

        await _context.SaveChangesAsync();
    }
    public async Task DeactivateByIdAsync(long id)
    {
        
        var train = await this.GetByIdAsync(id);
        
        if (train == null)
            return;

        train.IsActive = false;

        if (train.Incidents != null && train.Incidents.Any())
        {
            foreach (var incident in train.Incidents)
            {
                incident.IsActive = false;
            }
        }

        if (train.NextStation != null)
        {
            train.NextStation.IsActive = false;
        }

        await _context.SaveChangesAsync();
        
        
    }
    public async Task ChangeDelayTimeAsync(long id, int delayTime)
    {
        var trainEntity = await this.GetByIdAsync(id);
        
        if (trainEntity == null)
            return;
        
        trainEntity.DelayTime = delayTime;
        trainEntity.LastDelayUpdateAt = DateTime.UtcNow;
        
        
        await _context.SaveChangesAsync();
        
    }
    
    
    

}