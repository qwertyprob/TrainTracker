using Newtonsoft.Json;

namespace TrainTracker.DTO;

public class JsonRequestModel
{
    [JsonProperty("data")]
    public IEnumerable<ReturnValue> Data { get; set; } = new List<ReturnValue>();
}

public class ReturnValue
{
    
    [JsonProperty("returnValue")]
    public TrainDto Train  { get; set; } = new TrainDto();
    
    [JsonProperty("name")]
    public string Name { get; set; }

    
}