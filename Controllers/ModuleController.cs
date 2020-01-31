﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LexiconLMS.Core.Models;
using LexiconLMS.Data;
using LexiconLMS.Core.Repository;
using LexiconLMS.Core.ViewModels;

namespace LexiconLMS.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourseRepository _courseRepository;

        public ModuleController(ApplicationDbContext context)
        {
            _context = context;
            _courseRepository = new CourseRepository(_context);
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _courseRepository.GetModuleViewModel(id));
        }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> CreateActivity([Bind("Id, Name, StartTime, EndTime, CourseId")] Activity activity)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(activity);
                 await _context.SaveChangesAsync();
             }
                 return RedirectToAction("Edit", activity.ModuleId);
         }
         
        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _courseRepository.GetModuleViewModel(id));
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,Description")] Module module)
        {
            if (id != module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", module.Id);
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _courseRepository.GetModuleViewModel(id));
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Courses", new { id = module.CourseId });
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }

        public ActionResult ModalPopUp()
        {
            return View();
        }
    }
}
