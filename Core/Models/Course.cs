using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        [Range(typeof(DateTime), "2020-01-01", "2100-01-01")]
        public DateTime StartDate { get; set; }
        [MaxLength(350)]
        public string Description { get; set; }
        public List<Module> Modules { get; set; }

        public ICollection<SystemUserCourse> Members { get; set; }
    }
}
