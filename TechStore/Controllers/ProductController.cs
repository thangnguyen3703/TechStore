using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class ProductController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Details() => View();
}
