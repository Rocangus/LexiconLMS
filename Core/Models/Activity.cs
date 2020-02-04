using LexiconLMS.Core.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Document> Documents { get; set; }
        public int ModuleId { get; set; }
    }
}
