using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System.Collections.Generic;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces
{
    public interface IAccountListManager
    {
        IEnumerable<AccountListVM> GetAllAccount();
        UpdateAccountVM GetAccount(string AccNum);
        void UpdateAccount(UpdateAccountVM account);
        AccountChart GetAccountDetails(string AccNum);
    }
}