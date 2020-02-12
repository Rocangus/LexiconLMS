using LexiconLMS.Core.Repository;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
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
}
