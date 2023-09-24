using Microsoft.AspNetCore.Mvc;

namespace SoftwareBookList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Books()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    public IActionResult LogIn()
    {
        return View();
    }

    public IActionResult Account()
    {
        return View();
    }

    public IActionResult Discussion()
    {
        return View();
    }
}