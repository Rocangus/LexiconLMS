using LexiconLMS.Core.Models.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LexiconLMS.Services.Validation;
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
        [DateRange(first: new int[] { 2020, 1, 1 }, last: new int[] { 2100, 1, 1 })]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        [DateRange(first: new int[] { 2020, 1, 1 }, last: new int[] { 2100, 1, 1 })]
        public DateTime EndTime { get; set; }
        public List<Document> Documents { get; set; }
        public int ModuleId { get; set; }
    }
}
