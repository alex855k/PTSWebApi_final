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
    public class PtsTrialGroupsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTrialGroupsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTrialGroups
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTrialGroup.Include(p => p.FieldBlock).Include(p => p.Plant);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTrialGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialGroup = await _context.PtsTrialGroup
                .Include(p => p.FieldBlock)
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(m => m.TrialGroupId == id);
            if (ptsTrialGroup == null)
            {
                return NotFound();
            }

            return View(ptsTrialGroup);
        }

        // GET: PtsTrialGroups/Create
        public IActionResult Create()
        {
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar");
            ViewData["PlantId"] = new SelectList(_context.PtsPlant, "PlantId", "PlantName");
            return View();
        }

        // POST: PtsTrialGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrialGroupId,TrialGroupNr,PlantId,FieldBlockId")] PtsTrialGroup ptsTrialGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTrialGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar", ptsTrialGroup.FieldBlockId);
            ViewData["PlantId"] = new SelectList(_context.PtsPlant, "PlantId", "PlantName", ptsTrialGroup.PlantId);
            return View(ptsTrialGroup);
        }

        // GET: PtsTrialGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialGroup = await _context.PtsTrialGroup.FindAsync(id);
            if (ptsTrialGroup == null)
            {
                return NotFound();
            }
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar", ptsTrialGroup.FieldBlockId);
            ViewData["PlantId"] = new SelectList(_context.PtsPlant, "PlantId", "PlantName", ptsTrialGroup.PlantId);
            return View(ptsTrialGroup);
        }

        // POST: PtsTrialGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrialGroupId,TrialGroupNr,PlantId,FieldBlockId")] PtsTrialGroup ptsTrialGroup)
        {
            if (id != ptsTrialGroup.TrialGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTrialGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTrialGroupExists(ptsTrialGroup.TrialGroupId))
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
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar", ptsTrialGroup.FieldBlockId);
            ViewData["PlantId"] = new SelectList(_context.PtsPlant, "PlantId", "PlantName", ptsTrialGroup.PlantId);
            return View(ptsTrialGroup);
        }

        // GET: PtsTrialGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialGroup = await _context.PtsTrialGroup
                .Include(p => p.FieldBlock)
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(m => m.TrialGroupId == id);
            if (ptsTrialGroup == null)
            {
                return NotFound();
            }

            return View(ptsTrialGroup);
        }

        // POST: PtsTrialGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTrialGroup = await _context.PtsTrialGroup.FindAsync(id);
            _context.PtsTrialGroup.Remove(ptsTrialGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTrialGroupExists(int id)
        {
            return _context.PtsTrialGroup.Any(e => e.TrialGroupId == id);
        }
    }
}
