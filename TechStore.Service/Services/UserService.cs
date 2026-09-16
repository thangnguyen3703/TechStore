using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TechStore.Data.Identity;
using TechStore.Service.Interfaces;
using TechStore.Shared_ViewModels.User;

namespace TechStore.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<UserViewModel>> GetAllAsync()
    {
        var users = await _userManager.Users
            .AsNoTracking()
            .OrderBy(user => user.Email)
            .ToListAsync();

        var result = new List<UserViewModel>(users.Count);
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserViewModel
            {
                UserId = user.Id,
                Email = user.Email ?? user.UserName ?? string.Empty,
                RoleNames = roles.OrderBy(name => name).ToArray(),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }

        return result;
    }

    public async Task<UserCreateViewModel> GetCreateViewModelAsync(UserCreateViewModel? model = null)
    {
        model ??= new UserCreateViewModel();
        model.AvailableRoles = await GetRoleOptionsAsync(model.SelectedRoleNames);
        return model;
    }

    public async Task<UserEditViewModel?> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var model = new UserEditViewModel
        {
            UserId = user.Id,
            Email = user.Email ?? user.UserName ?? string.Empty,
            IsActive = user.IsActive,
            SelectedRoleNames = roles.ToList()
        };

        await PopulateRoleOptionsAsync(model);
        return model;
    }

    public async Task PopulateRoleOptionsAsync(UserEditViewModel model)
    {
        model.AvailableRoles = await GetRoleOptionsAsync(model.SelectedRoleNames);
    }

    public async Task<UserCreateResult> CreateAsync(UserCreateViewModel model)
    {
        var email = model.Email?.Trim();
        if (string.IsNullOrWhiteSpace(email))
        {
            return new UserCreateResult(false, false, "Email không được để trống.");
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            return new UserCreateResult(false, false, "Email không hợp lệ.");
        }

        if (!string.Equals(model.Password, model.ConfirmPassword, StringComparison.Ordinal))
        {
            return new UserCreateResult(false, false, "Mật khẩu xác nhận không khớp.");
        }

        if (await _userManager.FindByEmailAsync(email) is not null)
        {
            return new UserCreateResult(false, false, $"Email '{email}' đã được sử dụng.");
        }

        var roleValidation = await ValidateRolesAsync(model.SelectedRoleNames);
        if (!roleValidation.Success)
        {
            return new UserCreateResult(false, false, roleValidation.Error);
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        var createResult = await _userManager.CreateAsync(user, model.Password);
        if (!createResult.Succeeded)
        {
            return new UserCreateResult(false, false, FormatErrors(createResult));
        }

        if (roleValidation.RoleNames.Count == 0)
        {
            return new UserCreateResult(true, true, null);
        }

        var roleResult = await _userManager.AddToRolesAsync(user, roleValidation.RoleNames);
        if (roleResult.Succeeded)
        {
            return new UserCreateResult(true, true, null);
        }

        user.IsActive = false;
        user.UpdatedAt = DateTime.Now;
        var disableResult = await _userManager.UpdateAsync(user);
        var suffix = disableResult.Succeeded
            ? "Tài khoản đã được tự động khóa để tránh đăng nhập khi chưa có role đầy đủ."
            : "Không thể tự động khóa tài khoản; vui lòng kiểm tra ngay tài khoản vừa tạo.";

        return new UserCreateResult(
            false,
            true,
            $"Tài khoản đã được tạo nhưng gán role thất bại: {FormatErrors(roleResult)} {suffix}");
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(UserEditViewModel model)
    {
        var user = string.IsNullOrWhiteSpace(model.UserId)
            ? null
            : await _userManager.FindByIdAsync(model.UserId);

        if (user is null)
        {
            return (false, "Không tìm thấy user cần cập nhật.");
        }

        var roleValidation = await ValidateRolesAsync(model.SelectedRoleNames);
        if (!roleValidation.Success)
        {
            return (false, roleValidation.Error);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = roleValidation.RoleNames
            .Except(currentRoles, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var rolesToRemove = currentRoles
            .Except(roleValidation.RoleNames, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (rolesToAdd.Length > 0)
        {
            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
            {
                return (false, $"Không thể gán role: {FormatErrors(addResult)}");
            }
        }

        if (rolesToRemove.Length > 0)
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                return (false, $"Không thể bỏ role: {FormatErrors(removeResult)}");
            }
        }

        user.IsActive = model.IsActive;
        user.UpdatedAt = DateTime.Now;
        var updateResult = await _userManager.UpdateAsync(user);
        return updateResult.Succeeded
            ? (true, null)
            : (false, FormatErrors(updateResult));
    }

    public async Task<(bool Success, string? Error, bool IsActive)> ToggleActiveAsync(
        string userId,
        string? currentUserId)
    {
        var user = string.IsNullOrWhiteSpace(userId)
            ? null
            : await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return (false, "Không tìm thấy tài khoản.", false);
        }

        if (user.IsActive
            && !string.IsNullOrWhiteSpace(currentUserId)
            && string.Equals(user.Id, currentUserId, StringComparison.Ordinal))
        {
            return (false, "Bạn không thể tự khóa tài khoản đang đăng nhập.", true);
        }

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.Now;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? (true, null, user.IsActive)
            : (false, FormatErrors(result), !user.IsActive);
    }

    public async Task<(bool Success, string? Error)> ResetPasswordAsync(string id, string newPassword)
    {
        var user = string.IsNullOrWhiteSpace(id)
            ? null
            : await _userManager.FindByIdAsync(id);

        if (user is null)
        {
            return (false, "Không tìm thấy user cần reset mật khẩu.");
        }

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            return (false, "Mật khẩu mới không được để trống.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded
            ? (true, null)
            : (false, FormatErrors(result));
    }

    private async Task<IReadOnlyList<RoleSelectionViewModel>> GetRoleOptionsAsync(
        IEnumerable<string> selectedRoleNames)
    {
        var selected = new HashSet<string>(
            selectedRoleNames ?? [],
            StringComparer.OrdinalIgnoreCase);

        return await _roleManager.Roles
            .AsNoTracking()
            .OrderBy(role => role.Name)
            .Select(role => new RoleSelectionViewModel
            {
                Name = role.Name ?? string.Empty,
                IsSelected = role.Name != null && selected.Contains(role.Name)
            })
            .ToListAsync();
    }

    private async Task<(bool Success, List<string> RoleNames, string? Error)> ValidateRolesAsync(
        IEnumerable<string>? requestedRoleNames)
    {
        var requested = (requestedRoleNames ?? [])
            .Select(name => name?.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Select(name => name!)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var validated = new List<string>(requested.Count);
        foreach (var requestedName in requested)
        {
            var role = await _roleManager.FindByNameAsync(requestedName);
            if (role?.Name is null)
            {
                return (false, [], $"Role '{requestedName}' không tồn tại.");
            }

            validated.Add(role.Name);
        }

        return (true, validated, null);
    }

    private static string FormatErrors(IdentityResult result) =>
        string.Join(" ", result.Errors.Select(error => error.Description));

}
