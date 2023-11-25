using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (userManager.FindByEmailAsync("user@localhost").Result == null)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = "user@localhost",
                Email = "user@localhost",
                NormalizedUserName = "USER@LOCALHOST",
                NormalizedEmail = "USER@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = userManager.CreateAsync(applicationUser, "Pa$$w0rd132").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(applicationUser, "User").Wait();
            }
        }

        if (userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = userManager.CreateAsync(applicationUser, "Pa$$w0rd132").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(applicationUser, "Admin").Wait();
            }
        }
    }

    public void SeedRoles()
    {
        if (!roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole identityRole = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };

            roleManager.CreateAsync(identityRole).Wait();
        }

        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole identityRole = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            roleManager.CreateAsync(identityRole).Wait();
        }
    }
}
