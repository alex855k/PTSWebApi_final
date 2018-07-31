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
    public class PtsPlantTypesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsPlantTypesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsPlantTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsPlantType.ToListAsync());
        }

        // GET: PtsPlantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsPlantType = await _context.PtsPlantType
                .FirstOrDefaultAsync(m => m.PlantTypeId == id);
            if (ptsPlantType == null)
            {
                return NotFound();
            }

            return View(ptsPlantType);
        }

        // GET: PtsPlantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsPlantTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantTypeId,PlantTypeName")] PtsPlantType ptsPlantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsPlantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsPlantType);
        }

        // GET: PtsPlantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsPlantType = await _context.PtsPlantType.FindAsync(id);
            if (ptsPlantType == null)
            {
                return NotFound();
            }
            return View(ptsPlantType);
        }

        // POST: PtsPlantTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantTypeId,PlantTypeName")] PtsPlantType ptsPlantType)
        {
            if (id != ptsPlantType.PlantTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsPlantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsPlantTypeExists(ptsPlantType.PlantTypeId))
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
            return View(ptsPlantType);
        }

        // GET: PtsPlantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsPlantType = await _context.PtsPlantType
                .FirstOrDefaultAsync(m => m.PlantTypeId == id);
            if (ptsPlantType == null)
            {
                return NotFound();
            }

            return View(ptsPlantType);
        }

        // POST: PtsPlantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsPlantType = await _context.PtsPlantType.FindAsync(id);
            _context.PtsPlantType.Remove(ptsPlantType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsPlantTypeExists(int id)
        {
            return _context.PtsPlantType.Any(e => e.PlantTypeId == id);
        }
    }
}
