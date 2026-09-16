using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TechStore.Service.Interfaces;
using TechStore.Shared_ViewModels.User;

namespace TechStore.Areas.Admin.Controllers;

public class UserController : AdminBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetAllAsync());
    }

    public async Task<IActionResult> Create()
    {
        return View(await _userService.GetCreateViewModelAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await _userService.GetCreateViewModelAsync(model));
        }

        var result = await _userService.CreateAsync(model);
        if (!result.Success)
        {
            if (result.UserCreated)
            {
                TempData["Error"] = result.Error;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, result.Error ?? "Không thể tạo user.");
            return View(await _userService.GetCreateViewModelAsync(model));
        }

        TempData["Success"] = "Đã tạo user thành công.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var model = await _userService.GetByIdAsync(id);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        if (IsCurrentUser(model.UserId) && !model.IsActive)
        {
            ModelState.AddModelError(nameof(model.IsActive), "Bạn không thể tự khóa tài khoản đang đăng nhập.");
        }

        if (!ModelState.IsValid)
        {
            await _userService.PopulateRoleOptionsAsync(model);
            return View(model);
        }

        var result = await _userService.UpdateAsync(model);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Không thể cập nhật user.");
            await _userService.PopulateRoleOptionsAsync(model);
            return View(model);
        }

        TempData["Success"] = "Đã cập nhật user thành công.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(string id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _userService.ToggleActiveAsync(id, currentUserId);
        TempData[result.Success ? "Success" : "Error"] = result.Success
            ? result.IsActive
                ? "Mở khóa tài khoản thành công."
                : "Khóa tài khoản thành công."
            : result.Error ?? "Không thể thay đổi trạng thái user.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(
                " ",
                ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage));
            return RedirectToAction(nameof(Edit), new { id = model.UserId });
        }

        var result = await _userService.ResetPasswordAsync(model.UserId, model.NewPassword);
        TempData[result.Success ? "Success" : "Error"] = result.Success
            ? "Đã reset mật khẩu thành công."
            : result.Error ?? "Không thể reset mật khẩu.";

        return RedirectToAction(nameof(Edit), new { id = model.UserId });
    }

    private bool IsCurrentUser(string userId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return !string.IsNullOrWhiteSpace(currentUserId)
            && string.Equals(currentUserId, userId, StringComparison.Ordinal);
    }
}
