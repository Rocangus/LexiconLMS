using LexiconLMS.Core.Models;
using LexiconLMS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Services
{
    public interface IUserService
    {
        Task<SystemUser> GetUserAsync(string id);
        Task<MainViewModel> GetUserMainViewModel(string id);
        Task<IEnumerable<SystemUser>> GetUsersAsync();
        Task<IEnumerable<SystemUserViewModel>> GetUsersViewModelAsync();
        Task<IEnumerable<SystemUserViewModel>> Filter(string name);
    }
}
