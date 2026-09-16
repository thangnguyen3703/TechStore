using TechStore.Shared_ViewModels.Role;

namespace TechStore.Service.Interfaces;

public interface IRoleService
{
    Task<List<RoleViewModel>> GetAllAsync();

    Task<RoleFormViewModel?> GetByIdAsync(string id);

    Task<(bool Success, string? Error)> CreateAsync(string name);

    Task<(bool Success, string? Error)> UpdateAsync(string id, string name);

    Task<(bool Success, string? Error)> DeleteAsync(string id);
}
