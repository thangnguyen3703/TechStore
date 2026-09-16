using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class ComingSoonController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
