using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class SystemUser : IdentityUser
    {
        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        public ICollection<SystemUserCourse> AttendedCourses { get; set; }
    }
}
