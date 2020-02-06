using LexiconLMS.Core.Models.Documents;
﻿using LexiconLMS.Services.Validation;
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
        [DateRange(first: new int[] { 2020, 1, 1 }, last: new int[] { 2100, 1, 1 })]
        public DateTime StartDate { get; set; }
        [MaxLength(350)]
        public string Description { get; set; }
        public List<Module> Modules { get; set; }

        public List<DocumentsCourses> Documents { get; set; }

        public ICollection<SystemUserCourse> Members { get; set; }
    }
}
