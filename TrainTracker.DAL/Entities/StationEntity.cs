namespace TrainTracker.DAL.Entities;

public class StationEntity
{
    public long Id { get; set; }
    public string StationTitle { get; set; }
    
    public bool IsActive { get; set; }
    
    //Можно добавить данные..
    
    //Связь 1:1 TrainEntity
    public long TrainId { get; set; } // FK
    public TrainEntity Train { get; set; }
}