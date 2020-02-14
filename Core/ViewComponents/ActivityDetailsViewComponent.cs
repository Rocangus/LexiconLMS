using LexiconLMS.Core.Repository;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewComponents
{
    public class ActivityDetailsViewComponent : ViewComponent
    {
        private ICourseRepository _courseRepository { get; }
        public ActivityDetailsViewComponent(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int activityId)
        {
            return View(await _courseRepository.GetActivityViewModel(activityId));
        }
    }
}
