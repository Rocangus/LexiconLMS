using LexiconLMS.Core.Models;
using LexiconLMS.Core.Repository;
using LexiconLMS.Core.Services;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
    public class ModulesViewComponent : ViewComponent
    {
        private ApplicationDbContext _context { get; }
        private ICourseRepository _courseRepository { get; }
        public ModulesViewComponent(ApplicationDbContext context, ICourseRepository courseRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int courseId)
        {
            return View(await _courseRepository.GetAllCourseModulesAsync(courseId));
        }
    }
    
    public class ActivitiesViewComponent : ViewComponent
    {
        private ApplicationDbContext _context { get; }
        private ICourseRepository _courseRepository { get; }
        public ActivitiesViewComponent(ApplicationDbContext context, ICourseRepository courseRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int moduleId)
        {
            return View(await _courseRepository.GetAllModuleActivitiesAsync(moduleId));
        }
    }

    
    public class CourseViewComponent : ViewComponent
    {
        private ApplicationDbContext _context { get; }
        private ICourseRepository _courseRepository { get; }
        public CourseViewComponent(ApplicationDbContext context, ICourseRepository courseRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Course course)
        {
            return View(course);
        }
    }

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
            return View(await _userService.GetUsersViewModelAsync());
        }
    }
}
