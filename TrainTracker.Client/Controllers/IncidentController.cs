using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrainTracker.BLL;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;
using TrainTracker.Models;
using TrainTracker.Validators;

namespace TrainTracker.Controllers;

public class IncidentController:Controller
{
    private readonly ITrainService _trainService;
    private readonly IIncidentService _incidentService;

    public IncidentController(IIncidentService incidentService, ITrainService trainService)
    {
        _trainService = trainService;
        _incidentService = incidentService;
    }
    [Route("/Incidents/{id}")]
    public async Task<IActionResult> Incidents(int id)
    {
        var incidents = await _trainService.GetTrainByIdAsync(id);
        
        return View(incidents.Data);
    }
    public async Task<IActionResult> Test(int id)
    {
        var model = await _incidentService.GetAllIncidentsAsync(id);
        
        return PartialView("Test",id);
    }
    
    
    //Ajax
    //GET
    public async Task<IActionResult> LoadIncidents(int id)
    {
        var model = await _incidentService.GetAllIncidentsAsync(id);

        return PartialView("Components/ListOfIncidents", model);
    }
    
    //POST
    [HttpPost("/Incident/AddIncidentAsync")]
    public async Task<IActionResult> AddIncidentAsync([FromBody] IncidentPostModel postModel)
    {
        var validator = new IncidentValidator();

        var validationResult = await validator.ValidateAsync(postModel.IncidentDto);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError($"{error.PropertyName}", error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        var response = await _incidentService.AddIncidentAsync(postModel.IncidentDto, postModel.TrainId);

        return Ok(response);
    }


    
    
    
    
}