using TechStore.Shared_ViewModels.Account;

namespace TechStore.Service.Interfaces;

public interface IAccountService
{
    Task<(bool Success, string? Error)> LoginAsync(string email,string password,bool rememberMe);

    Task<(bool Success, string? Error)> AdminLoginAsync(string email, string password, bool rememberMe);

    Task LogoutAsync();

    Task<bool> IsInRoleAsync(string email, string roleName);

    Task<bool> HasAdminAsync();

    Task<SetupAdminResult> SetupAdminAsync(SetupAdminViewModel model);
}

public record SetupAdminResult(bool Success, bool AlreadyConfigured, string? Error);
