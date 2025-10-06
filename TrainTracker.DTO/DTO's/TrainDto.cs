using Newtonsoft.Json;

namespace TrainTracker.DTO;

public class TrainDto
{
    
    [JsonProperty("id")]
    public string RawId { get; set; }
    
    [JsonIgnore]
    public long Id => long.TryParse(RawId, out var result) ? result : 0;
    
    [JsonIgnore]
    public string Name { get; set; }

    [JsonProperty("train")]
    public string RawNumber { get; set; }
    
    [JsonIgnore]
    public int Number => int.TryParse(RawNumber, out var result) ? result : 0;
    
    [JsonProperty("arrivingTime")]
    public int RawDelayTime { get; set; }

    [JsonIgnore] 
    public int DelayTime {
        get
        {
            return this.MapToAnotherTime(RawDelayTime);
        }
        
    } // Измененное время для сценария работы приложения
    
    [JsonProperty("nextStopObj")]
    public StationDto? NextStation { get; set; }
    
    [JsonIgnore] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [JsonIgnore] 
    
    public IEnumerable<IncidentDto>? Incidents { get; set; } = new List<IncidentDto>();
    
    //random

    private static readonly Random _random = new Random(); 

    private int MapToAnotherTime(int rawDelayTime)
    {
        switch (rawDelayTime)
        {
            case -1: 
                return 0;
            case 0: 
                return 5;   // 1…4
            case 1: 
                return 10;  // 6…10
            case 4: 
                return 20; // 10…19
            default: 
                return 0;
        }
    }

    
}