using Newtonsoft.Json;

namespace TrainTracker.DTO;

public class StationDto
{
    [JsonProperty("id")]
    public string RawId { get; set; }
    
    [JsonIgnore]
    public int Id => int.TryParse(RawId, out var result) ? result : 0;

    [JsonProperty("title")]
    public string Title { get; set; }
    
    public bool IsActive { get; set; } = true;

}