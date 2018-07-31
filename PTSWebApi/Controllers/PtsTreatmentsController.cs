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
    public class PtsTreatmentsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTreatmentsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTreatments
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTreatment.Include(p => p.TreatmentType).Include(p => p.TreatmentTypeNavigation);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTreatments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatment = await _context.PtsTreatment
                .Include(p => p.TreatmentType)
                .Include(p => p.TreatmentTypeNavigation)
                .FirstOrDefaultAsync(m => m.TreatmentId == id);
            if (ptsTreatment == null)
            {
                return NotFound();
            }

            return View(ptsTreatment);
        }

        // GET: PtsTreatments/Create
        public IActionResult Create()
        {
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTreatmentType, "TreatmentTypeId", "TreatmentTypeId");
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId");
            return View();
        }

        // POST: PtsTreatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreatmentId,TreatmentDate,TreatmentStage,IsTrialTreatment,TreatmentTypeId,TrialGroupId")] PtsTreatment ptsTreatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTreatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTreatmentType, "TreatmentTypeId", "TreatmentTypeId", ptsTreatment.TreatmentTypeId);
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsTreatment.TreatmentTypeId);
            return View(ptsTreatment);
        }

        // GET: PtsTreatments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatment = await _context.PtsTreatment.FindAsync(id);
            if (ptsTreatment == null)
            {
                return NotFound();
            }
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTreatmentType, "TreatmentTypeId", "TreatmentTypeId", ptsTreatment.TreatmentTypeId);
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsTreatment.TreatmentTypeId);
            return View(ptsTreatment);
        }

        // POST: PtsTreatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreatmentId,TreatmentDate,TreatmentStage,IsTrialTreatment,TreatmentTypeId,TrialGroupId")] PtsTreatment ptsTreatment)
        {
            if (id != ptsTreatment.TreatmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTreatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTreatmentExists(ptsTreatment.TreatmentId))
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
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTreatmentType, "TreatmentTypeId", "TreatmentTypeId", ptsTreatment.TreatmentTypeId);
            ViewData["TreatmentTypeId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsTreatment.TreatmentTypeId);
            return View(ptsTreatment);
        }

        // GET: PtsTreatments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatment = await _context.PtsTreatment
                .Include(p => p.TreatmentType)
                .Include(p => p.TreatmentTypeNavigation)
                .FirstOrDefaultAsync(m => m.TreatmentId == id);
            if (ptsTreatment == null)
            {
                return NotFound();
            }

            return View(ptsTreatment);
        }

        // POST: PtsTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTreatment = await _context.PtsTreatment.FindAsync(id);
            _context.PtsTreatment.Remove(ptsTreatment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTreatmentExists(int id)
        {
            return _context.PtsTreatment.Any(e => e.TreatmentId == id);
        }
    }
}
