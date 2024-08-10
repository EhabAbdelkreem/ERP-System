using ERPv1.CRM.Model;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface ISupplierBalanceManager
    {
        void AddNewBalanceInCurrency(int SupplierId, string AccNum, int CurrencyId, decimal Amount);
        void ManageSupplierBalanceInCurrency(int SupplierId, string AccNum, int CurrencyId, decimal Amount, bool Plus);
        void UpdateBalanceInCurrency(int SupplierId, string AccNum, int CurrencyId, decimal Amount, bool Plus);
        decimal UpdateSupplierBalance(Contacts supplier, decimal LocalAmount, bool plus);
    }
}