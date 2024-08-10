using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel
{
    public class CreateAccountVM
    {
        [Required(ErrorMessage ="برجاء اضافة اسم الحساب"), StringLength(150)]
        [Display(Name ="اسم الحساب")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "برجاء اضافة اسم الحساب باللغة العربي"), StringLength(150)]
        [Display(Name = "اسم الحساب (ع)")]
        public string AccountNameAr { get; set; }
        [Required(ErrorMessage = "برجاء اختيار نوع الحساب")]
        [Display(Name = "نوع الحساب")]
        public int AccTypeId { get; set; }

        [Required(ErrorMessage = "برجاء اختيار العملة")]
        [Display(Name = "العملة")]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "برجاء اختيار الفرع")]
        [Display(Name = "الفرع")]
        public int BranchId { get; set; }
    }
}
