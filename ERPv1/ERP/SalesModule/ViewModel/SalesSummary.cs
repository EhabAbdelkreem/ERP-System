using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel
{
    public class SalesSummary
    {
        public string InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CurrencyId { get; set; }
        public bool IsVAT { get; set; }
        public decimal VATRate { get; set; } = .14M;
        public decimal VATAmount { get; set; }
        public decimal TotalWithVAT { get; set; }
        public string Description { get; set; }
    }
}
