using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Model
{
    [Table("Finance_Sales_ClientTransaction")]
    public class ClientTransaction
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Contacts ClientDetails { get; set; }
        public string  InvoiceNum { get; set; }
        [ForeignKey("InvoiceNum")]
        public Invoices InvoiceDetails { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentAmount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime PaymentDate { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public string PaymentAccNum { get; set; }
        [ForeignKey("PaymentAccNum")]
        public AccountChart PaymentAccDetails { get; set; }
        public ClientPaymentMethod PaymentMenthod { get; set; }

        public string TransId { get; set; }
        [ForeignKey("TransId")]
        public Journal TransactionDetails { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAfter { get; set; }
    }
}
