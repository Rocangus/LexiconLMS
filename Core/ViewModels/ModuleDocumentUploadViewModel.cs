using LexiconLMS.Core.Models.Documents;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LexiconLMS.Core.ViewModels
{
    public class ModuleDocumentUploadViewModel
    {
        public List<Document> Documents { get; set; }
        public ClaimsPrincipal User { get; set; }
        public int ModuleId { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Upload Document")]
        public IFormFile FormFile { get; set ; }
    }
}
