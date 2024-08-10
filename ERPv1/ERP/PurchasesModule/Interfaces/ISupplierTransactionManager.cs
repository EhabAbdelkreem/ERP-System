using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface ISupplierTransactionManager
    {
        void PurchaseSupplierTrans(PurchaseContainer vm, int purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter);
        void SupplierReturnTrans(Contacts Supplier, decimal LocalAmount, int CurrencyId, string TransId, decimal BalanceAfter);
    }
}