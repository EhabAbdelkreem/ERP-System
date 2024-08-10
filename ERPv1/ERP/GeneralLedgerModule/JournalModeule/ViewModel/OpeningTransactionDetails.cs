using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel
{
    public class OpeningTransactionDetails
    {
        public string AccNum { get; set; }
        public string AccountName { get; set; }
        public TransactionSidesEnum Side { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal UsedRate { get; set; }
        public string  CurrecnyAbbr { get; set; }
        public int CurrencyId { get; set; }
    }
}
