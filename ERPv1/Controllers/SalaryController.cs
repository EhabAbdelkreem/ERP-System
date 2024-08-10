using ERPv1.Data;
using ERPv1.HR.Services;
using ERPv1.HR.ViewModel.Payroll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Controllers
{
    public class SalaryController : Controller
    {
        private readonly SalaryBatchManager _salaryBatchManager;
        private readonly ApplicationDbContext _db;

        public SalaryController(SalaryBatchManager salaryBatchManager, ApplicationDbContext db)
        {
            _salaryBatchManager = salaryBatchManager;
            _db = db;
        }
        public IActionResult Index()
        {
            var vm = _db.SalaryBatches.ToList();
            return View(vm);
        }

        public IActionResult ManageBatch(int Id)
        {
            var vm = _salaryBatchManager.StartBatch(Id);
            return View(vm);
        }

        public JsonResult CalcTax ([FromBody] BatchContainer vm)
        {
            _salaryBatchManager.CalcTax(vm.EmployeeSalaries);
            return Json
                       (new { result = vm });
        }

        public JsonResult SaveBatch ([FromBody] BatchContainer vm)
        {
            _salaryBatchManager.SaveSalary(vm);
            return Json(new { newLocation = "/Home/Index" });
        }
    }
}
