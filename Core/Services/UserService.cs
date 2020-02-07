using LexiconLMS.Core.Models;
using LexiconLMS.Core.Repository;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context { get; }
        private ICourseRepository _courseRepository { get;  }
        public UserService(ApplicationDbContext context, ICourseRepository courseRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
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

        public async Task<MainViewModel> GetUserMainViewModel(string id)
        {
            Course course = await _courseRepository.GetUserCourse(id);

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
    }
}
