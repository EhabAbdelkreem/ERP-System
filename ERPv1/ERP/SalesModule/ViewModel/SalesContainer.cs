using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel
{
    public class SalesContainer
    {
        public SalesContainer()
        {
            ClientData = new ClientData();
            SalesItemDetails = new List<SalesItemDetails>();
            SalesSummary = new SalesSummary();
        }
        public ClientData ClientData { get; set; }
        public List<SalesItemDetails> SalesItemDetails { get; set; }
        public SalesSummary SalesSummary { get; set; }

        public string SaveURL { get; set; }
    }
}
