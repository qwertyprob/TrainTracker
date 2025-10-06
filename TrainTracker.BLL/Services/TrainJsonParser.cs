using System.Text;
using Newtonsoft.Json;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.BLL.Services;

public class TrainJsonParser
{
    private readonly string _filePath;

    public TrainJsonParser(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "App_Data", "task.json");
    }

    public async Task<List<TrainDto>> ParseAsync()
    {
        if (!File.Exists(_filePath))
            return new List<TrainDto>();

        var json = await File.ReadAllTextAsync(_filePath);
        var request = JsonConvert.DeserializeObject<JsonRequestModel>(json) ?? new JsonRequestModel();
        
        return request.Data?
            .Select(d =>
            {
                var train = d.Train;
                train.Name = d.Name; // костыль для отображения имени
                return train;
            }).ToList() ?? new List<TrainDto>();
    }
}
