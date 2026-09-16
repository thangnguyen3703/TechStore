using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TechStore.Data.Identity;
using TechStore.Service.Interfaces;
using TechStore.Shared_ViewModels.Account;

namespace TechStore.Service.Services;

public class AccountService : IAccountService
{
    private const string AdminRoleName = "Admin";
    private const string InvalidCredentialsMessage = "Email hoặc mật khẩu không đúng.";
    private static readonly SemaphoreSlim SetupLock = new(1, 1);

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<(bool Success, string? Error)> LoginAsync(
        string email,
        string password,
        bool rememberMe)
    {
        var normalizedEmail = email?.Trim();
        if (string.IsNullOrWhiteSpace(normalizedEmail)
            || string.IsNullOrEmpty(password)
            || !new EmailAddressAttribute().IsValid(normalizedEmail))
        {
            return (false, InvalidCredentialsMessage);
        }

        var user = await _userManager.FindByEmailAsync(normalizedEmail);
        if (user is null)
        {
            return (false, InvalidCredentialsMessage);
        }

        if (!user.IsActive)
        {
            return (false, "Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên.");
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            password,
            rememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return (true, null);
        }

        if (result.IsLockedOut)
        {
            return (false, "Tài khoản tạm thời bị khóa do đăng nhập sai nhiều lần. Vui lòng thử lại sau.");
        }

        return (false, InvalidCredentialsMessage);
    }

    public Task LogoutAsync() => _signInManager.SignOutAsync();

    public async Task<bool> IsInRoleAsync(string email, string roleName)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(roleName))
        {
            return false;
        }

        var user = await _userManager.FindByEmailAsync(email.Trim());
        return user is not null && await _userManager.IsInRoleAsync(user, roleName.Trim());
    }

    public async Task<bool> HasAdminAsync()
    {
        if (!await _roleManager.RoleExistsAsync(AdminRoleName))
        {
            return false;
        }

        var admins = await _userManager.GetUsersInRoleAsync(AdminRoleName);
        return admins.Count > 0;
    }

    public async Task<SetupAdminResult> SetupAdminAsync(SetupAdminViewModel model)
    {
        await SetupLock.WaitAsync();
        try
        {
            if (await HasAdminAsync())
            {
                return new SetupAdminResult(false, true, "Hệ thống đã có tài khoản Admin.");
            }

            var email = model.Email?.Trim();
            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return new SetupAdminResult(false, false, "Email không hợp lệ.");
            }

            if (string.IsNullOrEmpty(model.Password)
                || !string.Equals(model.Password, model.ConfirmPassword, StringComparison.Ordinal))
            {
                return new SetupAdminResult(false, false, "Mật khẩu xác nhận không khớp.");
            }

            if (!await _roleManager.RoleExistsAsync(AdminRoleName))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(AdminRoleName));
                if (!roleResult.Succeeded)
                {
                    return new SetupAdminResult(false, false, FormatErrors(roleResult));
                }
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
                return new SetupAdminResult(false, false, FormatErrors(createResult));
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, AdminRoleName);
            if (addRoleResult.Succeeded)
            {
                return new SetupAdminResult(true, false, null);
            }

            var rollbackResult = await _userManager.DeleteAsync(user);
            var rollbackMessage = rollbackResult.Succeeded
                ? "Tài khoản vừa tạo đã được rollback."
                : $"Không thể rollback tài khoản vừa tạo: {FormatErrors(rollbackResult)}";

            return new SetupAdminResult(
                false,
                false,
                $"Không thể gán role Admin: {FormatErrors(addRoleResult)} {rollbackMessage}");
        }
        finally
        {
            SetupLock.Release();
        }
    }

    private static string FormatErrors(IdentityResult result) =>
        string.Join(" ", result.Errors.Select(error => error.Description));
}
