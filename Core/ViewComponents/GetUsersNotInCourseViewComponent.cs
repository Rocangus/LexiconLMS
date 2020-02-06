using LexiconLMS.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
    public class GetUsersNotInCourseViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public GetUsersNotInCourseViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int courseId)
        {
            return View(await _userService.GetSystemUsersNotInCourse(courseId));
        }
    }
}
