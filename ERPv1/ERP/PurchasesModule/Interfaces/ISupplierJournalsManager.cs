using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.PurchasesModule.ViewModel.ReturnBack;
using Microsoft.AspNetCore.Http;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface ISupplierJournalsManager
    {
        string PurchaseJournal(PurchaseContainer vm, Contacts Contact, IFormFile Invoice);
        string PurchaseReturnJournal(PurchaseReturnBackContainer vm, Contacts Contact, Currency Currency, bool IsVAT, decimal TotalInLocal, decimal TotalAmountWithVat);
    }
}