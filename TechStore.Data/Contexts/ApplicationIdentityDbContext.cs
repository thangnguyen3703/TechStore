using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechStore.Data.Identity;

namespace TechStore.Data.Contexts;

public class ApplicationIdentityDbContext
    : IdentityDbContext<ApplicationUser>
{
    public ApplicationIdentityDbContext(
        DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }
}