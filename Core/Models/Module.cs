using LexiconLMS.Core.Models.Documents;
﻿using LexiconLMS.Services.Validation;
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
        [DateRange(first: new int[] { 2020, 1, 1 }, last: new int[] { 2100, 1, 1 })]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DateRange(first: new int[] { 2020, 1, 1 }, last: new int[] { 2100, 1, 1 })]
        public DateTime EndDate { get; set; }
        public List<Activity> Activities { get; set; }
        public List<DocumentsModules> Documents { get; set; }
        public int CourseId { get; set; }
    }
}
