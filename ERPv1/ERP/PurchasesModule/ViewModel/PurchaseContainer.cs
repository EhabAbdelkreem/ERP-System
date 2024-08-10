using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.PurchasesModule.ViewModel
{
    public class PurchaseContainer
    {
        public PurchaseContainer()
        {
            SupplierData = new SupplierData();
            PurchaseSummary = new PurchaseSummary();
            PurchaseDetails = new List<PurchaseItemDetails>();
            Messages = new List<string>();
            IsWaitingAreaVisible = false;
            IsDetailAreaVisible = true;
            IsMessageAreaVisible = false;

        }
        public SupplierData SupplierData { get; set; }
        public PurchaseSummary PurchaseSummary { get; set; }
        public List<PurchaseItemDetails> PurchaseDetails { get; set; }

        public bool IsWaitingAreaVisible { get; set; } // Upload data => load view
        public bool IsDetailAreaVisible { get; set; } // Normal View
        public bool IsMessageAreaVisible { get; set; } // Show In case an error

        public string SaveURL { get; set; }
        public List<string> Messages { get; set; }

    }
}
