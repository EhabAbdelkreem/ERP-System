using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Model
{
    [Table("Finance_Supplier_Purchase")]

    public class Purchase
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Contacts SupplierDetails { get; set; }
        [Column(TypeName = "Date")]
        public DateTime PurchaseDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public bool IsVAT { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VATAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmountWithVAT { get; set; }
        public bool IsFullyPaid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Paid { get; set; }
        public string InvoiceNum { get; set; }

        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
    }
}
