using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechStore.Areas.Admin.Controllers;

[Area("ADMIN")]
[Authorize(Roles = "ADMIN")]
public abstract class AdminBaseController : Controller
{
}
