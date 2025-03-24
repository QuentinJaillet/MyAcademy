using Microsoft.AspNetCore.Identity;
using MyAcademy.Identity.Infrastructure.Entities;

namespace MyAcademy.Identity.Infrastructure.Persistance;

public static class IdentitySeedData
{
    private const string AdminEmail = "admin@myacademy.com";
    private const string AdminPassword = "Admin123!";

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Create Admin role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        // Create Admin user if it doesn't exist
        var adminUser = await userManager.FindByEmailAsync(AdminEmail);
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, AdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine("✅ Compte admin créé avec succès !");
            }
            else
            {
                Console.WriteLine("❌ Erreur lors de la création du compte admin.");
            }
        }
        else
        {
            Console.WriteLine("ℹ️ Compte admin déjà existant.");
        }
    }
}