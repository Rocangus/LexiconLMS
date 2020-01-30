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
        //Task<List<Module>> GetAllCourseModulesAsync(int courseid);
        void RemoveModule(Module module);

        Task<CourseViewModel> GetCourseViewModel(int? id);

    }
}
