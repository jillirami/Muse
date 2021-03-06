﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Muse.Models;
using System.Diagnostics;
using VaderSharp;

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
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (userId.HasValue)
            {
                var musings = from m in _context.Musing
                              where m.User.Id == userId
                              orderby m.Date descending
                              select m;

                return View(musings);
            }

            return RedirectToAction("Frontpage", "Users");            
        }

        // GET: Musings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
            }

            var musing = await _context.Musing
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musing == null)
            {
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
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
        public async Task<IActionResult> Create([Bind("Id,Title,Date,SUDS,Entry,Aspirations")] Musing musing)
        {
            if (ModelState.IsValid)
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                if (userId.HasValue)
                {
                    var user = await _context.User
                        .FirstOrDefaultAsync(m => m.Id == userId);

                    musing.User = user;

                    // credits to https://github.com/cjhutto/vaderSentiment for this awesome sentiment analyzer

                    SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();
                    musing.Sentiment = analyzer.PolarityScores(musing.Entry).Compound;

                    _context.Add(musing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Sign In Expired";
                return RedirectToAction("Index");
            }
            return View(musing);
        }

        // GET: Musings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
            }

            var musing = await _context.Musing.FindAsync(id);

            if (musing == null)
            {
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
            }
            return View(musing);
        }

        // POST: Musings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,SUDS,Entry,Aspirations")] Musing musing)
        {
            if (id != musing.Id)
            {
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();
                    musing.Sentiment = analyzer.PolarityScores(musing.Entry).Compound;
                    _context.Update(musing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusingExists(musing.Id))
                    {
                        TempData["Error"] = "Unable to Find Musing";
                        return RedirectToAction("Index");
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
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
            }

            var musing = await _context.Musing
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musing == null)
            {
                TempData["Error"] = "Unable to Find Musing";
                return RedirectToAction("Index");
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



        public IActionResult Musing()
        {

            WebClient client = new WebClient();
            Random rnd = new Random();

            var downloadString = client.DownloadString($"https://www.forbes.com/forbesapi/thought/get.json?limit=1&start={rnd.Next(5000)}&stream=true");
            var thoughts = JObject.Parse(downloadString).ToObject<ForbesThoughts>().thoughtStream.thoughts;

            ViewBag.quote = thoughts[0].quote;
            ViewBag.quoteAuthor = thoughts[0].thoughtAuthor.name;

            return View();
        }

        public IActionResult Metrics()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId.HasValue)
            {
                var musings = from m in _context.Musing
                              where m.User.Id == userId
                              orderby m.Date descending
                              select m;

                return View(musings);
            }
            return RedirectToAction("Frontpage", "Users");
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}
