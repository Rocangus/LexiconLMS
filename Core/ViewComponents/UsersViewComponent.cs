using LexiconLMS.Core.Services;
using LexiconLMS.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
    public class UsersViewComponent : ViewComponent
    {
        private IUserService _userService;

        public UsersViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _userService.GetUsersViewModelAsync());
        }
    }
}
