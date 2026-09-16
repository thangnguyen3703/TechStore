using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStore.Service.Interfaces;
using TechStore.Shared_ViewModels.Role;

namespace TechStore.Service.Services;

public class RoleService : IRoleService
{
    private const string SystemRoleName = "Admin";
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<TechStore.Data.Identity.ApplicationUser> _userManager;

    public RoleService(
        RoleManager<IdentityRole> roleManager,
        UserManager<TechStore.Data.Identity.ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<RoleViewModel>> GetAllAsync()
    {
        var roles = await _roleManager.Roles
            .AsNoTracking()
            .OrderBy(role => role.Name)
            .ToListAsync();

        var result = new List<RoleViewModel>(roles.Count);
        foreach (var role in roles)
        {
            var name = role.Name ?? string.Empty;
            var users = string.IsNullOrWhiteSpace(name)
                ? []
                : await _userManager.GetUsersInRoleAsync(name);

            result.Add(new RoleViewModel
            {
                Id = role.Id,
                Name = name,
                UserCount = users.Count,
                IsSystemRole = IsSystemRole(name)
            });
        }

        return result;
    }

    public async Task<RoleFormViewModel?> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role is null)
        {
            return null;
        }

        return new RoleFormViewModel
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            IsSystemRole = IsSystemRole(role.Name)
        };
    }

    public async Task<(bool Success, string? Error)> CreateAsync(string name)
    {
        var normalizedName = NormalizeName(name);
        if (normalizedName is null)
        {
            return (false, "Tên role không được để trống.");
        }

        if (normalizedName.Length > 256)
        {
            return (false, "Tên role không được vượt quá 256 ký tự.");
        }

        if (await _roleManager.RoleExistsAsync(normalizedName))
        {
            return (false, $"Role '{normalizedName}' đã tồn tại.");
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(normalizedName));
        return result.Succeeded
            ? (true, null)
            : (false, FormatErrors(result));
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string id, string name)
    {
        var role = string.IsNullOrWhiteSpace(id)
            ? null
            : await _roleManager.FindByIdAsync(id);

        if (role is null)
        {
            return (false, "Không tìm thấy role cần cập nhật.");
        }

        if (IsSystemRole(role.Name))
        {
            return (false, "Role Admin là role hệ thống và không thể đổi tên.");
        }

        var normalizedName = NormalizeName(name);
        if (normalizedName is null)
        {
            return (false, "Tên role không được để trống.");
        }

        if (normalizedName.Length > 256)
        {
            return (false, "Tên role không được vượt quá 256 ký tự.");
        }

        var duplicate = await _roleManager.FindByNameAsync(normalizedName);
        if (duplicate is not null && !string.Equals(duplicate.Id, role.Id, StringComparison.Ordinal))
        {
            return (false, $"Role '{normalizedName}' đã tồn tại.");
        }

        role.Name = normalizedName;
        var result = await _roleManager.UpdateAsync(role);
        return result.Succeeded
            ? (true, null)
            : (false, FormatErrors(result));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(string id)
    {
        var role = string.IsNullOrWhiteSpace(id)
            ? null
            : await _roleManager.FindByIdAsync(id);

        if (role is null)
        {
            return (false, "Không tìm thấy role cần xóa.");
        }

        if (IsSystemRole(role.Name))
        {
            return (false, "Role Admin là role hệ thống và không thể xóa.");
        }

        var result = await _roleManager.DeleteAsync(role);
        return result.Succeeded
            ? (true, null)
            : (false, FormatErrors(result));
    }

    private static string? NormalizeName(string? name)
    {
        var trimmed = name?.Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed;
    }

    private static bool IsSystemRole(string? name) =>
        string.Equals(name, SystemRoleName, StringComparison.OrdinalIgnoreCase);

    private static string FormatErrors(IdentityResult result) =>
        string.Join(" ", result.Errors.Select(error => error.Description));
}
