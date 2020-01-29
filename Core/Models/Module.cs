using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class Module
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public List<Activity> Activities { get; set; }
        //public List<Document> Documents { get; set; }
        public string CourseID { get; set; }
        //public Course Course { get; set; }
    }
}
