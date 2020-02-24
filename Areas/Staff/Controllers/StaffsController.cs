using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallCenter.Models;

namespace CallCenter.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class StaffsController : Controller
    {
        private readonly CallCenterContext _context;

        public StaffsController(CallCenterContext context)
        {
            _context = context;
        }

        // GET: Staff/Staffs
        public async Task<IActionResult> Index()
        {
            var callCenterContext = _context.Staffs.Include(s => s.Designation).Include(s => s.Individual);
            return View(await callCenterContext.ToListAsync());
        }

        // GET: Staff/Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Designation)
                .Include(s => s.Individual)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // GET: Staff/Staffs/Create
        public IActionResult Create()
        {
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationName");
            ViewData["IndividualId"] = new SelectList(_context.Individuals, "IndividualId", "FullName");
            return View();
        }

        // POST: Staff/Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,IndividualId,DesignationId")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationName", staffs.DesignationId);
            ViewData["IndividualId"] = new SelectList(_context.Individuals, "IndividualId", "FullName", staffs.IndividualId);
            return View(staffs);
        }

        // GET: Staff/Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationName", staffs.DesignationId);
            ViewData["IndividualId"] = new SelectList(_context.Individuals, "IndividualId", "FullName", staffs.IndividualId);
            return View(staffs);
        }

        // POST: Staff/Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,IndividualId,DesignationId")] Staffs staffs)
        {
            if (id != staffs.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffsExists(staffs.StaffId))
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
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationDetails", staffs.DesignationId);
            ViewData["IndividualId"] = new SelectList(_context.Individuals, "IndividualId", "Address", staffs.IndividualId);
            return View(staffs);
        }

        // GET: Staff/Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Designation)
                .Include(s => s.Individual)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // POST: Staff/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffs = await _context.Staffs.FindAsync(id);
            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffsExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffId == id);
        }
    }
}
