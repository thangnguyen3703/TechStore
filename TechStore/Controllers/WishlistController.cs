using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class WishlistController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();
}
