using ERPv1.ERP.SalesModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.Payment
{
    public class ClientPaymentDetails
    {
        [Required]
        public ClientPaymentMethod PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        [Required]
        public string PaymentDate { get; set; }
        public string SafeAccNum { get; set; }
        public string BankAccNum { get; set; }
        public string CheckNum { get; set; }
        public string DueDate { get; set; }
        public string OriginalBank { get; set; }
        public string Description { get; set; }
        public string RecieptNumber { get; set; }
        public bool IsSafe { get; set; }
        public bool IsBank { get; set; }
        public bool IsCheck { get; set; }
    }
}
