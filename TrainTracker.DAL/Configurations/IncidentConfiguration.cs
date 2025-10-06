using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainTracker.DTO;

namespace TrainTracker.DAL.Configuration;

public class IncidentConfiguration :IEntityTypeConfiguration<IncidentDto>
{
    public void Configure(EntityTypeBuilder<IncidentDto> builder)
    {
        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();
    }
}