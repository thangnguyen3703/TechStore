using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

public class DemoController : AdminBaseController
{
    public IActionResult Tables() => View();

    public IActionResult Buttons() => View();

    public IActionResult Forms() => View();

    public IActionResult Blank() => View();
}
