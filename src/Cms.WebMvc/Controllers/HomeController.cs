using System.Diagnostics;
using Cms.Application.Public.Interfaces;
using Cms.Application.Public.Models;
using Microsoft.AspNetCore.Mvc;
using Cms.WebMvc.Models;

namespace Cms.WebMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeUseCase _useCase;
   

    public HomeController(
        ILogger<HomeController> logger, 
        IHomeUseCase useCase)
    {
        _logger = logger;
        _useCase = useCase;
    }

    public IActionResult Index()
    {
        ViewData["AboutUs"] = _useCase.GetAboutUsInfo();
        ViewData["Services"] = _useCase.GetServicesInfo();
        
        List<HomeInfoDto> model = _useCase.GetPostsInfo();
        return View(model);
    }
    
    
    
    public IActionResult AboutUs()
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