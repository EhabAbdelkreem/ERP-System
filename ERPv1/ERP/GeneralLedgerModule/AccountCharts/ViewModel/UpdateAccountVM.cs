using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel
{
    public class UpdateAccountVM
    {
        [Display(Name = "رقم الحساب")]
        public string AccNum { get; set; }
        [Display(Name = "الرصيد")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "برجاء اضافة اسم الحساب"), StringLength(150)]
        [Display(Name = "اسم الحساب")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "برجاء اضافة اسم الحساب باللغة العربي"), StringLength(150)]
        [Display(Name = "اسم الحساب (ع)")]
        public string AccountNameAr { get; set; }

        [Required(ErrorMessage = "برجاء اختيار العملة")]
        [Display(Name = "العملة")]
        [Remote(action: "VerifyCurrecny", controller: "AccountChart", areaName: "GLArea", AdditionalFields = nameof(AccNum))]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "برجاء اختيار الفرع")]
        [Display(Name = "الفرع")]
        [Remote(action: "VerifyBranch", controller: "AccountChart", areaName: "GLArea", AdditionalFields = nameof(AccNum))]
        public int BranchId { get; set; }

        public bool IsActive{ get; set; }
    }
}
