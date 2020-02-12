using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class UserDocumentUploadViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "Upload Document")]
        public IFormFile FormFile { get; set; }
    }
}
