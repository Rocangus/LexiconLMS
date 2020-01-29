using LexiconLMS.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider services, IConfiguration configuration,
            UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var option = services.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

            using (var context = new ApplicationDbContext(option))
            {
                string[] roleNames = { "Student", "Teacher" };

                foreach (var name in roleNames)
                {
                    IdentityResult roleExistsResult;
                    if (!await roleManager.RoleExistsAsync(name))
                    {
                        roleExistsResult = await roleManager.CreateAsync(new IdentityRole(name));
                        if(roleExistsResult != IdentityResult.Success)
                        {
                            throw new Exception(string.Join("\n", roleExistsResult.Errors));
                        }
                    }
                }

                string adminName = "Administrator";
                string adminEmail = "admin@lexiconlms.se";
                if (!context.Users.Any(u => u.Email == adminEmail))
                {
                    SystemUser admin = new SystemUser
                    {
                        UserName = "admin@lexiconlms.se",
                        Name = adminName,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var adminPassword = configuration["LMS:AdminPW"];

                    var addUserResult = await userManager.CreateAsync(admin, adminPassword);

                    if (!addUserResult.Succeeded)
                    {
                        throw new Exception(string.Join("\n", addUserResult.Errors));
                    }
                    
                    var addToRoleResult = await userManager.AddToRoleAsync(admin, roleNames[1]);

                    if(!addToRoleResult.Succeeded)
                    {
                        throw new Exception(string.Join("\n", addToRoleResult.Errors));
                    }

                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
