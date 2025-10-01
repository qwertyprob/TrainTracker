namespace TrainTracker.DAL.Entities;

public class StationEntity
{
    public int Id { get; set; }
    public string StationTitle { get; set; }
    //Можно добавить данные..
    
    //Связь 1:1 TrainEntity
    public long TrainId { get; set; } // FK
    public TrainEntity Train { get; set; }
}