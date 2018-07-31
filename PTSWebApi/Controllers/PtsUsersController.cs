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
    public class PtsUsersController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsUsersController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PtsUser.ToListAsync());
        }

        // GET: PtsUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsUser = await _context.PtsUser
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (ptsUser == null)
            {
                return NotFound();
            }

            return View(ptsUser);
        }

        // GET: PtsUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PtsUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,UserPassword,Firstname,Lastname,EmailAddress")] PtsUser ptsUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptsUser);
        }

        // GET: PtsUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsUser = await _context.PtsUser.FindAsync(id);
            if (ptsUser == null)
            {
                return NotFound();
            }
            return View(ptsUser);
        }

        // POST: PtsUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,UserPassword,Firstname,Lastname,EmailAddress")] PtsUser ptsUser)
        {
            if (id != ptsUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsUserExists(ptsUser.UserId))
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
            return View(ptsUser);
        }

        // GET: PtsUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsUser = await _context.PtsUser
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (ptsUser == null)
            {
                return NotFound();
            }

            return View(ptsUser);
        }

        // POST: PtsUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsUser = await _context.PtsUser.FindAsync(id);
            _context.PtsUser.Remove(ptsUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsUserExists(int id)
        {
            return _context.PtsUser.Any(e => e.UserId == id);
        }
    }
}
