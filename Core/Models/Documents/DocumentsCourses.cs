using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models.Documents
{
    public class DocumentsCourses
    {
        public Document Document { get; set; }
        public int DocumentId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
