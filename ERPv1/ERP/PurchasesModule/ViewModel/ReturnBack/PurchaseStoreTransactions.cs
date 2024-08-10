using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.ReturnBack
{
    public class PurchaseStoreTransactions
    {
        public int StoreItemId { get; set; }
        public string StoreItemName { get; set; }
        public decimal QTY { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReturnedQTY { get; set; }
    }
}
