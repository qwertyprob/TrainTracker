using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTracker.DAL.Entities;

public class TrainEntity
{
    public long Id { get; set; } //data[0].id
    
    public int Name { get; set; } //data[0].name
    
    public int Number { get; set; } //data[0].returnValue.train 
    
    public int DelayTime { get; set; } //data[0].returnValue.arrivingTime
    
    public DateTime LastUpdate { get; set; } //время обновления
    
    // Связь 1:1 StationEntity
    public StationEntity? NextStation { get; set; } //data[0].returnValue.nextStopObj 
    
    // Связь 1:M IncidentEntity
    public ICollection<IncidentEntity>? Incidents { get; set; } = new List<IncidentEntity>(); // данные от пользователя
    

}