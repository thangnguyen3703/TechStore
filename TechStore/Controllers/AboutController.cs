using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class AboutController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();
}
