using Microsoft.AspNetCore.Http;

namespace LexiconLMS.Core.ViewModels
{
    public class ModuleDocumentUploadViewModel
    {
        public int ModuleId { get; set; }
        public string UserId { get; set; }
        public IFormFile FormFile { get; set ; }
    }
}
