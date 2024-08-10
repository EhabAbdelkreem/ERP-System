using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ERPv1.Models;
using ERPv1.HR.Services;
using ERPv1.HR.ViewModel;

namespace ERPv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaxManager _taxManager;

        public HomeController(ILogger<HomeController> logger, TaxManager taxManager)
        {
            _logger = logger;
            _taxManager = taxManager;
        }

        public IActionResult Index()
        {
            var vm = new TestTax();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(TestTax vm )
        {
            ModelState.Clear();
            vm.YearlyTax = _taxManager.CalcTaxUpdated(vm.MonthlySalary, 9000);
            return View(vm);
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
