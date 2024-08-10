using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel
{
    public class AccountListVM
    {
        [Display(Name ="رقم الحساب")]
        public string AccNum { get; set; }
        [Display(Name = "اسم الحساب")]
        public string AccountName { get; set; }
        [Display(Name = "اسم الحساب عربي")]
        public string AccountNameAr { get; set; }
        [Display(Name = "نوع الحساب")]
        public string AccTypeName { get; set; }
        [Display(Name = "الرصيد")]
        public decimal Balance { get; set; }
        [Display(Name = "الرصيد الافتتاحي")]
        public decimal StartingBalance { get; set; }
        [Display(Name = " حساب رئيسي")]
        public bool IsParent { get; set; }
        [Display(Name = "العملة")]
        public string CurrencyAbbr { get; set; }
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        [Display(Name = "الفرع")]
        public string BranchName { get; set; }

    }
}
