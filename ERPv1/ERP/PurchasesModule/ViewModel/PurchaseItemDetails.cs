using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.PurchasesModule.ViewModel
{
    public class PurchaseItemDetails
    {
        public int StoreItemId { get; set; }
        public decimal QTY { get; set; }
        public decimal UnitPrice { get; set; }
        public string ExpiryDate { get; set; }
        public string SN { get; set; }
    }
}
