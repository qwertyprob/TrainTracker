using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainTracker.BLL;
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
    
    // Ajax - GET
    [HttpGet("/LoadTrains")]
    public async Task<IActionResult> LoadTrains()
    {
        var model = await _trainService.GetAllTrainsAsync() ?? new BaseResponseModel<IEnumerable<TrainDto>>();
        return PartialView("Components/Cards", model);
    }

    [HttpGet("/LoadTable")]
    public async Task<IActionResult> LoadTable()
    {
        var model = await _trainService.GetAllTrainsAsync() ?? new BaseResponseModel<IEnumerable<TrainDto>>();
        return PartialView("Components/Table", model);
    }

    
    
    public  IActionResult Trains()
    {
        
        
        return View();
    }
    
   
    

    
}