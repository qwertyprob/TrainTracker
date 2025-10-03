using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainTracker.BLL.Interfaces;
using TrainTracker.DTO;
using TrainTracker.Models;

namespace TrainTracker.Controllers;

public class TrainController : Controller
{
    private readonly ITrainService _trainService;

    public TrainController(ITrainService trainService)
    {
        _trainService = trainService;
    }
    
    [HttpGet]
    [Route("/api")]
    public async Task<IActionResult> Test()
    {
        var model = await _trainService.GetAllTrainsAsync();
        
        
        return Json(model);
    }
    
    
    public async Task<IActionResult> Index()
    {
        var model = await _trainService.GetAllTrainsAsync();
        
        
        return View(model);
    }
    
    [Route("/TrainInfo/{id}")]
    public IActionResult TrainInfo(int id)
    {
        return View(id);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}