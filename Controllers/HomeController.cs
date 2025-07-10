using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProManagementSystem.Models;
using ProManagementSystem.Services;

namespace ProManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IModuleService _moduleService;

    public HomeController(ILogger<HomeController> logger, IModuleService moduleService)
    {
        _logger = logger;
        _moduleService = moduleService;
    }

    public async Task<IActionResult> Index()
    {
        var modules = await _moduleService.GetActiveModulesAsync();
        return View(modules);
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
