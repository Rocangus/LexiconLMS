using LexiconLMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public List<Module> Modules { get; set; }
        public Module Module { get; set; }

    }
}
