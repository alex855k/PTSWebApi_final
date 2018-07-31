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
    public class PtsDosageTypesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsDosageTypesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsDosageTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsDosageType.ToListAsync());
        }

        // GET: PtsDosageTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsDosageType = await _context.PtsDosageType
                .FirstOrDefaultAsync(m => m.DosageTypeId == id);
            if (ptsDosageType == null)
            {
                return NotFound();
            }

            return View(ptsDosageType);
        }

        // GET: PtsDosageTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsDosageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DosageTypeId,DosageName,UnitTypeId")] PtsDosageType ptsDosageType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsDosageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsDosageType);
        }

        // GET: PtsDosageTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsDosageType = await _context.PtsDosageType.FindAsync(id);
            if (ptsDosageType == null)
            {
                return NotFound();
            }
            return View(ptsDosageType);
        }

        // POST: PtsDosageTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DosageTypeId,DosageName,UnitTypeId")] PtsDosageType ptsDosageType)
        {
            if (id != ptsDosageType.DosageTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsDosageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsDosageTypeExists(ptsDosageType.DosageTypeId))
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
            return View(ptsDosageType);
        }

        // GET: PtsDosageTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsDosageType = await _context.PtsDosageType
                .FirstOrDefaultAsync(m => m.DosageTypeId == id);
            if (ptsDosageType == null)
            {
                return NotFound();
            }

            return View(ptsDosageType);
        }

        // POST: PtsDosageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsDosageType = await _context.PtsDosageType.FindAsync(id);
            _context.PtsDosageType.Remove(ptsDosageType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsDosageTypeExists(int id)
        {
            return _context.PtsDosageType.Any(e => e.DosageTypeId == id);
        }
    }
}
