using System;
using System.Collections.Generic;
using System.Text;
using LexiconLMS.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Core.Models.Documents;

namespace LexiconLMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<SystemUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DocumentsActivities>().HasKey("DocumentId", "ActivityId");
            builder.Entity<DocumentsModules>().HasKey("DocumentId", "ModuleId");
            builder.Entity<DocumentsCourses>().HasKey("DocumentId", "CourseId");
        }

        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<SystemUserCourse> UserCourses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentsActivities> DocumentsActivities { get; set; }
        public DbSet<DocumentsModules> DocumentsModules { get; set; }
    }
}
