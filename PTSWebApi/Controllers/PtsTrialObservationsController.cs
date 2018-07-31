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
    public class PtsTrialObservationsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTrialObservationsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTrialObservations
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTrialObservation.Include(p => p.Comment).Include(p => p.TrialGroup);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTrialObservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialObservation = await _context.PtsTrialObservation
                .Include(p => p.Comment)
                .Include(p => p.TrialGroup)
                .FirstOrDefaultAsync(m => m.TrialObservationId == id);
            if (ptsTrialObservation == null)
            {
                return NotFound();
            }

            return View(ptsTrialObservation);
        }

        // GET: PtsTrialObservations/Create
        public IActionResult Create()
        {
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText");
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId");
            return View();
        }

        // POST: PtsTrialObservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrialObservationId,CommentId,TrialGroupId")] PtsTrialObservation ptsTrialObservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTrialObservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText", ptsTrialObservation.CommentId);
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsTrialObservation.TrialGroupId);
            return View(ptsTrialObservation);
        }

        // GET: PtsTrialObservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialObservation = await _context.PtsTrialObservation.FindAsync(id);
            if (ptsTrialObservation == null)
            {
                return NotFound();
            }
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText", ptsTrialObservation.CommentId);
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsTrialObservation.TrialGroupId);
            return View(ptsTrialObservation);
        }

        // POST: PtsTrialObservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrialObservationId,CommentId,TrialGroupId")] PtsTrialObservation ptsTrialObservation)
        {
            if (id != ptsTrialObservation.TrialObservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTrialObservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTrialObservationExists(ptsTrialObservation.TrialObservationId))
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
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText", ptsTrialObservation.CommentId);
            ViewData["TrialGroupId"] = new SelectList(_context.PtsTrialGroup, "TrialGroupId", "TrialGroupId", ptsTrialObservation.TrialGroupId);
            return View(ptsTrialObservation);
        }

        // GET: PtsTrialObservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialObservation = await _context.PtsTrialObservation
                .Include(p => p.Comment)
                .Include(p => p.TrialGroup)
                .FirstOrDefaultAsync(m => m.TrialObservationId == id);
            if (ptsTrialObservation == null)
            {
                return NotFound();
            }

            return View(ptsTrialObservation);
        }

        // POST: PtsTrialObservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTrialObservation = await _context.PtsTrialObservation.FindAsync(id);
            _context.PtsTrialObservation.Remove(ptsTrialObservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTrialObservationExists(int id)
        {
            return _context.PtsTrialObservation.Any(e => e.TrialObservationId == id);
        }
    }
}
