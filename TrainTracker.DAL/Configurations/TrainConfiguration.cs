using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainTracker.DAL.Entities;

namespace TrainTracker.DAL.Configuration;

public class TrainConfiguration : IEntityTypeConfiguration<TrainEntity>
{
    public void Configure(EntityTypeBuilder<TrainEntity> builder)
    {
        // 1:1 NextStation
        builder
            .HasOne(n => n.NextStation)
            .WithOne(t => t.Train)
            .HasForeignKey<StationEntity>(s => s.TrainId);
        
        // 1:M Incidents
        builder
            .HasMany<IncidentEntity>(i=>i.Incidents)
            .WithOne(t=>t.Train)
            .HasForeignKey(i=>i.TrainId);
        

    }
}