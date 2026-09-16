using System.ComponentModel.DataAnnotations;

namespace TechStore.Shared_ViewModels.User;

public class UserEditViewModel
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Đang hoạt động")]
    public bool IsActive { get; set; }

    public List<string> SelectedRoleNames { get; set; } = [];

    public IReadOnlyList<RoleSelectionViewModel> AvailableRoles { get; set; } = [];
}

public class RoleSelectionViewModel
{
    public string Name { get; set; } = string.Empty;

    public bool IsSelected { get; set; }
}

public class UserResetPasswordViewModel
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu mới")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu mới.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    [Display(Name = "Xác nhận mật khẩu mới")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
