using LexiconLMS.Core.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    interface IDocumentService
    {
        Task<IEnumerable<Document>> GetCourseDocumentsAsync(int id);
        Task<IEnumerable<Document>> GetModuleDocumentsAsync(int id);
        Task<IEnumerable<Document>> GetActivityDocumentsAsync(int id);
        Task<IEnumerable<Document>> GetAssignmentDocumentsAsync(int id);
        Task<Document> GetUserAssignmenDocumentAsync(string id);



    }
}
