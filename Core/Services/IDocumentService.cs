using LexiconLMS.Core.Models.Documents;
using LexiconLMS.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetCourseDocumentsAsync(int id);
        Task<IEnumerable<Document>> GetModuleDocumentsAsync(int id);
        Task<IEnumerable<Document>> GetActivityDocumentsAsync(int id);
        Task<IEnumerable<Document>> GetAssignmentDocumentsAsync(int id);
        Task<List<Document>> GetUserDocumentsAsync(string id);
        Task<Document> GetUserAssignmenDocumentAsync(string id);
        Task<Document> GetDocumentByIdAsync(int id);
        Task<bool> RemoveDocument(DocumentViewModel model);
        Task<bool> SaveActivityDocumentToFile(ActivityDocumentUploadViewModel model);
        Task<bool> SaveAssignmentDocumentToFile(AssignmentDocumentUploadViewModel model);
        Task<bool> SaveModuleDocumentToFile(ModuleDocumentUploadViewModel model);
        Task<bool> SaveUserDocumentToFile(IFormFile formFile, string id);
        Task<bool> SaveCourseDocumentToFile(IFormFile formFile, string userId, int courseId);

    }
}
