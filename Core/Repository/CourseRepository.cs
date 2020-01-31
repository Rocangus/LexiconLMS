using LexiconLMS.Core.Models;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private ApplicationDbContext _context { get; }

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddModule(Module module)
        {
            throw new NotImplementedException();
        }
        
        /*
        public async Task<List<Module>> GetAllCourseModulesAsync(int courseid)
        {
            return await _context.Courses
                .Where(c => c.Id == courseid)
                .Select(m => m.Modules);
        }
        */
        public void RemoveModule(Module module)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseViewModel> GetCourseViewModel(int? id)
        {
            var model = new CourseViewModel();

            model.Course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model.Course == null)
            {
                return NotFound();
            }

            model.Modules = await _context.Modules.Where(m => m.CourseId == id).ToListAsync();
            var userCourses = await _context.UserCourses.Include(uc => uc.SystemUser).Where(uc => uc.CourseId == id).ToListAsync();
            
            List<SystemUserViewModel> userViewModels = new List<SystemUserViewModel>();
            foreach(var item in userCourses)
            {
                var user = item.SystemUser;
                var mv = new SystemUserViewModel
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };

                userViewModels.Add(mv);
            }

            model.SystemUsers = userViewModels;

            return model;
        }

        private CourseViewModel NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
