using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LexiconLMS.Models;
using Microsoft.AspNetCore.Authorization;
using LexiconLMS.Data;
using LexiconLMS.Core.Models;
using Microsoft.AspNetCore.Identity;
using LexiconLMS.Core.Services;
using LexiconLMS.Core.ViewModels;

namespace LexiconLMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SystemUser> _userManager;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<SystemUser> userManager, IUserService userService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            SystemUser systemUser = await _userManager.GetUserAsync(User);
            return View(await _userService.GetUserMainViewModel(systemUser.Id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ModuleDetails(int id)
        {
            return ViewComponent("ModuleDetails", new { moduleId = id });
        }
        public IActionResult ActivityDetails(int id)
        {
            return ViewComponent("ActivityDetails", new { activityId = id });
        }
    }
}
