using LexiconLMS.Core.Services;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
    public class UsersViewComponent : ViewComponent
    {
        private ApplicationDbContext _context { get; }
        private IUserService _userService { get; }

        public UsersViewComponent(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int courseId)
        {
            //TODO Make get Parvin's function for getting users in a course.
            return View(_userService.GetSystemUserViewModels(courseId));
        }
    }
}
