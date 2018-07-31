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
    public class PtsTreatmentTypesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTreatmentTypesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTreatmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsTreatmentType.ToListAsync());
        }

        // GET: PtsTreatmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentType = await _context.PtsTreatmentType
                .FirstOrDefaultAsync(m => m.TreatmentTypeId == id);
            if (ptsTreatmentType == null)
            {
                return NotFound();
            }

            return View(ptsTreatmentType);
        }

        // GET: PtsTreatmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsTreatmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreatmentTypeId,TreatmentTypeName")] PtsTreatmentType ptsTreatmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTreatmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsTreatmentType);
        }

        // GET: PtsTreatmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentType = await _context.PtsTreatmentType.FindAsync(id);
            if (ptsTreatmentType == null)
            {
                return NotFound();
            }
            return View(ptsTreatmentType);
        }

        // POST: PtsTreatmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreatmentTypeId,TreatmentTypeName")] PtsTreatmentType ptsTreatmentType)
        {
            if (id != ptsTreatmentType.TreatmentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTreatmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTreatmentTypeExists(ptsTreatmentType.TreatmentTypeId))
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
            return View(ptsTreatmentType);
        }

        // GET: PtsTreatmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentType = await _context.PtsTreatmentType
                .FirstOrDefaultAsync(m => m.TreatmentTypeId == id);
            if (ptsTreatmentType == null)
            {
                return NotFound();
            }

            return View(ptsTreatmentType);
        }

        // POST: PtsTreatmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTreatmentType = await _context.PtsTreatmentType.FindAsync(id);
            _context.PtsTreatmentType.Remove(ptsTreatmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTreatmentTypeExists(int id)
        {
            return _context.PtsTreatmentType.Any(e => e.TreatmentTypeId == id);
        }
    }
}
