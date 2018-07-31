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
    public class PtsResultFormatsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsResultFormatsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsResultFormats
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsResultFormat.Include(p => p.TrialBlock).Include(p => p.UnitType);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsResultFormats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsResultFormat = await _context.PtsResultFormat
                .Include(p => p.TrialBlock)
                .Include(p => p.UnitType)
                .FirstOrDefaultAsync(m => m.ResultFormatId == id);
            if (ptsResultFormat == null)
            {
                return NotFound();
            }

            return View(ptsResultFormat);
        }

        // GET: PtsResultFormats/Create
        public IActionResult Create()
        {
            ViewData["TrialBlockId"] = new SelectList(_context.PtsTrialBlock, "TrialBlockId", "TrialBlockId");
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName");
            return View();
        }

        // POST: PtsResultFormats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultFormatId,ResultFormatTitle,ResultFormatDescription,TrialBlockId,UnitTypeId")] PtsResultFormat ptsResultFormat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsResultFormat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrialBlockId"] = new SelectList(_context.PtsTrialBlock, "TrialBlockId", "TrialBlockId", ptsResultFormat.TrialBlockId);
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName", ptsResultFormat.UnitTypeId);
            return View(ptsResultFormat);
        }

        // GET: PtsResultFormats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsResultFormat = await _context.PtsResultFormat.FindAsync(id);
            if (ptsResultFormat == null)
            {
                return NotFound();
            }
            ViewData["TrialBlockId"] = new SelectList(_context.PtsTrialBlock, "TrialBlockId", "TrialBlockId", ptsResultFormat.TrialBlockId);
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName", ptsResultFormat.UnitTypeId);
            return View(ptsResultFormat);
        }

        // POST: PtsResultFormats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultFormatId,ResultFormatTitle,ResultFormatDescription,TrialBlockId,UnitTypeId")] PtsResultFormat ptsResultFormat)
        {
            if (id != ptsResultFormat.ResultFormatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsResultFormat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsResultFormatExists(ptsResultFormat.ResultFormatId))
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
            ViewData["TrialBlockId"] = new SelectList(_context.PtsTrialBlock, "TrialBlockId", "TrialBlockId", ptsResultFormat.TrialBlockId);
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName", ptsResultFormat.UnitTypeId);
            return View(ptsResultFormat);
        }

        // GET: PtsResultFormats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsResultFormat = await _context.PtsResultFormat
                .Include(p => p.TrialBlock)
                .Include(p => p.UnitType)
                .FirstOrDefaultAsync(m => m.ResultFormatId == id);
            if (ptsResultFormat == null)
            {
                return NotFound();
            }

            return View(ptsResultFormat);
        }

        // POST: PtsResultFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsResultFormat = await _context.PtsResultFormat.FindAsync(id);
            _context.PtsResultFormat.Remove(ptsResultFormat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsResultFormatExists(int id)
        {
            return _context.PtsResultFormat.Any(e => e.ResultFormatId == id);
        }
    }
}
