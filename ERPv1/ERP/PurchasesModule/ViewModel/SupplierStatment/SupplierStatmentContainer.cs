using ERPv1.ERP.SalesModule.ViewModel.ClientStatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.SupplierStatment
{
    public class SupplierStatmentContainer
    {
        public SupplierStatmentContainer()
        {
            StatmentParams = new StatmentParams();
            Transactions = new List<StatmentTransactions>();
        }
        public string ReportURL { get; set; }
        public StatmentParams StatmentParams { get; set; }
        public List<StatmentTransactions> Transactions { get; set; }
    }
}
