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
    public class PtsProductCategoriesController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsProductCategoriesController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsProductCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsProductCategory.ToListAsync());
        }

        // GET: PtsProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsProductCategory = await _context.PtsProductCategory
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
            if (ptsProductCategory == null)
            {
                return NotFound();
            }

            return View(ptsProductCategory);
        }

        // GET: PtsProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductCategoryId,ProductCategoryName")] PtsProductCategory ptsProductCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsProductCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsProductCategory);
        }

        // GET: PtsProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsProductCategory = await _context.PtsProductCategory.FindAsync(id);
            if (ptsProductCategory == null)
            {
                return NotFound();
            }
            return View(ptsProductCategory);
        }

        // POST: PtsProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductCategoryId,ProductCategoryName")] PtsProductCategory ptsProductCategory)
        {
            if (id != ptsProductCategory.ProductCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsProductCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsProductCategoryExists(ptsProductCategory.ProductCategoryId))
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
            return View(ptsProductCategory);
        }

        // GET: PtsProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsProductCategory = await _context.PtsProductCategory
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
            if (ptsProductCategory == null)
            {
                return NotFound();
            }

            return View(ptsProductCategory);
        }

        // POST: PtsProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsProductCategory = await _context.PtsProductCategory.FindAsync(id);
            _context.PtsProductCategory.Remove(ptsProductCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsProductCategoryExists(int id)
        {
            return _context.PtsProductCategory.Any(e => e.ProductCategoryId == id);
        }
    }
}
