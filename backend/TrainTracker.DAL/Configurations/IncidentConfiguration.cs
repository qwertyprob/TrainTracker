using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainTracker.DAL.Entities;

namespace TrainTracker.DAL.Configuration;

public class IncidentConfiguration :IEntityTypeConfiguration<IncidentEntity>
{
    public void Configure(EntityTypeBuilder<IncidentEntity> builder)
    {
        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();
        
        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Reason)
            .IsRequired()
            .HasMaxLength(100);
        
    }
}