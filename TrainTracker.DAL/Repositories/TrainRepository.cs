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
            .ToListAsync();
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
    

    public async Task ClearAllAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Incidents");
        await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Incidents AUTO_INCREMENT = 1");

        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Trains");
        await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Trains AUTO_INCREMENT = 1");
    }

}