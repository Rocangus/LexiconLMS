using LexiconLMS.Core.Models;
using LexiconLMS.Core.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class CourseViewModel
    {
        //public int Id { get; set; }
        public Course Course { get; set; }
        public List<DocumentsCourses> Documents { get; set; }
        public List<Module> Modules { get; set; }
        public Module Module { get; set; }

        public IEnumerable<SystemUserViewModel> SystemUsers { get; set; }
    }
}
