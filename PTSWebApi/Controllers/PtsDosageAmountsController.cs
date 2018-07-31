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
    public class PtsDosageAmountsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsDosageAmountsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsDosageAmounts
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsDosageAmount.Include(p => p.DosageType).Include(p => p.UnitType);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsDosageAmounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsDosageAmount = await _context.PtsDosageAmount
                .Include(p => p.DosageType)
                .Include(p => p.UnitType)
                .FirstOrDefaultAsync(m => m.DosageAmountId == id);
            if (ptsDosageAmount == null)
            {
                return NotFound();
            }

            return View(ptsDosageAmount);
        }

        // GET: PtsDosageAmounts/Create
        public IActionResult Create()
        {
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId");
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName");
            return View();
        }

        // POST: PtsDosageAmounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DosageAmountId,ResultValue,UnitTypeId,DosageTypeId")] PtsDosageAmount ptsDosageAmount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsDosageAmount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId", ptsDosageAmount.DosageTypeId);
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName", ptsDosageAmount.UnitTypeId);
            return View(ptsDosageAmount);
        }

        // GET: PtsDosageAmounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsDosageAmount = await _context.PtsDosageAmount.FindAsync(id);
            if (ptsDosageAmount == null)
            {
                return NotFound();
            }
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId", ptsDosageAmount.DosageTypeId);
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName", ptsDosageAmount.UnitTypeId);
            return View(ptsDosageAmount);
        }

        // POST: PtsDosageAmounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DosageAmountId,ResultValue,UnitTypeId,DosageTypeId")] PtsDosageAmount ptsDosageAmount)
        {
            if (id != ptsDosageAmount.DosageAmountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsDosageAmount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsDosageAmountExists(ptsDosageAmount.DosageAmountId))
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
            ViewData["DosageTypeId"] = new SelectList(_context.PtsDosageType, "DosageTypeId", "DosageTypeId", ptsDosageAmount.DosageTypeId);
            ViewData["UnitTypeId"] = new SelectList(_context.PtsUnitType, "UnitTypeId", "UnitTypeName", ptsDosageAmount.UnitTypeId);
            return View(ptsDosageAmount);
        }

        // GET: PtsDosageAmounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsDosageAmount = await _context.PtsDosageAmount
                .Include(p => p.DosageType)
                .Include(p => p.UnitType)
                .FirstOrDefaultAsync(m => m.DosageAmountId == id);
            if (ptsDosageAmount == null)
            {
                return NotFound();
            }

            return View(ptsDosageAmount);
        }

        // POST: PtsDosageAmounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsDosageAmount = await _context.PtsDosageAmount.FindAsync(id);
            _context.PtsDosageAmount.Remove(ptsDosageAmount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsDosageAmountExists(int id)
        {
            return _context.PtsDosageAmount.Any(e => e.DosageAmountId == id);
        }
    }
}
