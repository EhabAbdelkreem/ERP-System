using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.PurchasesModule.Services;
using ERPv1.ERP.PurchasesModule.ViewModel.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class ExpensesController : Controller
    {
        private readonly ExpensesManager _expensesManager;

        public ExpensesController(ExpensesManager expensesManager)
        {
            _expensesManager = expensesManager;
        }
        public IActionResult Index()
        {
            var vm = _expensesManager.GetAllExpenseItems();
            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ExpenseCreationVM vm)
        {
            _expensesManager.AddNewExpenseItem(vm);
            return RedirectToAction(nameof(this.Index));

        }


        public IActionResult NewExpense()
        {
            var vm = new ExpenseVM();
            vm.SaveURL = "/Expenditure/Expenses/SaveExpenses";
            return View(vm);
        }

        public IActionResult SaveExpenses([FromBody] ExpenseVM expense)
        {
            _expensesManager.SaveNewExpense(expense);
            return Json
                        (new { newLocation = "/Home/Index/" });
        }
    }
}
