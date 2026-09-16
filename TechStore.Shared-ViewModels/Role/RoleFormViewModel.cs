using System.ComponentModel.DataAnnotations;

namespace TechStore.Shared_ViewModels.Role;

public class RoleFormViewModel
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên role.")]
    [StringLength(256, ErrorMessage = "Tên role không được vượt quá 256 ký tự.")]
    [Display(Name = "Tên role")]
    public string Name { get; set; } = string.Empty;

    public bool IsSystemRole { get; set; }
}
