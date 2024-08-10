using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces
{
    public interface IAccountGenerator
    {
        string GenerateAccount(CreateAccountVM newAccount);
        string CreateNewAccount(CreateAccountVM newAccount);
    }
}