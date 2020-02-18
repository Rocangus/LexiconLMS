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
        private static Faker faker;
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

                if (!context.ActivityTypes.Any())
                {
                    string[] activityTypeNames = new string[] { "E-learning session", "Lecture", "Exam", "Assignment",
                        "Practice session" };

                    foreach (var typeName in activityTypeNames)
                    {
                        var activityType = new ActivityType
                        {
                            Name = typeName
                        };
                        context.Add(activityType);
                    }
                    await context.SaveChangesAsync();
                }

                var faker = GetFaker();

                if (context.Users.Count() <= 10)
                {
                    var bogusPassword = configuration["LMS:AdminPW"];
                    for (int i = 0; i < 200; i++)
                    {
                        var name = faker.Name.FullName();
                        var userEmail = faker.Internet.Email($"{name}");
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

                if (context.Courses.Count() <= 1)
                {
                    var now = DateTime.UtcNow;
                    for (int i = 0; i < 4; i++)
                    {
                        var course = new Course
                        {
                            Name = faker.Company.CatchPhrase(),
                            StartDate = faker.Date.Future(1, now),
                            Description = faker.Lorem.Sentences(6)                            
                        };

                        await context.AddAsync(course);
                    }
                    await context.SaveChangesAsync();
                }

                foreach (var course in context.Courses.ToList())
                {
                    if (!context.Modules.Any(m => m.CourseId == course.Id))
                    {
                        var startDate = course.StartDate.AddDays(faker.Random.Int(0, 90));
                        for (int i = 0; i < faker.Random.Int(2, 10); i++)
                        {
                            var module = new Module
                            {
                                CourseId = course.Id,
                                Name = faker.Company.CatchPhrase(),
                                Description = faker.Lorem.Sentences(6),
                                StartDate = startDate,
                                EndDate = startDate.AddDays(faker.Random.Int(0, 5))
                            };

                            context.Add(module);
                        }
                        context.SaveChanges();
                    }
                }


                foreach (var module in await context.Modules.ToListAsync())
                {
                    if(!context.Activities.Any(a => a.ModuleId == module.Id))
                    {
                        for (int i = 0; i < faker.Random.Int(0, 5); i++)
                        {
                            var activityStartTime = faker.Date.Soon((module.StartDate - module.EndDate).Days, module.StartDate);
                            var activityTypeCount = context.ActivityTypes.Count();
                            var activity = new Activity
                            {
                                Name = faker.Company.CatchPhrase(),
                                ActivityTypeId = faker.Random.Int(0, activityTypeCount),
                                Description = faker.Lorem.Sentences(6),
                                StartTime = activityStartTime,
                                EndTime = activityStartTime + faker.Date.Timespan(new TimeSpan(TimeSpan.TicksPerDay)),
                                ModuleId = module.Id
                            };
                            context.Add(activity);
                        }
                        await context.SaveChangesAsync();
                    }
                }

                var teacherRole = context.Roles.Where(r => r.Name.Equals(roleNames[1])).FirstOrDefault();
                var courseCount = context.Courses.Count();

                foreach (var user in await context.Users.ToListAsync())
                {
                    if (!context.UserCourses.Any(uc => uc.SystemUserId.Equals(user.Id)))
                    {
                        SystemUserCourse userCourse;
                        if (context.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals(teacherRole.Id)))
                        {
                            userCourse = new SystemUserCourse
                            {
                                CourseId = faker.Random.Int(1, courseCount),
                                SystemUserId = user.Id,
                                Discriminator = "Teacher"
                            };
                        }
                        else
                        {
                            userCourse = new SystemUserCourse
                            {
                                CourseId = faker.Random.Int(1, courseCount),
                                SystemUserId = user.Id,
                                Discriminator = "Student"
                            };
                        };

                        context.Add(userCourse);
                        await context.SaveChangesAsync();
                    }
                }



            }
        }

        private static Faker GetFaker()
        {
            if (faker == null)
            {
                faker = new Faker("sv");
            }
            return faker;
        }
    }
}
