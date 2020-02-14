using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Core.ViewModels
{
    public class ModuleDocumentUploadViewModel
    {
        public int ModuleId { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Upload Document")]
        public IFormFile FormFile { get; set ; }
    }
}
