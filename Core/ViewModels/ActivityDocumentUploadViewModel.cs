using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class ActivityDocumentUploadViewModel
    {
        public string UserId { get; set; }
        public int ActivityId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
