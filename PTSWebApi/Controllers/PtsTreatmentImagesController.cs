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
    public class PtsTreatmentImagesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTreatmentImagesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTreatmentImages
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTreatmentImage.Include(p => p.Treatment);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTreatmentImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentImage = await _context.PtsTreatmentImage
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.TreatmentImageId == id);
            if (ptsTreatmentImage == null)
            {
                return NotFound();
            }

            return View(ptsTreatmentImage);
        }

        // GET: PtsTreatmentImages/Create
        public IActionResult Create()
        {
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId");
            return View();
        }

        // POST: PtsTreatmentImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreatmentImageId,ImagePath,Caption,TreatmentId")] PtsTreatmentImage ptsTreatmentImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTreatmentImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreatmentImage.TreatmentId);
            return View(ptsTreatmentImage);
        }

        // GET: PtsTreatmentImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentImage = await _context.PtsTreatmentImage.FindAsync(id);
            if (ptsTreatmentImage == null)
            {
                return NotFound();
            }
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreatmentImage.TreatmentId);
            return View(ptsTreatmentImage);
        }

        // POST: PtsTreatmentImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreatmentImageId,ImagePath,Caption,TreatmentId")] PtsTreatmentImage ptsTreatmentImage)
        {
            if (id != ptsTreatmentImage.TreatmentImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTreatmentImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTreatmentImageExists(ptsTreatmentImage.TreatmentImageId))
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
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreatmentImage.TreatmentId);
            return View(ptsTreatmentImage);
        }

        // GET: PtsTreatmentImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentImage = await _context.PtsTreatmentImage
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.TreatmentImageId == id);
            if (ptsTreatmentImage == null)
            {
                return NotFound();
            }

            return View(ptsTreatmentImage);
        }

        // POST: PtsTreatmentImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTreatmentImage = await _context.PtsTreatmentImage.FindAsync(id);
            _context.PtsTreatmentImage.Remove(ptsTreatmentImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTreatmentImageExists(int id)
        {
            return _context.PtsTreatmentImage.Any(e => e.TreatmentImageId == id);
        }
    }
}
