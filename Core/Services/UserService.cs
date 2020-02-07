using LexiconLMS.Core.Models;
using LexiconLMS.Core.Repository;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context { get; }
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SystemUser> GetUserAsync(string id)
        {
            return await _context.SystemUsers.Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SystemUser>> GetUsersAsync()
        {
            return await _context.SystemUsers.ToListAsync();
        }

        public async Task<IEnumerable<SystemUserViewModel>> GetUsersViewModelAsync()
        {
            return await _context.SystemUsers.Select(user => new SystemUserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            }).ToListAsync();
        }

        public async Task<IEnumerable<SystemUserViewModel>> Filter(string userName)
        {
            return await _context.SystemUsers.Where(user => user.Name.ToLower().Contains(userName.ToLower())).Select(user => new SystemUserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            }).ToListAsync();
        }

        //Used for ViewComponent
        public async Task<Course> GetUserCourse(string id)
        {
            SystemUserCourse course = await _context.UserCourses.Where(u => u.SystemUserId.Equals(id)).FirstOrDefaultAsync();
            return await _context.Courses.Where(u => u.Id.Equals(course.CourseId)).FirstOrDefaultAsync();
        }

        public async Task<MainViewModel> GetUserMainViewModel(string id)
        {
            Course course = await GetUserCourse(id);

            return await _context.SystemUsers.Where(user => user.Id == id).Select(user => new MainViewModel
            {
                SystemUser = new SystemUserViewModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber
                },
                Course = course
            }).SingleOrDefaultAsync();
        }

        public List<SystemUserViewModel> GetSystemUserViewModels(int? courseId)
        {
            var userCourses = _context.UserCourses.Include(uc => uc.SystemUser).Where(uc => uc.CourseId == courseId).ToList();
            return GetSystemUserViewModels(courseId, userCourses);
        }

        public async Task<List<SystemUserViewModel>> GetSystemUsersNotInCourse(int courseId)
        {
            var userIdsInCourse = await _context.UserCourses.Where(uc => uc.CourseId == courseId).Select(uc => uc.SystemUserId).ToListAsync();
            var usersNotInCourse = await _context.Users.Where(u => !userIdsInCourse.Contains(u.Id)).ToListAsync();
            return GetSystemUserViewModels(courseId, usersNotInCourse);

        }

        private static List<SystemUserViewModel> GetSystemUserViewModels(int? courseId, List<SystemUserCourse> userCourses)
        {
            List<SystemUserViewModel> userViewModels = new List<SystemUserViewModel>();
            foreach (var item in userCourses)
            {
                var user = item.SystemUser;
                SystemUserViewModel mv = PopulateSystemUserViewModel((int)courseId, user);

                userViewModels.Add(mv);
            }

            return userViewModels;
        }
        
        private static List<SystemUserViewModel> GetSystemUserViewModels(int courseId, List<SystemUser> users)
        {
            List<SystemUserViewModel> userViewModels = new List<SystemUserViewModel>();
            foreach (var user in users)
            {
                SystemUserViewModel mv = PopulateSystemUserViewModel(courseId, user);

                userViewModels.Add(mv);
            }

            return userViewModels;
        }

        private static SystemUserViewModel PopulateSystemUserViewModel(int courseId, SystemUser user)
        {
            return new SystemUserViewModel
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                CourseId = (int)courseId
            };
        }
    }
}
