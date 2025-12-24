using TrainTracker.DTO;

namespace TrainTracker.Api.Models;

public class IncidentPostModel
{
    public long TrainId { get; set; }
    public IncidentDto IncidentDto { get; set; }
}
