using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTSWebApi.Models;

namespace PTSWebApi.Controllers
{
    public class PtsTrialTypesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTrialTypesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTrialTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsTrialType.ToListAsync());
        }

        // GET: PtsTrialTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialType = await _context.PtsTrialType
                .FirstOrDefaultAsync(m => m.TrialTypeId == id);
            if (ptsTrialType == null)
            {
                return NotFound();
            }

            return View(ptsTrialType);
        }

        // GET: PtsTrialTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsTrialTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrialTypeId,TrialTypeName,TrialTypeDescription")] PtsTrialType ptsTrialType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTrialType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsTrialType);
        }

        // GET: PtsTrialTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialType = await _context.PtsTrialType.FindAsync(id);
            if (ptsTrialType == null)
            {
                return NotFound();
            }
            return View(ptsTrialType);
        }

        // POST: PtsTrialTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrialTypeId,TrialTypeName,TrialTypeDescription")] PtsTrialType ptsTrialType)
        {
            if (id != ptsTrialType.TrialTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTrialType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTrialTypeExists(ptsTrialType.TrialTypeId))
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
            return View(ptsTrialType);
        }

        // GET: PtsTrialTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialType = await _context.PtsTrialType
                .FirstOrDefaultAsync(m => m.TrialTypeId == id);
            if (ptsTrialType == null)
            {
                return NotFound();
            }

            return View(ptsTrialType);
        }

        // POST: PtsTrialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTrialType = await _context.PtsTrialType.FindAsync(id);
            _context.PtsTrialType.Remove(ptsTrialType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTrialTypeExists(int id)
        {
            return _context.PtsTrialType.Any(e => e.TrialTypeId == id);
        }
    }
}
