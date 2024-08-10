using ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.ReturnBack;
using Microsoft.AspNetCore.Http;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface IPurchaseManager
    {
        PurchaseContainer NewPurchase(int SupplierId);
        void SavePurchase(PurchaseContainer vm, IFormFile Invoicefile);
        PurchaseReturnBackContainer GetPurchaseData(int PurchaseId);
    }
}