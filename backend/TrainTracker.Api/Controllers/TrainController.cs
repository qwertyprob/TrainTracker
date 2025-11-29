using Microsoft.AspNetCore.Mvc;
using TrainTracker.BLL;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;

namespace TrainTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly ITrainService _trainService;

    public TrainController(ITrainService trainService)
    {
        _trainService = trainService;
    }
    
    // Получить конкретный поезд с инцидентами
    // GET: api/train/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrain(int id)
    {
        var train = await _trainService.GetTrainByIdAsync(id);
        if (train?.Data?.RawId == null)
            return NotFound(train);
        
        
        return Ok(train.Data);
    }
    
    // GET: api/train
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<IEnumerable<TrainDto>>>> LoadTrains()
    {
        var model = await _trainService.GetAllTrainsAsync() 
                    ?? new BaseResponseModel<IEnumerable<TrainDto>>();

        return model.StatusCode switch
        {
            404 => NotFound(model),
            400 => BadRequest(model),
            _ => Ok(model)
        };
    }
    
}