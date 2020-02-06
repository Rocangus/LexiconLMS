using LexiconLMS.Core.Models.Documents;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public DocumentService(ApplicationDbContext context,
            IDocumentIOService documentIOService)
        {
            _context = context;
            _documentIOService = documentIOService;
        }


        public async Task<IEnumerable<Document>> GetCourseDocumentsAsync(int id)
        {
            return await _context.Documents.Where(c => c.CourseId == id).ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetModuleDocumentsAsync(int id)
        {
            return await _context.Documents.Where(c => c.ModuleId == id).ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetActivityDocumentsAsync(int id)
        {
            return await _context.Documents.Where(c => c.ActivityId == id).ToListAsync();
        }

        public Task<IEnumerable<Document>> GetAssignmentDocumentsAsync(int id)
        {
            return GetActivityDocumentsAsync(id);
        }

        public Task<Document> GetUserAssignmenDocumentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveUserDocumentToFile(IFormFile formFile, string userId)
        {
     
            string fileSave= await _documentIOService.SaveUserDocumentAsync(formFile, userId);
            //if()
            return true;

        }
    }
}
