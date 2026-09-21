using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

[Area("Admin")]
public class RoleController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Create() => View();

    [HttpGet]
    public IActionResult Edit(string? id)
    {
        ViewData["RoleId"] = id;
        return View();
    }
}
