using Microsoft.AspNetCore.Mvc;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;
using TrainTracker.Api.Models;
using TrainTracker.Validators;

namespace TrainTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly ITrainService _trainService;
    private readonly IIncidentService _incidentService;

    public IncidentController(IIncidentService incidentService, ITrainService trainService)
    {
        _trainService = trainService;
        _incidentService = incidentService;
    }
    // Получить все инциденты конкретного поезда
    // GET: api/incident?trainId=5
    [HttpGet]
    public async Task<IActionResult> GetByTrain([FromQuery] int trainId)
    {
        var incidents = await _incidentService.GetAllIncidentsAsync(trainId);
        return Ok(incidents);
    }

    // Получить конкретный поезд с инцидентами
    // GET: api/incident/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrain(int id)
    {
        var train = await _trainService.GetTrainByIdAsync(id);
        if (train?.Data == null)
            return NotFound(new { Message = "Train not found" });

        return Ok(train.Data);
    }

    // POST: api/incident
    [HttpPost]
    public async Task<IActionResult> AddIncident([FromBody] IncidentPostModel model)
    {
        // валидация через FluentValidation
        var validator = new IncidentValidator();
        var result = await validator.ValidateAsync(model.IncidentDto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var response = await _incidentService.AddIncidentAsync(model.IncidentDto, model.TrainId);
        return Ok(response);
    }
}