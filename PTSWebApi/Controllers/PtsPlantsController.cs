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
    public class PtsPlantsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsPlantsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsPlants
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsPlant.Include(p => p.PlantType);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsPlants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsPlant = await _context.PtsPlant
                .Include(p => p.PlantType)
                .FirstOrDefaultAsync(m => m.PlantId == id);
            if (ptsPlant == null)
            {
                return NotFound();
            }

            return View(ptsPlant);
        }

        // GET: PtsPlants/Create
        public IActionResult Create()
        {
            ViewData["PlantTypeId"] = new SelectList(_context.PtsPlantType, "PlantTypeId", "PlantTypeName");
            return View();
        }

        // POST: PtsPlants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantId,PlantName,PlantTypeId")] PtsPlant ptsPlant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsPlant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantTypeId"] = new SelectList(_context.PtsPlantType, "PlantTypeId", "PlantTypeName", ptsPlant.PlantTypeId);
            return View(ptsPlant);
        }

        // GET: PtsPlants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsPlant = await _context.PtsPlant.FindAsync(id);
            if (ptsPlant == null)
            {
                return NotFound();
            }
            ViewData["PlantTypeId"] = new SelectList(_context.PtsPlantType, "PlantTypeId", "PlantTypeName", ptsPlant.PlantTypeId);
            return View(ptsPlant);
        }

        // POST: PtsPlants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantId,PlantName,PlantTypeId")] PtsPlant ptsPlant)
        {
            if (id != ptsPlant.PlantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsPlant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsPlantExists(ptsPlant.PlantId))
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
            ViewData["PlantTypeId"] = new SelectList(_context.PtsPlantType, "PlantTypeId", "PlantTypeName", ptsPlant.PlantTypeId);
            return View(ptsPlant);
        }

        // GET: PtsPlants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsPlant = await _context.PtsPlant
                .Include(p => p.PlantType)
                .FirstOrDefaultAsync(m => m.PlantId == id);
            if (ptsPlant == null)
            {
                return NotFound();
            }

            return View(ptsPlant);
        }

        // POST: PtsPlants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsPlant = await _context.PtsPlant.FindAsync(id);
            _context.PtsPlant.Remove(ptsPlant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsPlantExists(int id)
        {
            return _context.PtsPlant.Any(e => e.PlantId == id);
        }
    }
}
