using LexiconLMS.Core.Repository;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
    public class ModuleDetailsViewComponent : ViewComponent
    {
        private ICourseRepository _courseRepository { get; }
        public ModuleDetailsViewComponent(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int moduleId)
        {
            return View(await _courseRepository.GetModuleViewModel(moduleId));
        }
    }
}
