using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainTracker.Models;

namespace TrainTracker.Controllers;

public class DashboardController : Controller
{
    
    public DashboardController()
    {
     
        
    }

    public async Task<IActionResult> Index()
    {
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}