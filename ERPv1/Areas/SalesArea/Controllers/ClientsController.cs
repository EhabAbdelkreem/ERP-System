using ERPv1.CRM.Services;
using ERPv1.CRM.ViewModel;
using ERPv1.ERP.SalesModule.Service;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.ClientStatment;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.SalesArea.Controllers
{
    [Area("SalesArea")]
    public class ClientsController : Controller
    {
        private readonly ClientGenerationManager _clientGenerationManager;
        private readonly SalesManager _salesManager;
        private readonly ClientPayamentManager _clientPayamentManager;
        private readonly ClientReports _clientReports;

        public ClientsController(ClientGenerationManager clientGenerationManager,
            SalesManager salesManager, ClientPayamentManager clientPayamentManager,
            ClientReports clientReports)
        {
            _clientGenerationManager = clientGenerationManager;
            _salesManager = salesManager;
            _clientPayamentManager = clientPayamentManager;
            _clientReports = clientReports;

        }
        public IActionResult Index()
        {
            return View(_clientGenerationManager.GetAllClients());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ContactCreatingViewModel client)
        {
            if (ModelState.IsValid)
            {
                var feedback = _clientGenerationManager.AddNewClient(client);

                if (feedback.Done)
                    return RedirectToAction(nameof(this.Index));
                else{
                    foreach (var item in feedback.Errors)
                    {
                        ModelState.AddModelError("error", item);
                    }
                    return View(client);
                }
            }
            return View(client);
        }

        public IActionResult NewSale(int id)
        {
            var vm = _salesManager.NewSale(id);
            vm.SaveURL = "/SalesArea/Clients/SaveNewSale";
            return View(vm);
        }
        public JsonResult SaveNewSale([FromBody] SalesContainer vm)
        {
            List<string> errors = new List<string>();
           

            if (ModelState.IsValid)
            {
                try
                {
                    _salesManager.SaveNewSale(vm);
                    return Json
                        (new { newLocation = "/Home/Index/" });
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    errors.Add("Please Contact System Admin");
                    return Json(new { errors = errors });
                }

            }
            else
            {
                errors.AddRange(ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));

                return Json(new { errors = errors });
            }
        }


        public IActionResult ClientPayment(int id)
        {
            var vm = _clientPayamentManager.NewPayment(id);
            return View(vm);
        }

        public JsonResult SaveClientPayment([FromBody] ClientPaymentContainer vm)
        {
           
                _clientPayamentManager.SaveClientPayment(vm);
                return Json
                    (new { newLocation = "/Home/Index/" });
           
        }


        public IActionResult ClientStatment()
        {
            var vm = new ClientStatmentContainer();
            vm.ReportURL = "/SalesArea/Clients/BuildClientStatment";
            return View(vm);
        }


        public JsonResult BuildClientStatment([FromBody] StatmentParams vm)
        {
            if (ModelState.IsValid)
            {
                var newStatment = new ClientStatmentContainer();
                newStatment.StatmentParams = vm;
                _clientReports.UpdateStatement(newStatment);
                return Json(new { result = newStatment });
            }
            else
            {
                return Json(new { newLocation = "/home/index/" });
            }
        }
    }
}
