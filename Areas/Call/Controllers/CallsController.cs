using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallCenter.Models;

namespace CallCenter.Areas.Call.Controllers
{
    [Area("Call")]
    public class CallsController : Controller
    {
        private readonly CallCenterContext _context;

        public CallsController(CallCenterContext context)
        {
            _context = context;
        }

        // GET: Call/Calls
        public async Task<IActionResult> Index()
        {
            var callCenterContext = _context.Calls.Include(c => c.CallAttendee).Include(c=>c.CallAttendee.Individual).Include(c => c.CallState).Include(c => c.CallerIndividual).Include(c=>c.CallDetails);
            return View(await callCenterContext.ToListAsync());
        }

        // GET: Call/Calls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calls = await _context.Calls
                .Include(c => c.CallAttendee)
                .Include(c => c.CallState)
                .Include(c => c.CallerIndividual)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (calls == null)
            {
                return NotFound();
            }

            return View(calls);
        }

        // GET: Call/Calls/Create
        public IActionResult Create()
        {
            ViewData["CallAttendeeId"] = new SelectList(_context.Staffs, "StaffId", "StaffId");
            ViewData["CallStateId"] = new SelectList(_context.States, "StateId", "Details");
            ViewData["CallerIndividualId"] = new SelectList(_context.Individuals, "IndividualId", "Address");
            return View();
        }

        // POST: Call/Calls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallId,CallerIndividualId,CallAttendeeId,CallStateId,Date")] Calls calls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CallAttendeeId"] = new SelectList(_context.Staffs, "StaffId", "StaffId", calls.CallAttendeeId);
            ViewData["CallStateId"] = new SelectList(_context.States, "StateId", "Details", calls.CallStateId);
            ViewData["CallerIndividualId"] = new SelectList(_context.Individuals, "IndividualId", "Address", calls.CallerIndividualId);
            return View(calls);
        }

        // GET: Call/Calls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calls = await _context.Calls.FindAsync(id);
            if (calls == null)
            {
                return NotFound();
            }
            ViewData["CallAttendeeId"] = new SelectList(_context.Staffs, "StaffId", "StaffId", calls.CallAttendeeId);
            ViewData["CallStateId"] = new SelectList(_context.States, "StateId", "Details", calls.CallStateId);
            ViewData["CallerIndividualId"] = new SelectList(_context.Individuals, "IndividualId", "Address", calls.CallerIndividualId);
            return View(calls);
        }

        // POST: Call/Calls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CallId,CallerIndividualId,CallAttendeeId,CallStateId,Date")] Calls calls)
        {
            if (id != calls.CallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallsExists(calls.CallId))
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
            ViewData["CallAttendeeId"] = new SelectList(_context.Staffs, "StaffId", "StaffId", calls.CallAttendeeId);
            ViewData["CallStateId"] = new SelectList(_context.States, "StateId", "Details", calls.CallStateId);
            ViewData["CallerIndividualId"] = new SelectList(_context.Individuals, "IndividualId", "Address", calls.CallerIndividualId);
            return View(calls);
        }

        // GET: Call/Calls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calls = await _context.Calls
                .Include(c => c.CallAttendee)
                .Include(c => c.CallState)
                .Include(c => c.CallerIndividual)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (calls == null)
            {
                return NotFound();
            }

            return View(calls);
        }

        // POST: Call/Calls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calls = await _context.Calls.FindAsync(id);
            _context.Calls.Remove(calls);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallsExists(int id)
        {
            return _context.Calls.Any(e => e.CallId == id);
        }
    }
}
