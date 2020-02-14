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
        public async Task<IViewComponentResult> InvokeAsync(List<Models.Documents.Document> documents, ClaimsPrincipal user, string entityId, string entityType)
        {
            var userId = _userService.GetUserId(user);

            switch (entityType)
            {
                case "Activity":
                    return ActivityDocumentUpload(documents, userId, int.Parse(entityId));
                case "Assignment":
                    return AssignmentDocumentUpload(userId, int.Parse(entityId));
                case "Module":
                    return ModuleDocumentUpload(documents, userId, int.Parse(entityId));
                default:
                    break;
            }

            if (!entityId.Equals(_userService.GetUserId(user)))
            { 
                return View();
            }

            return View("User", new UserDocumentUploadViewModel { UserId = userId });
        }

        private IViewComponentResult ActivityDocumentUpload(List<Models.Documents.Document> documents, string userId, int entityId)
        {
            var user = UserClaimsPrincipal;
            return View("Activity", new ActivityDocumentUploadViewModel {User = user, Documents = documents, ActivityId = entityId, UserId = userId });
        }

        private IViewComponentResult AssignmentDocumentUpload(string userId, int entityId)
        {
            return View("Assignment", new AssignmentDocumentUploadViewModel { ActivityId = entityId, UserId = userId });
        }

        private IViewComponentResult ModuleDocumentUpload(List<Models.Documents.Document> documents, string userId, int entityId)
        {
            var user = UserClaimsPrincipal;
            return View("Module", new ModuleDocumentUploadViewModel { User = user, Documents = documents, ModuleId = entityId, UserId = userId });
        }
    }
}
