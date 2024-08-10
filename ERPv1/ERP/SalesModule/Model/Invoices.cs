using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Model
{
    [Table("Finance_Sales_Invoices")]
    public class Invoices
    {
        [StringLength(6),Key]
        public string InoviceNum { get; set; }

        public int InvoiceCount { get; set; }

        public int ContactId { get; set; }
        public Contacts ContactDetails { get; set; }
        [Column(TypeName = "Date")]
        public DateTime InvoiceDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public bool IsVAT { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VATAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWithVAT { get; set; }

    }
}
