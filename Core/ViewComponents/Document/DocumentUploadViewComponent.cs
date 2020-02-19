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
                case "Course":
                    return CourseDocumentUpload(userId, int.Parse(entityId));
                case "Assignment":
                    return AssignmentDocumentUpload(userId, int.Parse(entityId));
                case "Module":
                    return ModuleDocumentUpload(userId, int.Parse(entityId));
                default:
                    break;
            }

            if (!entityId.Equals(_userService.GetUserId(user)))
            { 
                return View();
            }

            return View("User", new UserDocumentUploadViewModel { UserId = userId });
        }

        private IViewComponentResult ActivityDocumentUpload(string userId, int entityId)
        {
            return View("Activity", new ActivityDocumentUploadViewModel { ActivityId = entityId, UserId = userId });

        }


        private IViewComponentResult CourseDocumentUpload(string userId, int entityId)
        {
            return View("Course", new CourseDocumentUploadViewModel { CourseId = entityId, UserId = userId });
        }
        private IViewComponentResult AssignmentDocumentUpload(string userId, int entityId)
        {
            return View("Assignment", new AssignmentDocumentUploadViewModel { ActivityId = entityId, UserId = userId });
        }

        private IViewComponentResult ModuleDocumentUpload(string userId, int entityId)
        {
            return View("Module", new ModuleDocumentUploadViewModel { ModuleId = entityId, UserId = userId });
        }
    }
}
