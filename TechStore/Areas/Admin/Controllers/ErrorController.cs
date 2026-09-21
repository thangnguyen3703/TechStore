using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

[Area("Admin")]
public class ErrorController : Controller
{
    [ActionName("NotFound")]
    public IActionResult PageNotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View();
    }
}
