using LexiconLMS.Core.Models;
using LexiconLMS.Core.Repository;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
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
}
