using LexiconLMS.Core.Models;
using LexiconLMS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Repository
{
    public interface ICourseRepository
    {
        void AddModule(Module module);
        Task<IEnumerable<Module>> GetAllCourseModulesAsync(int courseid);
        Task<IEnumerable<Activity>> GetAllModuleActivitiesAsync(int moduleId);
        void RemoveModule(Module module);

        Task<CourseViewModel> GetCourseViewModel(int? id);
        Task<Course> GetUserCourse(string id);
        Task<ModuleViewModel> GetModuleViewModel(int? id);
        Task<Activity> GetActivity(int? id);

    }
}
