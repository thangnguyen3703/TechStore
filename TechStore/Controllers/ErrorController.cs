using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers;

public class ErrorController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index(int? statusCode = null)
    {
        ViewBag.StatusCode = statusCode;

        if (statusCode is >= 400 and <= 599)
        {
            Response.StatusCode = statusCode.Value;
        }

        return View();
    }
}
