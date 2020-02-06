using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LexiconLMS.Core.Services;
using LexiconLMS.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LexiconLMS.Controllers
{
    public class DocumentController : Controller
    {
        private IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult UploadUserDocument(string userId)
        {
            return View(new UserDocumentUploadViewModel { UserId = userId });
        }


        public async Task<IActionResult> UploadUserDocument(IFormFile formFile, string userId)
        {
            var result = await _documentService.SaveUserDocumentToFile(formFile, userId);
            return RedirectToAction(@"Details", "SystemUser", new { Id = userId });
        }
    }
}