using Microsoft.AspNetCore.Mvc;
using TechStore.Service.Interfaces;
using TechStore.Shared_ViewModels.Role;

namespace TechStore.Areas.Admin.Controllers;

public class RoleController : AdminBaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _roleService.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View(new RoleFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _roleService.CreateAsync(model.Name);
        if (!result.Success)
        {
            ModelState.AddModelError(nameof(model.Name), result.Error ?? "Không thể tạo role.");
            return View(model);
        }

        TempData["Success"] = "Đã tạo role thành công.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var model = await _roleService.GetByIdAsync(id);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RoleFormViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Id))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _roleService.UpdateAsync(model.Id, model.Name);
        if (!result.Success)
        {
            ModelState.AddModelError(nameof(model.Name), result.Error ?? "Không thể cập nhật role.");
            var current = await _roleService.GetByIdAsync(model.Id);
            model.IsSystemRole = current?.IsSystemRole ?? false;
            return View(model);
        }

        TempData["Success"] = "Đã cập nhật role thành công.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _roleService.DeleteAsync(id);
        TempData[result.Success ? "Success" : "Error"] = result.Success
            ? "Đã xóa role thành công."
            : result.Error ?? "Không thể xóa role.";

        return RedirectToAction(nameof(Index));
    }
}
