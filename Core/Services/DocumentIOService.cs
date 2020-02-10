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
        private ILogger<DocumentIOService> _logger;

        public DocumentIOService(ILogger<DocumentIOService> logger)
        {
            _logger = logger;
        }

        public async Task<string> SaveActivityDocumentAsync(IFormFile formFile, int activityId)
        {
            string path = Environment.CurrentDirectory + @"\Data\Activity\" + activityId + @"\" + Path.GetRandomFileName();

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            try
            {
                using (var stream = File.Create(path))
                {
                    await SaveFileToDisk(formFile, path, stream);
                    return path;
                }
            }
            catch (Exception e)
            {
                LogError(e);
                return string.Empty;
            }
        }

        public async Task<string> SaveCourseDocumentAsync(IFormFile formFile, int courseId)
        {
            string path = Environment.CurrentDirectory + @"Data\Course\" + courseId + @"\" + Path.GetRandomFileName();

            try
            {
                using (var stream = File.Create(path))
                {
                    await SaveFileToDisk(formFile, path, stream);
                    return path;
                }
            }
            catch(Exception e)
            {
                LogError(e);
                return string.Empty;
            }
        }

        public async Task<string> SaveUserDocumentAsync(IFormFile formFile, string userId)
        {
            string path = Environment.CurrentDirectory + @"\Data\User\" + userId + @"\" + Path.GetRandomFileName();

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            try
            {
                using (var stream = File.Create(path))
                {
                    await SaveFileToDisk(formFile, path, stream);
                    return path;
                }
            }
            catch (Exception e)
            {
                LogError(e);
                return string.Empty;
            }
        }

        private async Task SaveFileToDisk(IFormFile formFile, string path, FileStream stream)
        {
            await formFile.CopyToAsync(stream);
            _logger.LogInformation($"Successfully wrote file to disk at {path}");
        }

        private void LogError(Exception e)
        {
            _logger.LogWarning($"Failed to write file to disk: " + e.Message);
            _logger.LogTrace(e.StackTrace);
        }

        public bool RemoveDocument(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                LogError(e);
                return false;
            }
        }
  


    }
}
