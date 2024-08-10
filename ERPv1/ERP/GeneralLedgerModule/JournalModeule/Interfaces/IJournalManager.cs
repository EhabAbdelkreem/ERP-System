using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces
{
    public interface IJournalManager
    {
        JournalVM NewJournal();
        string SaveJournal(JournalVM vm);
    }
}