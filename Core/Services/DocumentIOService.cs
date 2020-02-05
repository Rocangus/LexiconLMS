using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LexiconLMS.Core.Services
{
    public class DocumentIOService : IDocumentIOService
    {
        private ILogger _logger;

        public DocumentIOService(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> SaveCourseDocument(IFormFile formFile, int courseId)
        {
            string path = Environment.CurrentDirectory + @"Data\Course\" + courseId + @"\" + Path.GetRandomFileName();

            try
            {
                using (var stream = File.Create(path))
                {
                    await formFile.CopyToAsync(stream);
                    _logger.LogInformation($"Successfully wrote file to disk at {path}");
                    return true;
                }
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Failed to write file to disk: " + e.Message);
                _logger.LogTrace(e.StackTrace);
                return false;
            }
        }
    }
}
