namespace TrainTracker.DAL.Entities;

public class IncidentEntity
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Reason { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    //Связь 1:M TrainEntity
    public long TrainId { get; set; } // FK
    public TrainEntity Train { get; set; }
}