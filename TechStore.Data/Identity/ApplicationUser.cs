using Microsoft.AspNetCore.Identity;

namespace TechStore.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}