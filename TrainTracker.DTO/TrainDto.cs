namespace TrainTracker.DTO;

public class TrainDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public int Number { get; set; }
    
    public int DelayTime { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    
    public string? NextStation { get; set; }  
    
    public IEnumerable<IncidentDto> Incidents { get; set; } = new List<IncidentDto>();
    
}