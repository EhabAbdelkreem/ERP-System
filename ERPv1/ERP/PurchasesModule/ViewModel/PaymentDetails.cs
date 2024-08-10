using ERPv1.ERP.PurchasesModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel
{
    public class PaymentDetails
    {
        public PaymentDetails()
        {
            PaymentMethod = SupplierPaymentMethod.Safe;
            IsSafe = true;
            IsBank = false;
            IsCheck= false;
        }
        [Required]
        public SupplierPaymentMethod PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        [Required]
        public string PaymentDate { get; set; }
        public string SafeAccNum { get; set; }
        public string BankAccNum { get; set; }
        public string CustodyAccNum { get; set; }
        public string CheckNum { get; set; }
        public string WritingDate { get; set; }
        public string PaymentDueDate { get; set; }
        public string Description { get; set; }
        public string RecieptNumber { get; set; }
        public bool IsSafe { get; set; }
        public bool IsBank { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCustody { get; set; }
    }
}
