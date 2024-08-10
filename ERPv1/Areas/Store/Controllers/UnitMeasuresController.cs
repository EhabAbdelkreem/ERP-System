using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Settings;

namespace ERPv1.Areas.Store.Controllers
{
    [Area("Store")]
    public class UnitMeasuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitMeasuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Store/UnitMeasures
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitMeasures.ToListAsync());
        }

        // GET: Store/UnitMeasures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasure = await _context.UnitMeasures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitMeasure == null)
            {
                return NotFound();
            }

            return View(unitMeasure);
        }

        // GET: Store/UnitMeasures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Store/UnitMeasures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UnitName")] UnitMeasure unitMeasure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitMeasure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitMeasure);
        }

        // GET: Store/UnitMeasures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasure = await _context.UnitMeasures.FindAsync(id);
            if (unitMeasure == null)
            {
                return NotFound();
            }
            return View(unitMeasure);
        }

        // POST: Store/UnitMeasures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UnitName")] UnitMeasure unitMeasure)
        {
            if (id != unitMeasure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitMeasure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitMeasureExists(unitMeasure.Id))
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
            return View(unitMeasure);
        }

        // GET: Store/UnitMeasures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasure = await _context.UnitMeasures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitMeasure == null)
            {
                return NotFound();
            }

            return View(unitMeasure);
        }

        // POST: Store/UnitMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unitMeasure = await _context.UnitMeasures.FindAsync(id);
            _context.UnitMeasures.Remove(unitMeasure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitMeasureExists(int id)
        {
            return _context.UnitMeasures.Any(e => e.Id == id);
        }
    }
}
