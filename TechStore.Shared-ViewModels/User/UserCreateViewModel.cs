using System.ComponentModel.DataAnnotations;

namespace TechStore.Shared_ViewModels.User;

public class UserCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    [Display(Name = "Xác nhận mật khẩu")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Đang hoạt động")]
    public bool IsActive { get; set; } = true;

    public List<string> SelectedRoleNames { get; set; } = [];

    public IReadOnlyList<RoleSelectionViewModel> AvailableRoles { get; set; } = [];
}
