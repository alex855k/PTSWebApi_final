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
    public class PtsTrialBlocksController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTrialBlocksController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTrialBlocks
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTrialBlock.Include(p => p.FieldBlock).Include(p => p.TrialType);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTrialBlocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialBlock = await _context.PtsTrialBlock
                .Include(p => p.FieldBlock)
                .Include(p => p.TrialType)
                .FirstOrDefaultAsync(m => m.TrialBlockId == id);
            if (ptsTrialBlock == null)
            {
                return NotFound();
            }

            return View(ptsTrialBlock);
        }

        // GET: PtsTrialBlocks/Create
        public IActionResult Create()
        {
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar");
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId");
            return View();
        }

        // POST: PtsTrialBlocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrialBlockId,TrialBlockDescription,TrialEnd,TrialTypeId,FieldBlockId")] PtsTrialBlock ptsTrialBlock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTrialBlock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar", ptsTrialBlock.FieldBlockId);
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId", ptsTrialBlock.TrialTypeId);
            return View(ptsTrialBlock);
        }

        // GET: PtsTrialBlocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialBlock = await _context.PtsTrialBlock.FindAsync(id);
            if (ptsTrialBlock == null)
            {
                return NotFound();
            }
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar", ptsTrialBlock.FieldBlockId);
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId", ptsTrialBlock.TrialTypeId);
            return View(ptsTrialBlock);
        }

        // POST: PtsTrialBlocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrialBlockId,TrialBlockDescription,TrialEnd,TrialTypeId,FieldBlockId")] PtsTrialBlock ptsTrialBlock)
        {
            if (id != ptsTrialBlock.TrialBlockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTrialBlock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTrialBlockExists(ptsTrialBlock.TrialBlockId))
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
            ViewData["FieldBlockId"] = new SelectList(_context.PtsFieldBlock, "FieldBlockId", "BlockChar", ptsTrialBlock.FieldBlockId);
            ViewData["TrialTypeId"] = new SelectList(_context.PtsTrialType, "TrialTypeId", "TrialTypeId", ptsTrialBlock.TrialTypeId);
            return View(ptsTrialBlock);
        }

        // GET: PtsTrialBlocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTrialBlock = await _context.PtsTrialBlock
                .Include(p => p.FieldBlock)
                .Include(p => p.TrialType)
                .FirstOrDefaultAsync(m => m.TrialBlockId == id);
            if (ptsTrialBlock == null)
            {
                return NotFound();
            }

            return View(ptsTrialBlock);
        }

        // POST: PtsTrialBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTrialBlock = await _context.PtsTrialBlock.FindAsync(id);
            _context.PtsTrialBlock.Remove(ptsTrialBlock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTrialBlockExists(int id)
        {
            return _context.PtsTrialBlock.Any(e => e.TrialBlockId == id);
        }
    }
}
