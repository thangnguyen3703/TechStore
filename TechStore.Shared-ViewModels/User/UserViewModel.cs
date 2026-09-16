namespace TechStore.Shared_ViewModels.User;

public class UserViewModel
{
    public string UserId { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IReadOnlyList<string> RoleNames { get; set; } = Array.Empty<string>();

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
