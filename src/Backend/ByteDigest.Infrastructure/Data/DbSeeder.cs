using ByteDigest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDigest.Infrastructure.Data;

/// <summary>
/// Seeds initial data to the database.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the database with initial data including admin user and roles.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "User" };

        foreach (string roleName in roleNames)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        string adminEmail = "admin@bytedigest.com";
        ApplicationUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            ApplicationUser newAdmin = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "System Administrator",
                CreatedAt = DateTime.UtcNow
            };

            IdentityResult result = await userManager.CreateAsync(newAdmin, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
