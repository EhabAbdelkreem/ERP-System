using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreItemsController : Controller
    {
        private readonly IStoreItemManager _storeItemManager;

        public StoreItemsController(IStoreItemManager storeItemManager)
        {
            _storeItemManager = storeItemManager;
        }
        public async Task<IActionResult> Index()
        {
            var StoreItems = await _storeItemManager.GetAllStoreItems();
            return View(StoreItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreItemCreationVM vm)
        {
            if(ModelState.IsValid)
            {
                _storeItemManager.CreateStoreItem(vm);
                return RedirectToAction(nameof(this.Index));
            }
            return View(vm);
        }
    }
}
