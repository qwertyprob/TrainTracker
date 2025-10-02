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
            .Where(train => train.Id == id)
            .FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<TrainEntity>> GetAllAsync()
    { 
        return await _context.Trains.ToListAsync();
        
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
                StationTitle = train.NextStation
            },
            
            CreatedAt = train.CreatedAt,
            
            
        };
        await _context.Trains.AddAsync(trainEntity);
        await _context.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(long id)
    {
        var trainToDelete = await this.GetByIdAsync(id);
        
        _context.Trains.Remove(trainToDelete);

        await _context.SaveChangesAsync();
    }
}