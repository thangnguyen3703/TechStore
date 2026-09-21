using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Create() => View();

    [HttpGet]
    public IActionResult Edit(string? id)
    {
        ViewData["UserId"] = id;
        return View();
    }
}
