using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Interfaces;
using ERPv1.CRM.ViewModel;
using ERPv1.Data;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Services;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.ReturnBack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class SupplierController : Controller
    {
        private readonly ISupplierGenerationManager _supplierGenerationManager;
        private readonly IPurchaseManager _purchaseManager;
        private readonly SupplierPaymentsManager _supplierPaymentsManager;

        public ApplicationDbContext _db { get; }

        public SupplierController(ISupplierGenerationManager supplierGenerationManager,
                        IPurchaseManager purchaseManager, ApplicationDbContext db,
                        SupplierPaymentsManager supplierPaymentsManager)
        {
            _supplierGenerationManager = supplierGenerationManager;
            _purchaseManager = purchaseManager;
            _supplierPaymentsManager = supplierPaymentsManager;
            _db = db;
        }
        public IActionResult Index()
        {
            var suppliers = _supplierGenerationManager.GetAllSuppliers();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult PurchaseIncovices()
        {
            var vm = _db.Purchases.Include(x=>x.SupplierDetails).Include(x=>x.Currency).ToList();
            return View(vm);
        }

        public IActionResult ReturnBack(int Id)
        {
            var vm = _purchaseManager.GetPurchaseData(Id);
            return View(vm);
        }

        public JsonResult ReturnPurchase([FromBody] PurchaseReturnBackContainer vm)
        {

            return Json
                       (new { newLocation = "/Home/Index/" });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ContactCreatingViewModel supplier)
        {
            if(ModelState.IsValid)
            {
                _supplierGenerationManager.AddNewSupplier(supplier);
                return RedirectToAction(nameof(this.Index));
            }
            return View(supplier);
        }


        public IActionResult NewPurchase(int id)
        {
            var vm = _purchaseManager.NewPurchase(id);
            vm.SaveURL = "/Expenditure/Supplier/SavePurchase";
            return View(vm); 
        }


        public JsonResult GetItemBalance(int id)
        {
            var currentqty = _db.StoreItems.Find(id).Qty;

            return Json(new { CurrentQty = currentqty });
        }


        public JsonResult SavePurchase([FromForm] IFormFile InvoiceFile)
        {
            List<string> errors = new List<string>();
            var body = Request.Form["vm"];
            var model = JsonConvert.DeserializeObject<PurchaseContainer>(body);

            if (ModelState.IsValid)
            {
                try
                {
                    _purchaseManager.SavePurchase(model,InvoiceFile);
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

        public IActionResult SupplierPayment(int id)
        {
            var vm = _supplierPaymentsManager.NewPayment(id);
            return View(vm);
        }

        //SaveSupplierPayment


        public JsonResult SaveSupplierPayment([FromBody] SupplierPaymentContainer vm )
        {
            List<string> errors = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                   _supplierPaymentsManager.SaveSupplierPayment(vm);
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

    }
}
