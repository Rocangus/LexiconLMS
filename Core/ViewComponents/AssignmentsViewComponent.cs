using LexiconLMS.Core.Services;
using LexiconLMS.Core.Models;
using LexiconLMS.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiconLMS.Data;

namespace LexiconLMS.Core.ViewComponents
{
    public class AssignmentsViewComponent : ViewComponent
    {
        private readonly IDocumentService _documentService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public AssignmentsViewComponent(IDocumentService documentService, IUserService userService, ApplicationDbContext context)
        {
            _documentService = documentService;
            _userService = userService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int activityId)
        {
            var documents = await _documentService.GetAssignmentDocumentsAsync(activityId);

            var assignmentViewModels = new List<AssignmentViewModel>();

            foreach(var document in documents)
            {
                var assignmentModel = new AssignmentViewModel { Document = document };
                assignmentModel.User = await _userService.GetSystemUserViewModelAsync(document.SystemUserId);
            }

            return View(documents);
        }
    }
}
