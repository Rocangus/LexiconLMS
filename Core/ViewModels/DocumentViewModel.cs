using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public int EntityId { get; set; }
        public string ModelPath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
