using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.Expenses
{
    public class ExpenseVM : IValidatableObject
    {
        public ExpenseVM()
        {
            ExpenseDetails = new ExpenseDetailsVM();
            PaymentDetails = new PaymentDetails();
            Messages = new List<string>();
        }
        public ExpenseDetailsVM ExpenseDetails { get; set; }
        public PaymentDetails PaymentDetails { get; set; }

        public bool IsWaitingAreaVisible { get; set; } // Upload data => load view
        public bool IsDetailAreaVisible { get; set; } // Normal View
        public bool IsMessageAreaVisible { get; set; } // Show In case an error

        public string SaveURL { get; set; }
        public List<string> Messages { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if(PaymentDetails.PaymentMethod == Model.SupplierPaymentMethod.Credit)
            {
                if (!ExpenseDetails.SupplierId.HasValue)
                    errors.Add(new ValidationResult("برجاء اختيار المورد"));
            }
            if(PaymentDetails.PaymentAmount < ExpenseDetails.Amount)
            {
                if (!ExpenseDetails.SupplierId.HasValue)
                    errors.Add(new ValidationResult("برجاء اختيار المورد"));
            }
                
            return errors;
        }
    }
}
