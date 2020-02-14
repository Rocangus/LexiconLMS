using LexiconLMS.Core.Models.Documents;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class ActivityDocumentUploadViewModel
    {
        public List<Document> Documents { get; set; }
        public ClaimsPrincipal User { get; set; }
        public string UserId { get; set; }
        public int ActivityId { get; set; }

        [Display(Name = "Upload Document")]
        public IFormFile FormFile { get; set; }
    }
}
