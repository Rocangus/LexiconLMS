using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class SystemUserCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string SystemUserId { get; set; }

        public Course Course { get; set; }
        public SystemUser SystemUser { get; set; }

        public string Discriminator { get; set; }
    }
}
