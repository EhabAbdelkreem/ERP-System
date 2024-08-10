using ERPv1.ERP.CurrentAssetModules.ChecksModule.Services;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.CheckInBank;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.ChecksInSafe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.SalesArea.Controllers
{
    [Area("SalesArea")]
    public class NRController : Controller
    {
        private readonly NRManager _nRManager;

        public NRController(NRManager nRManager)
        {
            _nRManager = nRManager;
        }
        public IActionResult CheckInSafe()
        {
            var vm = _nRManager.GetChecksInSafe();
            return View(vm);
        }

        public JsonResult MoveToBank([FromBody] CheckInSafeContainer  vm)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                try
                {
                    _nRManager.MoveToBank(vm);

                    return Json(new
                    {
                        newLocation
                        = "/SalesArea/NR/CheckInSafe"
                    });
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    return Json(new { errors = errors });
                }

            }
            else
            {
                var errors = ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage).ToList();

                return Json(new { errors = errors });
            }
        }

        public IActionResult CheckInBank()
        {
            var vm = _nRManager.GetCheckInBank();
            return View(vm);
        }


        public JsonResult CollectChecks([FromBody] CheckInBankContainer vm)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                try
                {
                    _nRManager.CollectCheck(vm);

                    return Json(new
                    {
                        newLocation
                        = "/SalesArea/NR/CheckInBank/"
                    });
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    return Json(new { errors = errors });
                }

            }
            else
            {
                var errors = ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage).ToList();

                return Json(new { errors = errors });
            }
        }
    }
}
