using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

[Area("ADMIN")]
[Authorize(Policy = "AdminArea")]
public abstract class AdminBaseController : Controller
{
}
