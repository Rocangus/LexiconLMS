using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models.Documents
{
    public class DocumentsAssignments
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
