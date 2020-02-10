using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public interface IDocumentIOService
    {
        Task<string> SaveActivityDocumentAsync(IFormFile formFile, int activityId);

        Task<string> SaveUserDocumentAsync(IFormFile formFile, string userId);

        Task<string> SaveCourseDocumentAsync(IFormFile formFile, int courseId);

        bool RemoveDocument(string path);
    }
}
