using Microsoft.AspNetCore.Identity;
using Inventory.Api.Models;
using Inventory.Api.Entity;
using System;

namespace Inventory.Api.Data;
public static class DbSeeder
{
    public static async Task SeedData(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var db = services.GetRequiredService<InventoryDbContext>();

        // 1️⃣ Create Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        // 2️⃣ Create Default Tenant
        if (!db.Tenants.Any())
        {
            db.Tenants.Add(new Tenant { TenantName = "Tenant" });
            await db.SaveChangesAsync();
        }

        var tenantId = db.Tenants.First().TenantId;

        // 3️⃣ Create Admin User
        var adminEmail = "admin@inventory.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                TenantId = Convert.ToInt32(tenantId),
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, "Admin@123");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
