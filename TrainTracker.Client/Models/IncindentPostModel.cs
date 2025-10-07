using TrainTracker.DTO;

namespace TrainTracker.Models;

public class IncidentPostModel
{
    public long TrainId { get; set; }
    public IncidentDto IncidentDto { get; set; }
}
