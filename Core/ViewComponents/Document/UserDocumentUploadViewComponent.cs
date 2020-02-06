using LexiconLMS.Core.Services;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents.Document
{
    public class UserDocumentUploadViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public UserDocumentUploadViewComponent(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            return View(new UserDocumentUploadViewModel { UserId = userId });
        }
    }
}
