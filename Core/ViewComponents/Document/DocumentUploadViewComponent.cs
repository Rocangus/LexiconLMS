using LexiconLMS.Core.Services;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents.Document
{
    public class DocumentUploadViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public DocumentUploadViewComponent(ApplicationDbContext applicationDbContext, IUserService userService)
        {
            _context = applicationDbContext;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal user, string entityId, string entityType)
        {
            var userId = _userService.GetUserId(user);

            switch (entityType)
            {
                case "Activity":
                    return ActivityDocumentUpload(userId, int.Parse(entityId));
            }

            return View(new UserDocumentUploadViewModel { UserId = userId });
        }

        private IViewComponentResult ActivityDocumentUpload(string userId, int entityId)
        {
            return View("Activity", new ActivityDocumentUploadViewModel { ActivityId = entityId, UserId = userId });
        }
    }
}
