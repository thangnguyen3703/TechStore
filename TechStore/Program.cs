using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Infrastructure.Contexts;
using TechStore.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

// Đăng ký DbContext
builder.Services.AddDbContext<TechStoreDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<TechStoreDBEntities>();

builder.Services.AddScoped<IBranchService, BranchService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();