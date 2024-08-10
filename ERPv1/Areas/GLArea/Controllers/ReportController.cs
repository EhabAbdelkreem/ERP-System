using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Services;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel.AccountStatment;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel.TrailBalance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.GLArea.Controllers
{
    [Area("GLArea")]
    public class ReportController : Controller
    {
        private readonly StatmentManager _statmentManager;
        private readonly TrailBalanceManager _trailBalanceManager;

        public ReportController( StatmentManager statmentManager, TrailBalanceManager trailBalanceManager)
        {
            _statmentManager = statmentManager;
            _trailBalanceManager = trailBalanceManager;
        }
        public IActionResult AccountStatment()
        {
            var vm = new StatmentConatiner();
            vm.ReportURL = "/GLArea/Report/BuildStatment";
            return View(vm);
        }

        public JsonResult BuildStatment([FromBody] StatmentParams vm)
        {
            if (ModelState.IsValid)
            {
                var newStatment = new StatmentConatiner();
                newStatment.StatmentParams = vm;
                _statmentManager.UpdateStatement(newStatment);
                return Json(new { result = newStatment });
            }
            else
            {
                return Json(new { newLocation = "/home/index/" });
            }
        }

        public IActionResult TrailBalance()
        {
            var vm = new TrailBalanceContainer();
            vm.ReportURL = "/GLArea/Report/BuildTrail";
            return View(vm);
        }

        public JsonResult BuildTrail([FromBody] TrailBalanceContainer vm)
        {
            if (ModelState.IsValid)
            {
                _trailBalanceManager.BuildTrailBalanceParent(vm);
                return Json(new { result = vm });
            }
            else
            {
                return Json(new { newLocation = "/home/index/" });
            }
        }
    }
}
