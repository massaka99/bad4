using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using bad4.Models;

namespace UserApiLogic.Data
{
    public static class DbInitializer
    {
        public static async Task SeedUsers(UserManager<ApiUser> userManager)
        {
            const string adminEmail = "Admin@localhost";
            const string adminPassword = "Secret7$";
            const string bakerEmail = "Baker@localhost";
            const string bakerPassword = "Secret6$";
            const string driverEmail = "Driver@localhost";
            const string driverPassword = "Secret5$";
            const string managerEmail = "Manager@localhost";
            const string managerPassword = "Secret4$";

            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            // Seed admin user
            if ((await userManager.FindByNameAsync(adminEmail)) == null)
            {
                var adminUser = new ApiUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " + result.Errors.First().Description);
                }

                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adminUser.UserName),
                    new Claim("IsAdmin", "true"),
                    new Claim("IsManager", "true"),
                    new Claim("IsDriver", "true"),
                    new Claim("IsBaker", "true")
                };
                await userManager.AddClaimsAsync(adminUser, claim);
            }

            // Seed baker user
            if ((await userManager.FindByNameAsync(bakerEmail)) == null)
            {
                var user = new ApiUser { UserName = bakerEmail, Email = bakerEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, bakerPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user: " + result.Errors.First().Description);
                }

                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("IsBaker", "true")
                };
                await userManager.AddClaimsAsync(user, claim);
            }

            // Seed driver user
            if ((await userManager.FindByNameAsync(driverEmail)) == null)
            {
                var user = new ApiUser { UserName = driverEmail, Email = driverEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, driverPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user: " + result.Errors.First().Description);
                }

                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("IsDriver", "true")
                };
                await userManager.AddClaimsAsync(user, claim);
            }

            // Seed manager user
            if ((await userManager.FindByNameAsync(managerEmail)) == null)
            {
                var user = new ApiUser { UserName = managerEmail, Email = managerEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, managerPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user: " + result.Errors.First().Description);
                }

                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("IsManager", "true"),
                    new Claim("IsDriver", "true"),
                    new Claim("IsBaker", "true")
                };
                await userManager.AddClaimsAsync(user, claim);
            }
        }
    }
}