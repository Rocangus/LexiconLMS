using LexiconLMS.Core.Repository;
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
}
