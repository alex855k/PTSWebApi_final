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
    public class PtsProductsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsProductsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsProducts
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsProduct.Include(p => p.ProductCategory);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsProduct = await _context.PtsProduct
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (ptsProduct == null)
            {
                return NotFound();
            }

            return View(ptsProduct);
        }

        // GET: PtsProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.PtsProductCategory, "ProductCategoryId", "ProductCategoryName");
            return View();
        }

        // POST: PtsProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductOwner,ProductCategoryId")] PtsProduct ptsProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.PtsProductCategory, "ProductCategoryId", "ProductCategoryName", ptsProduct.ProductCategoryId);
            return View(ptsProduct);
        }

        // GET: PtsProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsProduct = await _context.PtsProduct.FindAsync(id);
            if (ptsProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.PtsProductCategory, "ProductCategoryId", "ProductCategoryName", ptsProduct.ProductCategoryId);
            return View(ptsProduct);
        }

        // POST: PtsProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductOwner,ProductCategoryId")] PtsProduct ptsProduct)
        {
            if (id != ptsProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsProductExists(ptsProduct.ProductId))
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
            ViewData["ProductCategoryId"] = new SelectList(_context.PtsProductCategory, "ProductCategoryId", "ProductCategoryName", ptsProduct.ProductCategoryId);
            return View(ptsProduct);
        }

        // GET: PtsProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsProduct = await _context.PtsProduct
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (ptsProduct == null)
            {
                return NotFound();
            }

            return View(ptsProduct);
        }

        // POST: PtsProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsProduct = await _context.PtsProduct.FindAsync(id);
            _context.PtsProduct.Remove(ptsProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsProductExists(int id)
        {
            return _context.PtsProduct.Any(e => e.ProductId == id);
        }
    }
}
