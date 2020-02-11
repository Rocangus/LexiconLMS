﻿using LexiconLMS.Core.Models.Documents;
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

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> RemoveDocument(DocumentViewModel model)
        {
            var document = await _context.Documents.Where(d => d.Id == model.DocumentId).FirstOrDefaultAsync();
            if (document == null)
            {
                throw new ArgumentException($"No document with Id {model.DocumentId} could be found.");
            }

            var documentActivity = await _context.DocumentsActivities.FirstOrDefaultAsync(da => da.DocumentId == model.DocumentId && da.ActivityId == model.EntityId);
            if (documentActivity != null) 
            {
                return await RemoveActivityDocument(document, documentActivity);
            }

            _context.Documents.Remove(document);
            var fileResult = _documentIOService.RemoveDocument(document.Path);
            if (fileResult)
            {
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        private async Task<bool> RemoveActivityDocument(Document document, DocumentsActivities documentsActivities)
        {
            var success = _documentIOService.RemoveDocument(document.Path);

            if (success)
            {
                var documentActivity = await _context.DocumentsActivities.FirstOrDefaultAsync(da => da.DocumentId == document.Id);
                _context.DocumentsActivities.Remove(documentActivity);
                _context.Documents.Remove(document);
                return true;
            }
            return false;
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



        
        public async Task<bool> SaveCourseDocumentToFile(IFormFile formFile, string userId, int courseId)
        {

            string path = await _documentIOService.SaveCourseDocumentAsync(formFile, courseId);

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
