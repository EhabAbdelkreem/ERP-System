using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.GLArea.Controllers
{
    [Area("GLArea")]
    public class OpenBalanceController : Controller
    {
        private readonly IJournalManager _journalManager;
        private readonly IOpeningBalanceManager _openingBalanceManager;
        private readonly IAccountListManager _accountListManager;

        public OpenBalanceController(IOpeningBalanceManager openingBalanceManager, 
            IJournalManager journalManager, IAccountListManager accountListManager)
        {
            _journalManager = journalManager;
            _openingBalanceManager = openingBalanceManager;
            _accountListManager = accountListManager;
        }
        public IActionResult NewBalance()
        {
            var vm = _openingBalanceManager.NewOpeningTrans();
            return View(vm);
        }
        
        public JsonResult SaveOpeningBalance([FromBody] OpeningTransaction vm)
        {
            if(ModelState.IsValid)
            {
                _openingBalanceManager.SaveOpeningBalance(vm);
                return Json(new { newLocation = "/GLArea/AccountChart/Index" });
            }
            else
            {
                return Json(new { newLocation = "/home/index/" });
            }
        }


        public IActionResult CreateJounral()
        {
            return View(_journalManager.NewJournal());
        }

        public JsonResult GetAccountDetails([FromBody] string Id)
        {
            if(!string.IsNullOrEmpty(Id))
            {
                var acc =_accountListManager.GetAccountDetails(Id);
                return Json(new { Account = acc });
            }
            return Json(new { error = "Please Choose Account" });
        }

        public JsonResult SaveJournal([FromBody] JournalVM vm)
        {
            if(ModelState.IsValid)
            {
                _journalManager.SaveJournal(vm);
                return Json(new { newLocation = "/GLArea/AccountChart/Index" });
            }
            else
            {
                var errors = ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage);

                
                return Json(new { errors = errors });
            }
        }

    }
}
