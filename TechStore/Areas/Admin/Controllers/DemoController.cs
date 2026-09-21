using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

[Area("Admin")]
public class DemoController : Controller
{
    public IActionResult Tables() => View();

    public IActionResult Buttons() => View();

    public IActionResult Forms() => View();

    public IActionResult Blank() => View();
}
