using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Service.Interfaces;
using TechStore.Shared_ViewModels.Account;

namespace TechStore.Controllers;

[AllowAnonymous]
public class SetupController : Controller
{
    private readonly IAccountService _accountService;

    public SetupController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (await _accountService.HasAdminAsync())
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new SetupAdminViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(SetupAdminViewModel model)
    {
        if (await _accountService.HasAdminAsync())
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _accountService.SetupAdminAsync(model);
        if (result.AlreadyConfigured)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Không thể tạo tài khoản Admin.");
            return View(model);
        }

        TempData["Success"] = "Tạo tài khoản Admin thành công. Vui lòng đăng nhập.";
        return RedirectToAction("Login", "Account");
    }
}
