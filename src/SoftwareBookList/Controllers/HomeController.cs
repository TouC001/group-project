using Microsoft.AspNetCore.Mvc;

namespace SoftwareBookList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> About()
    {
        return View();
    }

    public async Task<IActionResult> Account()
    {
        return View();
    }

    public async Task<IActionResult> Discussion()
    {
        return View();
    }
}