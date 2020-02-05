using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models.Documents
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description{get;set;}
        public string UserId { get; set; }
        public DateTime UploadTime { get; set; }

    }
}
