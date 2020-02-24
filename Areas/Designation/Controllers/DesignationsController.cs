using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallCenter.Models;

namespace CallCenter.Areas.Designation.Controllers
{
    [Area("Designation")]
    public class DesignationsController : Controller
    {
        private readonly CallCenterContext _context;

        public DesignationsController(CallCenterContext context)
        {
            _context = context;
        }

        // GET: Designation/Designations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Designations.ToListAsync());
        }

        // GET: Designation/Designations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designations = await _context.Designations
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designations == null)
            {
                return NotFound();
            }

            return View(designations);
        }

        // GET: Designation/Designations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Designation/Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesignationId,DesignationName,DesignationDetails,IsActive")] Designations designations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designations);
        }

        // GET: Designation/Designations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designations = await _context.Designations.FindAsync(id);
            if (designations == null)
            {
                return NotFound();
            }
            return View(designations);
        }

        // POST: Designation/Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesignationId,DesignationName,DesignationDetails,IsActive")] Designations designations)
        {
            if (id != designations.DesignationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationsExists(designations.DesignationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(designations);
        }

        // GET: Designation/Designations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designations = await _context.Designations
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designations == null)
            {
                return NotFound();
            }

            return View(designations);
        }

        // POST: Designation/Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designations = await _context.Designations.FindAsync(id);
            _context.Designations.Remove(designations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignationsExists(int id)
        {
            return _context.Designations.Any(e => e.DesignationId == id);
        }
    }
}
