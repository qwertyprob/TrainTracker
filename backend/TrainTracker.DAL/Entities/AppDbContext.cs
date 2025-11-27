using Microsoft.EntityFrameworkCore;
using TrainTracker.DAL.Configuration;

namespace TrainTracker.DAL.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}

    public DbSet<TrainEntity> Trains { get; set; }
    public DbSet<StationEntity> Stations { get; set; }
    public DbSet<IncidentEntity> Incidents { get; set; }

    //Конфигурация TrainCofiguration
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TrainConfiguration());
        base.OnModelCreating(builder);
    }

    
}
