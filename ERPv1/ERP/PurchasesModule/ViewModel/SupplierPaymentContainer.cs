using ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel
{
    public class SupplierPaymentContainer:IValidatableObject
    {
        public SupplierPaymentContainer()
        {
            PaymentDetails = new PaymentDetails();
            SupplierBalanceDetails = new List<SupplierBalanceDetails>();
            SelectedBalance = new SupplierBalanceDetails();
            SupplierData = new SupplierData();
        }
        public PaymentDetails PaymentDetails { get; set; }
        public List<SupplierBalanceDetails> SupplierBalanceDetails { get; set; }
        public SupplierBalanceDetails SelectedBalance { get; set; }
        public SupplierData SupplierData { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            DateTime payDate;
            var IsValidDate = DateTime.TryParse(PaymentDetails.PaymentDate, out payDate);
            if(!IsValidDate)
            {
                errors.Add(new ValidationResult("برجاء اضافى تاريخ دفع صيحيح"));
            }
            if (PaymentDetails.PaymentAmount <= 0)
                errors.Add(new ValidationResult("برجاء اضافة المبلغ المدفوع بشكل صحيح"));
            if(PaymentDetails.PaymentAmount > SelectedBalance.Amount)
                errors.Add(new ValidationResult("لا يمكنك دفع مبلغ أكبر من المدينونية"));
            
            if(PaymentDetails.PaymentMethod == SupplierPaymentMethod.Safe && 
                string.IsNullOrEmpty(PaymentDetails.SafeAccNum))
            {
                errors.Add(new ValidationResult("برجاء اختيار رقم حساب الخزنة"));
            }

            if (PaymentDetails.PaymentMethod == SupplierPaymentMethod.Bank &&
               string.IsNullOrEmpty(PaymentDetails.BankAccNum))
            {
                errors.Add(new ValidationResult("برجاء اختيار رقم حساب البنك"));
            }

            if (PaymentDetails.PaymentMethod == SupplierPaymentMethod.Check)
            {
                if(string.IsNullOrEmpty(PaymentDetails.BankAccNum))
                    errors.Add(new ValidationResult("برجاء اختيار رقم حساب البنك"));
                if(string.IsNullOrEmpty(PaymentDetails.CheckNum))
                    errors.Add(new ValidationResult("برجاء اضافة رقم الشيك"));

                if (string.IsNullOrEmpty(PaymentDetails.PaymentDueDate))
                {
                    errors.Add(new ValidationResult("برجاء اضافة تاريخ الأستحقاق"));
                }
                else
                {
                    DateTime dueDate;
                    var isValidDueDate = DateTime.TryParse(PaymentDetails.PaymentDueDate, out dueDate);
                    if(!isValidDueDate)
                        errors.Add(new ValidationResult("برجاء اضافى تاريخ استحقاق صحيح"));

                }
                 
                if (string.IsNullOrEmpty(PaymentDetails.WritingDate))
                {
                    errors.Add(new ValidationResult("برجاء اضافة تاريخ التحرير"));
                }
                else
                {
                    DateTime writingDate;
                    var isValidWritingDate = DateTime.TryParse(PaymentDetails.WritingDate, out writingDate);
                    if (!isValidWritingDate)
                        errors.Add(new ValidationResult("برجاء اضافى تاريخ تحرير صحيح"));
                }
                    
            }


            return errors;
        }
    }
}
