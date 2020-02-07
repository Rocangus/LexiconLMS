using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models.Documents
{
    public class DocumentsModules
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }
}
