namespace TrainTracker.DTO;

public class IncidentDto
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Reason { get; set; }
    
    public string? Comment { get; set; }
}