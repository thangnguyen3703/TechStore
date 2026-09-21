using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class DealsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
