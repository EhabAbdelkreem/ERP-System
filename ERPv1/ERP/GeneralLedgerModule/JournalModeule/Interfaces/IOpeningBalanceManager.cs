using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces
{
    public interface IOpeningBalanceManager
    {
        OpeningTransaction NewOpeningTrans();
        void SaveOpeningBalance(OpeningTransaction vm);
    }
}