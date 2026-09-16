using TechStore.Shared_ViewModels.User;

namespace TechStore.Service.Interfaces;

public interface IUserService
{
    Task<List<UserViewModel>> GetAllAsync();

    Task<UserCreateViewModel> GetCreateViewModelAsync(UserCreateViewModel? model = null);

    Task<UserEditViewModel?> GetByIdAsync(string id);

    Task PopulateRoleOptionsAsync(UserEditViewModel model);

    Task<UserCreateResult> CreateAsync(UserCreateViewModel model);

    Task<(bool Success, string? Error)> UpdateAsync(UserEditViewModel model);

    Task<(bool Success, string? Error, bool IsActive)> ToggleActiveAsync(
        string userId,
        string? currentUserId);

    Task<(bool Success, string? Error)> ResetPasswordAsync(string id, string newPassword);
}

public record UserCreateResult(bool Success, bool UserCreated, string? Error);
