using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.ReturnBack
{
    public class PurchaseReturnBackContainer
    {
        public PurchaseReturnBackContainer()
        {
            PurchaseDetails = new PurchaseReturnBackDetails();
            PurchaseStoreDetails = new List<PurchaseStoreTransactions>();
        }
        public int PurchaseId { get; set; }
        public PurchaseReturnBackDetails PurchaseDetails { get; set; }
        public List<PurchaseStoreTransactions> PurchaseStoreDetails { get; set; }
    }
}
