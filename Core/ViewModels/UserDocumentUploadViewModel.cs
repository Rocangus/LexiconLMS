using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class UserDocumentUploadViewModel
    {
        public string UserId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
