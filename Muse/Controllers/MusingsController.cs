using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Muse.Models;
using System.Diagnostics;

namespace Muse.Controllers
{
    public class MusingsController : Controller
    {
        private readonly MuseContext _context;

        public MusingsController(MuseContext context)
        {
            _context = context;
        }

        // GET: Musings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Musing.ToListAsync());

            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId.HasValue)
            {
                var musings = from m in _context.Musing
                              select m;
                musings = musings.Where(m => m.User.Id == userId.Value);
                return View(musings);
            }
            return View(await _context.Musing.ToListAsync());
        }

        // GET: Musings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musing = await _context.Musing
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musing == null)
            {
                return NotFound();
            }

            return View(musing);
        }

        // GET: Musings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,SUDS,Entry")] Musing musing)
        {
            if (ModelState.IsValid)
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                if (userId.HasValue)
                {
                    _context.Add(musing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                    musing.User.Id = userId.Value;
                }
            }
            return View(musing);
        }

        // GET: Musings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musing = await _context.Musing.FindAsync(id);
            if (musing == null)
            {
                return NotFound();
            }
            return View(musing);
        }

        // POST: Musings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,SUDS,Entry")] Musing musing)
        {
            if (id != musing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusingExists(musing.Id))
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
            return View(musing);
        }

        // GET: Musings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musing = await _context.Musing
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musing == null)
            {
                return NotFound();
            }

            return View(musing);
        }

        // POST: Musings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musing = await _context.Musing.FindAsync(id);
            _context.Musing.Remove(musing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusingExists(int id)
        {
            return _context.Musing.Any(e => e.Id == id);
        }

        public IActionResult Meaning()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }
    }
}
