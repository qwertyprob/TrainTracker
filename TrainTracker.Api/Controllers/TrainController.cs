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

    // GET: api/train
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<IEnumerable<TrainDto>>>> LoadTrains()
    {
        var model = await _trainService.GetAllTrainsAsync() 
                    ?? new BaseResponseModel<IEnumerable<TrainDto>>();
        return Ok(model);
    }
    
}