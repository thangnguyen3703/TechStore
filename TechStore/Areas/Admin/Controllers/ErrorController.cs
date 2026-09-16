using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

public class ErrorController : AdminBaseController
{
    [ActionName("NotFound")]
    public IActionResult PageNotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View();
    }
}
