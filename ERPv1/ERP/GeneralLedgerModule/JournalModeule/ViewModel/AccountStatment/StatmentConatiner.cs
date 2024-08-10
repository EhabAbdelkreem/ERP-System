using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel.AccountStatment
{
    public class StatmentConatiner
    {
        public StatmentConatiner()
        {
            StatmentParams = new StatmentParams();
            Transactions = new List<StatmentTransactions>();
        }
        public string ReportURL { get; set; }
        public StatmentParams StatmentParams { get; set; }
        public List<StatmentTransactions> Transactions { get; set; }
    }
}
