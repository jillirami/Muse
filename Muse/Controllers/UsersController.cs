﻿using System;
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
    public class UsersController : Controller
    {
        private readonly MuseContext _context;

        public UsersController(MuseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId == 0 || userId == null)
            {
                return View(await _context.User.ToListAsync());
            }
            return RedirectToAction("Home");
        }

        public IActionResult Signout()
        {
            HttpContext.Session.SetInt32("userId", 0);
            return RedirectToAction("Frontpage");
        }

        public IActionResult Signin(string searchEmail, string searchPassword)
        {
            HttpContext.Session.SetInt32("userId", 0);
            var users = from u in _context.User
                        select u;

            var user = users.SingleOrDefault(s => s.Email.Equals(searchEmail));
            if (user != null)
            {
                if (user.Password.Equals(searchPassword))
                {
                    HttpContext.Session.SetInt32("userId", user.Id);                    
                    return RedirectToAction("Home");
                }
                TempData["Error"] = "Password incorrect";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Email not found or matched";
            return RedirectToAction("Create");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details()
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (!userId.HasValue)
            {
                return RedirectToAction("Home");
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Home");
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Zip,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit()
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (!userId.HasValue)
            {
                return NotFound();
            }

            var user = await _context.User
                .FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Zip,Email,Password")] User user)
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        public IActionResult Frontpage()
        {
            return View();
        }

        public async Task<IActionResult> Home()
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (!userId.HasValue)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
