﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LexiconLMS.Core.Models;
using LexiconLMS.Data;
using LexiconLMS.Core.Services;
using LexiconLMS.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using LexiconLMS.Core.ViewModels;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class SystemUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;

        public SystemUserController(ApplicationDbContext context, IUserService userService,
            IDocumentService documentService)
        {
            _context = context;
            _userService = userService;
            _documentService = documentService;
        }

        // GET: SystemUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.SystemUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Documents = await _documentService.GetUserDocumentsAsync(user.Id);

            var viewModel = new SystemUserDetailsViewModel { SystemUser = user, SignedInUserId = _userService.GetUserId(User) };

            return View(viewModel);
        }

        // GET: SystemUsers/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: SystemUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name, PhoneNumber")] SystemUser user)
        {

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: SystemUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.SystemUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: SystemUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,PhoneNumber")] SystemUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemUserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: SystemUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.SystemUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: SystemUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.SystemUsers.FindAsync(id);
            _context.SystemUsers.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemUserExists(string id)
        {
            return _context.SystemUsers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUsersViewModelAsync());
        }

        public async Task<IActionResult> Filter(string userName)
        {
            if(String.IsNullOrEmpty(userName))
            {
                return View(nameof(Index), await _userService.GetUsersViewModelAsync());
            }
            return View(nameof(Index), await _userService.Filter(userName));
        }
        
        
    }
}
