using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public interface IDocumentIOService
    {
        Task<bool> SaveCourseDocument(IFormFile formFile, int courseId);

        Task<string> SaveUserDocumentAsync(IFormFile formFile, string userId);
    }
}
