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
    public class PtsTrialTypeDosageTypesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTrialTypeDosageTypesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTrialTypeDosageTypes
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTrialTypeDosageType.Include(p => p.DosageType).Include(p => p.TrialType);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTrialTypeDosageTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialTypeDosageType = await _context.PtsTrialTypeDosageType
                .Include(p => p.DosageType)
                .Include(p => p.TrialType)
                .FirstOrDefaultAsync(m => m.TrialTypeDosageTypeId == id);
            if (ptsTrialTypeDosageType == null)
            {
                return NotFound();
            }

            return View(ptsTrialTypeDosageType);
        }

        // GET: PtsTrialTypeDosageTypes/Create
        public IActionResult Create()
        {
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId");
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId");
            return View();
        }

        // POST: PtsTrialTypeDosageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrialTypeDosageTypeId,TrialTypeId,DosageTypeId")] PtsTrialTypeDosageType ptsTrialTypeDosageType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTrialTypeDosageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId", ptsTrialTypeDosageType.DosageTypeId);
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId", ptsTrialTypeDosageType.TrialTypeId);
            return View(ptsTrialTypeDosageType);
        }

        // GET: PtsTrialTypeDosageTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialTypeDosageType = await _context.PtsTrialTypeDosageType.FindAsync(id);
            if (ptsTrialTypeDosageType == null)
            {
                return NotFound();
            }
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId", ptsTrialTypeDosageType.DosageTypeId);
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId", ptsTrialTypeDosageType.TrialTypeId);
            return View(ptsTrialTypeDosageType);
        }

        // POST: PtsTrialTypeDosageTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrialTypeDosageTypeId,TrialTypeId,DosageTypeId")] PtsTrialTypeDosageType ptsTrialTypeDosageType)
        {
            if (id != ptsTrialTypeDosageType.TrialTypeDosageTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTrialTypeDosageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTrialTypeDosageTypeExists(ptsTrialTypeDosageType.TrialTypeDosageTypeId))
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
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId", ptsTrialTypeDosageType.DosageTypeId);
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId", ptsTrialTypeDosageType.TrialTypeId);
            return View(ptsTrialTypeDosageType);
        }

        // GET: PtsTrialTypeDosageTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialTypeDosageType = await _context.PtsTrialTypeDosageType
                .Include(p => p.DosageType)
                .Include(p => p.TrialType)
                .FirstOrDefaultAsync(m => m.TrialTypeDosageTypeId == id);
            if (ptsTrialTypeDosageType == null)
            {
                return NotFound();
            }

            return View(ptsTrialTypeDosageType);
        }

        // POST: PtsTrialTypeDosageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTrialTypeDosageType = await _context.PtsTrialTypeDosageType.FindAsync(id);
            _context.PtsTrialTypeDosageType.Remove(ptsTrialTypeDosageType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTrialTypeDosageTypeExists(int id)
        {
            return _context.PtsTrialTypeDosageType.Any(e => e.TrialTypeDosageTypeId == id);
        }
    }
}
