using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class ContactController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();
}
