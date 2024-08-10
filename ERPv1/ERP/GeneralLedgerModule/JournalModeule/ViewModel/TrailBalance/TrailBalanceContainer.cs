using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel.TrailBalance
{
    public class TrailBalanceContainer
    {
        public TrailBalanceContainer()
        {
            TrailBalanceItems = new List<TrailBalanceItem>();
            TrailBalanceParams = new TrailBalanceParams();
        }
        public TrailBalanceParams TrailBalanceParams { get; set; }
        public List<TrailBalanceItem> TrailBalanceItems { get; set; }


        public decimal TotalStartingBalanceDebit { get; set; }
        public decimal TotalStartingBalanceCredit { get; set; }
        public decimal GrandTotalAmountDebit { get; set; }
        public decimal GrandTotalAmountCredit { get; set; }

        public decimal TotalEndingBalanceDebit { get; set; }
        public decimal TotalEndingBalanceCredit { get; set; }
        public string ReportURL { get;  set; }
    }
}
