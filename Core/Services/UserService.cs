using LexiconLMS.Core.Models;
using LexiconLMS.Core.ViewModels;
using LexiconLMS.Data;
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
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SystemUser> GetUserAsync(string? id)
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

            //foreach (var user in users)
            //{
            //    var vm = new SystemUserViewModel
            //    {
            //        Name = user.Name,
            //        Email = user.Email,
            //        Id = user.Id,
            //        PhoneNumber = user.PhoneNumber
            //    };
            //}
        }

    }
}
