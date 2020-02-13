using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LexiconLMS.Core.Services;
using LexiconLMS.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ActivityDocumentUpload(ActivityDocumentUploadViewModel model)
        {
            var success = await _documentService.SaveActivityDocumentToFile(model);
            if (success)
                return RedirectToAction(@"Details", "Activity", new { Id = model.ActivityId });
            else
                return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AssignmentDocumentUpload(AssignmentDocumentUploadViewModel model)
        {
            var success = await _documentService.SaveAssignmentDocumentToFile(model);
            if (success)
                return RedirectToAction(@"Details", "Activity", new { Id = model.ActivityId });
            else
                return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ModuleDocumentUpload(ModuleDocumentUploadViewModel model)
        {
            var success = await _documentService.SaveModuleDocumentToFile(model);
            if (success)
                return RedirectToAction(@"Details", "Module", new { Id = model.ModuleId });
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult UploadUserDocument(string userId)
        {
            return View(new UserDocumentUploadViewModel { UserId = userId });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadDocument(int documentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);
            if (document != null)
            {
                using (FileStream stream = new FileStream(document.Path, FileMode.Open))
                {
                    var memory = new MemoryStream();
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    var ext = Path.GetExtension(document.Name).ToLowerInvariant();
                    return File(memory, GetMimeTypes()[ext], document.Name);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // Keeps track of some common expected file types.
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".rtf", "application/rtf" },
                {".zip", "application/zip" }
            };
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadUserDocument(UserDocumentUploadViewModel model)
        {
            var success = await _documentService.SaveUserDocumentToFile(model.FormFile, model.UserId);
            if (success)
                return RedirectToAction(@"Details", "SystemUser", new { Id = model.UserId });
            else
                return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public IActionResult UploadCourseDocument(string userId)
        {
            return View(new UserDocumentUploadViewModel { UserId = userId });
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadCourseDocument(IFormFile formFile, string userId, int courseId)
        {
            var result = await _documentService.SaveCourseDocumentToFile(formFile, userId, courseId);
            return RedirectToAction(@"Details", "Courses", new { Id = courseId });
        }

        // Should be accessible by everyone, but only from own documents unless teacher.
        [HttpGet]
        public async Task<IActionResult> RemoveDocument(int documentId, int entityId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);

            if (document == null)
                return NotFound();

            string adjustedPath = Path.GetDirectoryName(document.Path) + document.Name;

            var viewModel = new DocumentViewModel
            {
                EntityId = entityId,
                DocumentId = document.Id,
                ModelPath = adjustedPath,
                UploadDate = document.UploadTime
            };

            return View(viewModel);
        }

        // Should be accessible by everyone, but only from own documents unless teacher.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveDocument(DocumentViewModel viewModel)
        {
            await _documentService.RemoveDocument(viewModel);

            return RedirectToAction("Index", "Home");
        }
    }
}