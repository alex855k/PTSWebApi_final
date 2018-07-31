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
    public class PtsResultEntriesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsResultEntriesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsResultEntries
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsResultEntry.Include(p => p.DosageAmount).Include(p => p.ResultFormat).Include(p => p.TrialGroup);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsResultEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsResultEntry = await _context.PtsResultEntry
                .Include(p => p.DosageAmount)
                .Include(p => p.ResultFormat)
                .Include(p => p.TrialGroup)
                .FirstOrDefaultAsync(m => m.ResultEntryId == id);
            if (ptsResultEntry == null)
            {
                return NotFound();
            }

            return View(ptsResultEntry);
        }

        // GET: PtsResultEntries/Create
        public IActionResult Create()
        {
            ViewData["DosageAmountId"] = new SelectList(_context.PtsDosageAmount, "DosageAmountId", "DosageAmountId");
            ViewData["ResultFormatId"] = new SelectList(_context.PtsResultFormat, "ResultFormatId", "ResultFormatDescription");
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId");
            return View();
        }

        // POST: PtsResultEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultEntryId,ResultValue,ResultFormatId,DosageAmountId,TrialGroupId")] PtsResultEntry ptsResultEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsResultEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DosageAmountId"] = new SelectList(_context.PtsDosageAmount, "DosageAmountId", "DosageAmountId", ptsResultEntry.DosageAmountId);
            ViewData["ResultFormatId"] = new SelectList(_context.PtsResultFormat, "ResultFormatId", "ResultFormatDescription", ptsResultEntry.ResultFormatId);
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsResultEntry.TrialGroupId);
            return View(ptsResultEntry);
        }

        // GET: PtsResultEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsResultEntry = await _context.PtsResultEntry.FindAsync(id);
            if (ptsResultEntry == null)
            {
                return NotFound();
            }
            ViewData["DosageAmountId"] = new SelectList(_context.PtsDosageAmount, "DosageAmountId", "DosageAmountId", ptsResultEntry.DosageAmountId);
            ViewData["ResultFormatId"] = new SelectList(_context.PtsResultFormat, "ResultFormatId", "ResultFormatDescription", ptsResultEntry.ResultFormatId);
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsResultEntry.TrialGroupId);
            return View(ptsResultEntry);
        }

        // POST: PtsResultEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultEntryId,ResultValue,ResultFormatId,DosageAmountId,TrialGroupId")] PtsResultEntry ptsResultEntry)
        {
            if (id != ptsResultEntry.ResultEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsResultEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsResultEntryExists(ptsResultEntry.ResultEntryId))
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
            ViewData["DosageAmountId"] = new SelectList(_context.PtsDosageAmount, "DosageAmountId", "DosageAmountId", ptsResultEntry.DosageAmountId);
            ViewData["ResultFormatId"] = new SelectList(_context.PtsResultFormat, "ResultFormatId", "ResultFormatDescription", ptsResultEntry.ResultFormatId);
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsResultEntry.TrialGroupId);
            return View(ptsResultEntry);
        }

        // GET: PtsResultEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsResultEntry = await _context.PtsResultEntry
                .Include(p => p.DosageAmount)
                .Include(p => p.ResultFormat)
                .Include(p => p.TrialGroup)
                .FirstOrDefaultAsync(m => m.ResultEntryId == id);
            if (ptsResultEntry == null)
            {
                return NotFound();
            }

            return View(ptsResultEntry);
        }

        // POST: PtsResultEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsResultEntry = await _context.PtsResultEntry.FindAsync(id);
            _context.PtsResultEntry.Remove(ptsResultEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsResultEntryExists(int id)
        {
            return _context.PtsResultEntry.Any(e => e.ResultEntryId == id);
        }
    }
}
