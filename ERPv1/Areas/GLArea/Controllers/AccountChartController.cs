using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.GLArea.Controllers
{
    [Area("GLArea")]
    public class AccountChartController : Controller
    {
        private readonly IAccountGenerator _accountGenerator;
        private readonly IAccountListManager _accountListManager;
        private readonly IAccountUpdateChecksManager _accountUpdateChecks;

        public AccountChartController(IAccountGenerator accountGenerator, 
            IAccountListManager accountListManager,IAccountUpdateChecksManager accountUpdateChecks)
        {
            _accountGenerator = accountGenerator;
            _accountListManager = accountListManager;
            _accountUpdateChecks = accountUpdateChecks;
        }

        public IActionResult Index()
        {
            var vm = _accountListManager.GetAllAccount();
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount(CreateAccountVM vm)
        {
            if(ModelState.IsValid)
            {
                _accountGenerator.GenerateAccount(vm);
                return RedirectToAction(nameof(this.Index));
            }
            return View(vm);
        }

        public IActionResult Edit(string id)
        {
            var account = _accountListManager.GetAccount(id);
            return View(account);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (UpdateAccountVM account)
        {
            if(ModelState.IsValid)
            {
                _accountListManager.UpdateAccount(account);
                return RedirectToAction(nameof(this.Index));
            }
            return View(account);
        }


        public IActionResult VerifyCurrecny(string accNum, int CurrencyId)
        {
            if (!_accountUpdateChecks.ValidateCurrecny(accNum, CurrencyId))
                return Json($"الرصيد أكير من صفر . لايمكن تعديل العملة");
            return Json(true);
        }

        public IActionResult VerifyBranch(string accNum, int BranchId)
        {
            if (!_accountUpdateChecks.ValidateBranch(accNum, BranchId))
                return Json($"الرصيد أكير من صفر . لايمكن تعديل الفرع");
            return Json(true);
        }
       
    }
}
