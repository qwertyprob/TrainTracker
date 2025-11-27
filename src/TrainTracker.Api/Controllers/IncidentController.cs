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
    private readonly IIncidentService _incidentService;

    public IncidentController(IIncidentService incidentService)
    {
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

    

    // POST: api/incident
    [HttpPost]
    public async Task<IActionResult> AddIncident([FromBody] IncidentPostModel model)
    {
        // валидация через FluentValidation
        var validator = new IncidentValidator();
        var result = await validator.ValidateAsync(model.IncidentDto);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(f => new
                {
                    property = f.PropertyName,
                    message = f.ErrorMessage
                });
                
            return BadRequest(errors);
        }

        var response = await _incidentService.AddIncidentAsync(model.IncidentDto, model.TrainId);
        return Ok(response);
    }
}