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
    public class PtsFieldBlocksController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsFieldBlocksController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsFieldBlocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsFieldBlock.ToListAsync());
        }

        // GET: PtsFieldBlocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsFieldBlock = await _context.PtsFieldBlock
                .FirstOrDefaultAsync(m => m.FieldBlockId == id);
            if (ptsFieldBlock == null)
            {
                return NotFound();
            }

            return View(ptsFieldBlock);
        }

        // GET: PtsFieldBlocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsFieldBlocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldBlockId,BlockChar,YearCreated,BlockDescription")] PtsFieldBlock ptsFieldBlock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsFieldBlock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsFieldBlock);
        }

        // GET: PtsFieldBlocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsFieldBlock = await _context.PtsFieldBlock.FindAsync(id);
            if (ptsFieldBlock == null)
            {
                return NotFound();
            }
            return View(ptsFieldBlock);
        }

        // POST: PtsFieldBlocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FieldBlockId,BlockChar,YearCreated,BlockDescription")] PtsFieldBlock ptsFieldBlock)
        {
            if (id != ptsFieldBlock.FieldBlockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsFieldBlock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsFieldBlockExists(ptsFieldBlock.FieldBlockId))
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
            return View(ptsFieldBlock);
        }

        // GET: PtsFieldBlocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsFieldBlock = await _context.PtsFieldBlock
                .FirstOrDefaultAsync(m => m.FieldBlockId == id);
            if (ptsFieldBlock == null)
            {
                return NotFound();
            }

            return View(ptsFieldBlock);
        }

        // POST: PtsFieldBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsFieldBlock = await _context.PtsFieldBlock.FindAsync(id);
            _context.PtsFieldBlock.Remove(ptsFieldBlock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsFieldBlockExists(int id)
        {
            return _context.PtsFieldBlock.Any(e => e.FieldBlockId == id);
        }
    }
}
