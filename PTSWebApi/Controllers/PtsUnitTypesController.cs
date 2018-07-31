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
    public class PtsUnitTypesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsUnitTypesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsUnitTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsUnitType.ToListAsync());
        }

        // GET: PtsUnitTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsUnitType = await _context.PtsUnitType
                .FirstOrDefaultAsync(m => m.UnitTypeId == id);
            if (ptsUnitType == null)
            {
                return NotFound();
            }

            return View(ptsUnitType);
        }

        // GET: PtsUnitTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsUnitTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitTypeId,UnitTypeName")] PtsUnitType ptsUnitType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsUnitType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsUnitType);
        }

        // GET: PtsUnitTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsUnitType = await _context.PtsUnitType.FindAsync(id);
            if (ptsUnitType == null)
            {
                return NotFound();
            }
            return View(ptsUnitType);
        }

        // POST: PtsUnitTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitTypeId,UnitTypeName")] PtsUnitType ptsUnitType)
        {
            if (id != ptsUnitType.UnitTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsUnitType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsUnitTypeExists(ptsUnitType.UnitTypeId))
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
            return View(ptsUnitType);
        }

        // GET: PtsUnitTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsUnitType = await _context.PtsUnitType
                .FirstOrDefaultAsync(m => m.UnitTypeId == id);
            if (ptsUnitType == null)
            {
                return NotFound();
            }

            return View(ptsUnitType);
        }

        // POST: PtsUnitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsUnitType = await _context.PtsUnitType.FindAsync(id);
            _context.PtsUnitType.Remove(ptsUnitType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsUnitTypeExists(int id)
        {
            return _context.PtsUnitType.Any(e => e.UnitTypeId == id);
        }
    }
}
