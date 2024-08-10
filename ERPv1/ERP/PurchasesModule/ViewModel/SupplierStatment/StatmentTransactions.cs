using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.SupplierStatment
{
    public class StatmentTransactions
    {
        public string TransDate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Description { get; set; }
        public decimal BalanceAfter { get; set; }
        public int? PurchaseId { get; set; }
    }
}
