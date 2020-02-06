using LexiconLMS.Core.Models.Documents;
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
        Task<Document> GetUserAssignmenDocumentAsync(string id);

        Task<bool> SaveUserDocumentToFile(IFormFile formFile, string id);

    }
}
