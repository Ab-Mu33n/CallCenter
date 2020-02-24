using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallCenter.Models;

namespace CallCenter.Areas.Individual.Controllers
{
    [Area("Individual")]
    public class IndividualsController : Controller
    {
        private readonly CallCenterContext _context;

        public IndividualsController(CallCenterContext context)
        {
            _context = context;
        }

        // GET: Individual/Individuals
        public async Task<IActionResult> Index()
        {
            var callCenterContext = _context.Individuals.Include(i => i.Gender);
            return View(await callCenterContext.ToListAsync());
        }

        // GET: Individual/Individuals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individuals = await _context.Individuals
                .Include(i => i.Gender)
                .FirstOrDefaultAsync(m => m.IndividualId == id);
            if (individuals == null)
            {
                return NotFound();
            }

            return View(individuals);
        }

        // GET: Individual/Individuals/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Gender1");
            return View();
        }

        // POST: Individual/Individuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IndividualId,Nicnumber,PassportNumber,FullName,DateofBirth,GenderId,Address")] Individuals individuals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(individuals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Gender1", individuals.GenderId);
            return View(individuals);
        }

        // GET: Individual/Individuals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individuals = await _context.Individuals.FindAsync(id);
            if (individuals == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Gender1", individuals.GenderId);
            return View(individuals);
        }

        // POST: Individual/Individuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IndividualId,Nicnumber,PassportNumber,FullName,DateofBirth,GenderId,Address")] Individuals individuals)
        {
            if (id != individuals.IndividualId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(individuals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndividualsExists(individuals.IndividualId))
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Gender1", individuals.GenderId);
            return View(individuals);
        }

        // GET: Individual/Individuals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individuals = await _context.Individuals
                .Include(i => i.Gender)
                .FirstOrDefaultAsync(m => m.IndividualId == id);
            if (individuals == null)
            {
                return NotFound();
            }

            return View(individuals);
        }

        // POST: Individual/Individuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var individuals = await _context.Individuals.FindAsync(id);
            _context.Individuals.Remove(individuals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndividualsExists(int id)
        {
            return _context.Individuals.Any(e => e.IndividualId == id);
        }
    }
}
