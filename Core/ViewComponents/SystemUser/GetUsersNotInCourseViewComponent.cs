using LexiconLMS.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents.SystemUser
{
    public class GetUsersNotInCourseViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public GetUsersNotInCourseViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int courseId, bool mainPage)
        {
            if (mainPage)
            {
                return View(await _userService.GetSystemUsersNotInCourse(courseId));

            }
            return View("StandardEdit", await _userService.GetSystemUsersNotInCourse(courseId));
        }
    }
}
