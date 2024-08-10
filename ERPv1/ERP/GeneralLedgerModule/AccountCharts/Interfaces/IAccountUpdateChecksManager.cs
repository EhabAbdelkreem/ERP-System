using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System.Collections.Generic;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces
{
    public interface IAccountUpdateChecksManager
    {
        IEnumerable<string> ValidateAccountData(UpdateAccountVM vm);
        bool ValidateBranch(string AccNum, int BranchId);
        bool ValidateCurrecny(string AccNum, int CurrecnyId);
    }
}