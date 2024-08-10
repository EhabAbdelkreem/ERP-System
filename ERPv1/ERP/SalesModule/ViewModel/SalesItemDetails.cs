using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel
{
    public class SalesItemDetails
    {
        public int StoreItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        //public decimal Total { get; set; }
    }
}
