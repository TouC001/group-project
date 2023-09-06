using Microsoft.AspNetCore.Mvc;

namespace MarysToyStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return new ContentResult { Content = "Hello world!" };
    }
}