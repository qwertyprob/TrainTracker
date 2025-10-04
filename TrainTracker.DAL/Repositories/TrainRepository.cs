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
        return await _context.Trains.ToListAsync();
        
    }

    public async Task AddAsync(TrainDto train)
    {
        var trainEntity = new TrainEntity()
        {
            Id = train.Id,
            Name = train.Name,
            Number = train.Number,
            DelayTime = train.RawDelayTime,
            NextStation = new StationEntity()
            {
                StationTitle = train.NextStation.Title
            },
            
            CreatedAt = train.CreatedAt,
            
            
        };
        await _context.Trains.AddAsync(trainEntity);
        await _context.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(long id)
    {
        var trainToDelete = await this.GetByIdAsync(id);
        
        if(trainToDelete == null)
            return;
        
        _context.Trains.Remove(trainToDelete);

        await _context.SaveChangesAsync();
    }

    public async Task ClearAllAsync()
    {
        _context.Trains.RemoveRange(_context.Trains);
        _context.Incidents.RemoveRange(_context.Incidents);

        await _context.SaveChangesAsync();

    }
}