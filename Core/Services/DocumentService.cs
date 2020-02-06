using LexiconLMS.Core.Models.Documents;
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


        //public async Task<IEnumerable<Document>> GetCourseDocumentsAsync(int id)
        //{
        //    return await _context.Documents.Where(c => c.CourseId == id).ToListAsync();
        //}

        //public async Task<IEnumerable<Document>> GetModuleDocumentsAsync(int id)
        //{
        //    return await _context.Documents.Where(c => c.ModuleId == id).ToListAsync();
        //}

        //public async Task<IEnumerable<Document>> GetActivityDocumentsAsync(int id)
        //{
        //    return await _context.Documents.Where(c => c.ActivityId == id).ToListAsync();
        //}

        //public Task<IEnumerable<Document>> GetAssignmentDocumentsAsync(int id)
        //{
        //    return GetActivityDocumentsAsync(id);
        //}

        public Task<Document> GetUserAssignmenDocumentAsync(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SaveActivityDocumentToFile(IFormFile formFile, string activityId)
        {
            string path = await _documentIOService.SaveActivityDocumentAsync(formFile, activityId);

            if (path.Equals(string.Empty))
                return false;

            var document = new Document
            {
                Path = path,
                Name = formFile.FileName,
                SystemUserId = User
            };
        }

        public async Task<bool> SaveUserDocumentToFile(IFormFile formFile, string userId)
        {
     
            string path = await _documentIOService.SaveUserDocumentAsync(formFile, userId);
            
            if(path.Equals(string.Empty))
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
