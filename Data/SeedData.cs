using Bogus;
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


                if (context.Users.Count() <= 10)
                {
                    Faker faker = new Faker("sv");
                    var bogusPassword = configuration["LMS:AdminPW"];
                    for (int i = 0; i < 200; i++)
                    {
                        var name = faker.Name.FullName();
                        var userEmail = faker.Internet.Email($"{name.Split(' ')}");
                        var user = new SystemUser
                        {
                            Name = name,
                            Email = userEmail,
                            UserName = userEmail
                        };
                        var addFakeUserResult = await userManager.CreateAsync(user, bogusPassword);

                        if (!addFakeUserResult.Succeeded)
                        {
                            throw new Exception(string.Join("\n", addFakeUserResult.Errors));
                        }

                        var teacherRandomness = faker.Random.Int(1, 10);

                        IdentityResult addFakeUserToRoleResult;

                        if (teacherRandomness < 10)
                            addFakeUserToRoleResult = await userManager.AddToRoleAsync(user, roleNames[0]);
                        else
                            addFakeUserToRoleResult = await userManager.AddToRoleAsync(user, roleNames[1]);

                        if (!addFakeUserToRoleResult.Succeeded)
                        {
                            throw new Exception(string.Join("\n", addFakeUserToRoleResult.Errors));
                        }
                    }

                    await context.SaveChangesAsync();

                }

            }
        }
    }
}
