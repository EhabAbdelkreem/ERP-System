using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Services;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class NPController : Controller
    {
        private readonly NotesPayableManager _notesPayableManager;

        public NPController(NotesPayableManager notesPayableManager)
        {
            _notesPayableManager = notesPayableManager;
        }
        public IActionResult ManageNP()
        {
            var vm = _notesPayableManager.GetAllNP();
            return View(vm);
        }

        public JsonResult CollectCheck([FromBody]NPDetails np)
        {
            _notesPayableManager.CollectNP(np);
            return Json
                        (new { newLocation = "/Home/Index/" });
        }

        public JsonResult MoveToCashCollection([FromBody]NPDetails np)
        {
            _notesPayableManager.MoveCheckToCashPayment(np);
            return Json
                       (new { newLocation = "/Home/Index/" });
        }

        public JsonResult CollectCashCollection([FromBody] NPContainer vm)
        {
            _notesPayableManager.CollectCashNP(vm.SelectedNote, vm.PaymentDetails);
            return Json
                       (new { newLocation = "/Home/Index/" });
        }
    }
}
