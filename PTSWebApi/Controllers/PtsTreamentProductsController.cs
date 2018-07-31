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
    public class PtsTreamentProductsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTreamentProductsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTreamentProducts
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTreamentProduct.Include(p => p.Product).Include(p => p.Treatment);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTreamentProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreamentProduct = await _context.PtsTreamentProduct
                .Include(p => p.Product)
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.TreamentProductId == id);
            if (ptsTreamentProduct == null)
            {
                return NotFound();
            }

            return View(ptsTreamentProduct);
        }

        // GET: PtsTreamentProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.PtsProduct, "ProductId", "ProductName");
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId");
            return View();
        }

        // POST: PtsTreamentProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreamentProductId,ProductDose,TreatmentId,ProductId")] PtsTreamentProduct ptsTreamentProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTreamentProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.PtsProduct, "ProductId", "ProductName", ptsTreamentProduct.ProductId);
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreamentProduct.TreatmentId);
            return View(ptsTreamentProduct);
        }

        // GET: PtsTreamentProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreamentProduct = await _context.PtsTreamentProduct.FindAsync(id);
            if (ptsTreamentProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.PtsProduct, "ProductId", "ProductName", ptsTreamentProduct.ProductId);
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreamentProduct.TreatmentId);
            return View(ptsTreamentProduct);
        }

        // POST: PtsTreamentProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreamentProductId,ProductDose,TreatmentId,ProductId")] PtsTreamentProduct ptsTreamentProduct)
        {
            if (id != ptsTreamentProduct.TreamentProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTreamentProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTreamentProductExists(ptsTreamentProduct.TreamentProductId))
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
            ViewData["ProductId"] = new SelectList(_context.PtsProduct, "ProductId", "ProductName", ptsTreamentProduct.ProductId);
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreamentProduct.TreatmentId);
            return View(ptsTreamentProduct);
        }

        // GET: PtsTreamentProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreamentProduct = await _context.PtsTreamentProduct
                .Include(p => p.Product)
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.TreamentProductId == id);
            if (ptsTreamentProduct == null)
            {
                return NotFound();
            }

            return View(ptsTreamentProduct);
        }

        // POST: PtsTreamentProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTreamentProduct = await _context.PtsTreamentProduct.FindAsync(id);
            _context.PtsTreamentProduct.Remove(ptsTreamentProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTreamentProductExists(int id)
        {
            return _context.PtsTreamentProduct.Any(e => e.TreamentProductId == id);
        }
    }
}
