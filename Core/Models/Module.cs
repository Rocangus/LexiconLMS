using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class Module
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(350)]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Range(typeof(DateTime), "2020-01-01", "2100-01-01")]
        public DateTime EndDate { get; set; }
        public List<Activity> Activities { get; set; }
        //public List<Document> Documents { get; set; }
        public int CourseId { get; set; }
    }
}
