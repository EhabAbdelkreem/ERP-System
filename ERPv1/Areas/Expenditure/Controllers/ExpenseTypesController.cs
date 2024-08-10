using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPv1.Data;
using ERPv1.ERP.PurchasesModule.Model;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class ExpenseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenditure/ExpenseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.expenseTypes.ToListAsync());
        }

        // GET: Expenditure/ExpenseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.expenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        // GET: Expenditure/ExpenseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenditure/ExpenseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenseTypeName")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        // GET: Expenditure/ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.expenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // POST: Expenditure/ExpenseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenseTypeName")] ExpenseType expenseType)
        {
            if (id != expenseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(expenseType.Id))
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
            return View(expenseType);
        }

        // GET: Expenditure/ExpenseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.expenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        // POST: Expenditure/ExpenseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseType = await _context.expenseTypes.FindAsync(id);
            _context.expenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseTypeExists(int id)
        {
            return _context.expenseTypes.Any(e => e.Id == id);
        }
    }
}
