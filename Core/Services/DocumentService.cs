using LexiconLMS.Core.Models.Documents;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public class DocumentService: IDocumentService
    {
        private readonly ApplicationDbContext _context;

        private readonly IDocumentIOService _documentIOService;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(ApplicationDbContext context,
            IDocumentIOService documentIOService,
            ILogger<DocumentService> logger)
        {
            _context = context;
            _documentIOService = documentIOService;
            _logger = logger;
        }

        public Task<IEnumerable<Document>> GetActivityDocumentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Document>> GetAssignmentDocumentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Document>> GetCourseDocumentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Document>> GetModuleDocumentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Document> GetUserAssignmenDocumentAsync(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SaveActivityDocumentToFile(ActivityDocumentUploadViewModel model)
        {
            string path = await _documentIOService.SaveActivityDocumentAsync(model.FormFile, model.ActivityId);

            if (path.Equals(string.Empty))
                return false;

            var document = CreateDocument(model.FormFile, model.UserId, path);

            if (!await SaveDocument(document))
                return false;

            var documentId = await _context.Documents.Where(d => d.Path.Equals(path)).Select(d => d.Id).FirstOrDefaultAsync();

            DocumentsActivities documentsActivities = new DocumentsActivities
            {
                ActivityId = model.ActivityId,
                DocumentId = documentId
            };
            return await SaveDocumentActivity(documentsActivities);
        }

        private async Task<bool> SaveDocumentActivity(DocumentsActivities documentsActivities)
        {
            _context.DocumentsActivities.Add(documentsActivities);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException due)
            {
                LogException(due);
                return false;
            }
            return true;
        }

        private void LogException(DbUpdateException due)
        {
            _logger.LogWarning("Failed to save document to database: " + due.InnerException);
            _logger.LogTrace(due.StackTrace);
        }
        
        public async Task<bool> SaveUserDocumentToFile(IFormFile formFile, string userId)
        {

            string path = await _documentIOService.SaveUserDocumentAsync(formFile, userId);

            if (path.Equals(string.Empty))
                return false;

            var document = CreateDocument(formFile, userId, path);

            return await SaveDocument(document);
        }

        private static Document CreateDocument(IFormFile formFile, string userId, string path)
        {
            return new Document
            {
                Path = path,
                Name = formFile.FileName,
                SystemUserId = userId,
                UploadTime = DateTime.UtcNow,
                Description = string.Empty
            };
        }

        private async Task<bool> SaveDocument(Document document)
        {
            _context.Add(document);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException due)
            {
                LogException(due);
                return false;
            }
            return true;
        }



        
        public async Task<bool> SaveCourseDocumentToFile(IFormFile formFile, string  userId)
        {

            string path = await _documentIOService.SaveCourseDocumentAsync(formFile, userId);

            if (path.Equals(string.Empty))
                return false;
            var document = new Document
            {
                Path = path,
                Name = formFile.FileName,
                SystemUserId = userId,
                UploadTime = DateTime.UtcNow,
                Description = string.Empty
            };

            _context.Add(document);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException due)
            {
                _logger.LogWarning("Failed to save document to database: " + due.InnerException);
                _logger.LogTrace(due.StackTrace);
                return false;
            }
            return true;
        }


    }
}
