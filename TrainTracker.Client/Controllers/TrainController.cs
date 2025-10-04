using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;
using TrainTracker.Models;

namespace TrainTracker.Controllers;

public class TrainController : Controller
{
    private readonly ITrainService _trainService;
    private readonly IIncidentService _incidentService;


    public TrainController(ITrainService trainService, IIncidentService incidetService)
    {
        _trainService = trainService;
        _incidentService = incidetService;
    }
    
    [HttpGet]
    [Route("/trains")]
    public async Task<IActionResult> TestTrains()
    {
        var model = await _trainService.GetAllTrainsAsync();
        
        
        return Json(model);
    }
    
    [HttpGet]
    [Route("/incidents/{trainId}")]
    public async Task<IActionResult> TestIncidents(long trainId)
    {

        var model = await _incidentService.GetAllIncidentsAsync(trainId);

        return View("Error",model.Message);
        

    }
    
    
    public async Task<IActionResult> Index()
    {
        var model = await _trainService.GetAllTrainsAsync();
        
        
        return View(model);
    }
    
    [Route("/TrainInfo/{id}")]
    public async Task<IActionResult> TrainInfo(int id)
    {
        
        
        return View(id);
    }
    

    
}