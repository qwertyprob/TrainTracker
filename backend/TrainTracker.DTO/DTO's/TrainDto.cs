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
    public int RawDelayTime { get; set; } //Время из JSON парсинга

    [JsonIgnore] 
    private int? _customDelayTime;

    [JsonIgnore] 
    public int DelayTime //Время в БД
    {
        get => _customDelayTime ?? MapToAnotherTime(RawDelayTime);
        set => _customDelayTime = value;
    } // Измененное время для сценария работы приложения
    
    [JsonProperty("nextStopObj")]
    public StationDto? NextStation { get; set; }
    
    [JsonIgnore] 
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore] 
    public DateTime? LastDelayUpdateAt { get; set; }

    [JsonIgnore] 
    public bool IsActive { get; set; } = true;
    
    [JsonIgnore] 
    public IEnumerable<IncidentDto>? Incidents { get; set; } = new List<IncidentDto>();
    
    private int MapToAnotherTime(int rawDelayTime)
    {
        switch (rawDelayTime)
        {
            case -1: 
                return 0;
            case 0: 
                return new Random().Next(1,5);   // 1…4
            case 1: 
                return new Random().Next(6,10); // 6…10
            case 2: 
                return new Random().Next(1,5);
            case 3: 
                return new Random().Next(6,15);
            case 4: 
                return new Random().Next(1,9); // 10…19
            default: 
                return 0;
        }
    }

    
}