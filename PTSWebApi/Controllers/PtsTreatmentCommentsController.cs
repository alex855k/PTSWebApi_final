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
    public class PtsTreatmentCommentsController : Controller
    {
        private readonly EJL83_DBContext _context;

        public PtsTreatmentCommentsController(EJL83_DBContext context)
        {
            _context = context;
        }

        // GET: PtsTreatmentComments
        public async Task<IActionResult> Index()
        {
            var eJL83_DBContext = _context.PtsTreatmentComment.Include(p => p.Comment).Include(p => p.Treatment);
            return View(await eJL83_DBContext.ToListAsync());
        }

        // GET: PtsTreatmentComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentComment = await _context.PtsTreatmentComment
                .Include(p => p.Comment)
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.TreatmentCommentId == id);
            if (ptsTreatmentComment == null)
            {
                return NotFound();
            }

            return View(ptsTreatmentComment);
        }

        // GET: PtsTreatmentComments/Create
        public IActionResult Create()
        {
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText");
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId");
            return View();
        }

        // POST: PtsTreatmentComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreatmentCommentId,TreatmentId,CommentId")] PtsTreatmentComment ptsTreatmentComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptsTreatmentComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText", ptsTreatmentComment.CommentId);
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreatmentComment.TreatmentId);
            return View(ptsTreatmentComment);
        }

        // GET: PtsTreatmentComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentComment = await _context.PtsTreatmentComment.FindAsync(id);
            if (ptsTreatmentComment == null)
            {
                return NotFound();
            }
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText", ptsTreatmentComment.CommentId);
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreatmentComment.TreatmentId);
            return View(ptsTreatmentComment);
        }

        // POST: PtsTreatmentComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreatmentCommentId,TreatmentId,CommentId")] PtsTreatmentComment ptsTreatmentComment)
        {
            if (id != ptsTreatmentComment.TreatmentCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptsTreatmentComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtsTreatmentCommentExists(ptsTreatmentComment.TreatmentCommentId))
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
            ViewData["CommentId"] = new SelectList(_context.PtsComment, "CommentId", "CommentText", ptsTreatmentComment.CommentId);
            ViewData["TreatmentId"] = new SelectList(_context.PtsTreatment, "TreatmentId", "TreatmentId", ptsTreatmentComment.TreatmentId);
            return View(ptsTreatmentComment);
        }

        // GET: PtsTreatmentComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptsTreatmentComment = await _context.PtsTreatmentComment
                .Include(p => p.Comment)
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.TreatmentCommentId == id);
            if (ptsTreatmentComment == null)
            {
                return NotFound();
            }

            return View(ptsTreatmentComment);
        }

        // POST: PtsTreatmentComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptsTreatmentComment = await _context.PtsTreatmentComment.FindAsync(id);
            _context.PtsTreatmentComment.Remove(ptsTreatmentComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtsTreatmentCommentExists(int id)
        {
            return _context.PtsTreatmentComment.Any(e => e.TreatmentCommentId == id);
        }
    }
}
