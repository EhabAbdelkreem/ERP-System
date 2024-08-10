using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.SupplierStatment
{
    public class StatmentParams
    {
        public int SupplierId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal StartBalance { get; set; }
        public decimal EndBalance { get; set; }
    }
}
