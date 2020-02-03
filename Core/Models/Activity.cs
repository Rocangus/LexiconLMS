using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(350)]
        public string Description { get; set; }
        [Display(Name = "Start Time")]
        [Range(typeof(DateTime), "2020-01-01", "2100-01-01")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        [Range(typeof(DateTime), "2020-01-01", "2100-01-01")]
        public DateTime EndTime { get; set; }
        //public List<Document> Documents { get; set; }
        public int ModuleId { get; set; }
    }
}
