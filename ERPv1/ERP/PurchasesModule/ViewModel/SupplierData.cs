using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.PurchasesModule.ViewModel
{
    public class SupplierData
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
    }
}
