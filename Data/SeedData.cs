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
                    
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
