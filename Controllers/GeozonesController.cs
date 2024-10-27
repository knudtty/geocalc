using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Geocalc.Data;
using Geocalc.Models;

namespace Geocalc.Controllers
{
    public class GeozonesController : Controller
    {
        private readonly GeoZoneContext _context;

        public GeozonesController(GeoZoneContext context)
        {
            _context = context;
        }

        // GET: Geozones
        public async Task<IActionResult> Index()
        {
            return View(await _context.GeoZones.ToListAsync());
        }

        // GET: Geozones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geoZone = await _context.GeoZones
                .FirstOrDefaultAsync(m => m.GeoZoneID == id);
            if (geoZone == null)
            {
                return NotFound();
            }

            return View(geoZone);
        }

        // GET: Geozones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Geozones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeoZoneID,Border")] GeoZone geoZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(geoZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(geoZone);
        }

        // GET: Geozones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geoZone = await _context.GeoZones.FindAsync(id);
            if (geoZone == null)
            {
                return NotFound();
            }
            return View(geoZone);
        }

        // POST: Geozones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GeoZoneID,Border")] GeoZone geoZone)
        {
            if (id != geoZone.GeoZoneID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(geoZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeoZoneExists(geoZone.GeoZoneID))
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
            return View(geoZone);
        }

        // GET: Geozones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geoZone = await _context.GeoZones
                .FirstOrDefaultAsync(m => m.GeoZoneID == id);
            if (geoZone == null)
            {
                return NotFound();
            }

            return View(geoZone);
        }

        // POST: Geozones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var geoZone = await _context.GeoZones.FindAsync(id);
            if (geoZone != null)
            {
                _context.GeoZones.Remove(geoZone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeoZoneExists(int id)
        {
            return _context.GeoZones.Any(e => e.GeoZoneID == id);
        }
    }
}
