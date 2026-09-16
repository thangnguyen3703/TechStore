using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

public class DashboardController : AdminBaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
