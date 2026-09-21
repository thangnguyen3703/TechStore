using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class CartController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();
}
