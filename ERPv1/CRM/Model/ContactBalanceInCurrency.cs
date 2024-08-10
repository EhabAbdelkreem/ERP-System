using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Model
{
    [Table("CRM_ContactBalanceInCurrency")]
    public class ContactBalanceInCurrency
    {
        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public Contacts Contacts { get; set; }

        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [StringLength(50)]
        public string AccNum { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}
