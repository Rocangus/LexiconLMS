using LexiconLMS.Core.Models;
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

    }
}
