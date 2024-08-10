using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel.TrailBalance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.Services
{
    public class TrailBalanceManager
    {
        private readonly ApplicationDbContext _db;

        public TrailBalanceManager(ApplicationDbContext db)
        {
            _db = db;
        }

        
        
        //3- Foreach child account openbalance end balance TotalDebit TotalCredit
        //4-

        public void BuildTrailBalanceParent(TrailBalanceContainer vm)
        {
            DateTime StartDate = DateTime.Parse(vm.TrailBalanceParams.StartDate);
            DateTime EndDate = DateTime.Parse(vm.TrailBalanceParams.EndDate).AddDays(1);
            //1- List All Parent Account
            List<AccountChart> accounts = _db.AccountChart.Include(x => x.Currency)
                                            .Where(x=>x.IsParent == true).ToList();
            
            

            foreach (var item in accounts)
            {
                var trailitem = new TrailBalanceItem();
                trailitem.AccNum = item.AccNum;
                trailitem.AccName = item.AccountName;
                //2- foreach Parent Account => List child account 
                var ChildAccounts = _db.AccountChart.Include(x => x.Currency)
                            .Where(x => x.ParentAcNum == item.AccNum && x.IsParent == false);
                decimal TotalOpenening = 0;
                decimal TotalClosing = 0;
                TransSummary TotalForParent = new TransSummary();
                foreach (var child in ChildAccounts)
                {
                    // 3- Openinig balance Total
                    TotalOpenening = TotalOpenening + OpeningBalanceChild(StartDate, child.AccNum);
                    // 4- Closing balance Total
                    TotalClosing = TotalClosing + ClosingBalanceChild(EndDate, child.AccNum);
                    // Foreach child Total Debit and Total Credit
                    TransSummary TotalForChild = BuildTotalChild(StartDate, EndDate, child.AccNum);
                    // Sum for Child in Parent With Side
                    TotalForParent.Debit = TotalForParent.Debit + TotalForChild.Debit;
                    TotalForParent.Credit = TotalForParent.Credit + TotalForChild.Credit;
                   
                }

                // starting and ending balance
                if (item.AccountNature == AccountNatureEnum.Debit)
                {
                    trailitem.StartingBalanceDebit = TotalOpenening;
                    trailitem.EndingBalanceDebit = TotalClosing;
                }

                if (item.AccountNature == AccountNatureEnum.Credit)
                {
                    trailitem.StartingBalanceCredit = TotalOpenening;
                    trailitem.EndingBalanceCredit = TotalClosing;
                }
                // Total Transactions Amount For debit and credit
                trailitem.TotalAmountDebit = TotalForParent.Debit;
                trailitem.TotalAmountCredit = TotalForParent.Credit;

                vm.TrailBalanceItems.Add(trailitem);
            }
            vm.TotalStartingBalanceDebit = vm.TrailBalanceItems.Sum(x => x.StartingBalanceDebit);
            vm.TotalStartingBalanceCredit = vm.TrailBalanceItems.Sum(x => x.StartingBalanceCredit);
            vm.TotalEndingBalanceDebit = vm.TrailBalanceItems.Sum(x => x.EndingBalanceDebit);
            vm.TotalEndingBalanceCredit = vm.TrailBalanceItems.Sum(x => x.EndingBalanceCredit);
            vm.GrandTotalAmountDebit = vm.TrailBalanceItems.Sum(x => x.TotalAmountDebit);
            vm.GrandTotalAmountCredit = vm.TrailBalanceItems.Sum(x => x.TotalAmountCredit);
        }


        private decimal OpeningBalanceChild(DateTime StartDate, string AccNum)
        {

            var open =_db.JournalDetails.Include(x => x.Trans)
                        .Where(x => x.AccNum == AccNum && x.Trans.TransDate < StartDate)
                        .OrderByDescending(x => x.Trans.TransCount)
                        .Select(x => new { BalanceAfter = x.BalanceAfter }).ToList();
            
            if (open.Count > 0)
                return open.FirstOrDefault().BalanceAfter;
            else
                return 0;

        }

        private decimal ClosingBalanceChild(DateTime EndDate, string AccNum)
        {
            var close = _db.JournalDetails.Include(x => x.Trans)
                       .Where(x => x.AccNum == AccNum && x.Trans.TransDate < EndDate)
                       .OrderByDescending(x => x.Trans.TransCount)                      
                       .Select(x => new { BalanceAfter = x.BalanceAfter }).ToList();

            if (close.Count() > 0)
                return close.FirstOrDefault().BalanceAfter;
            else
                return 0;

        }


        private TransSummary BuildTotalChild(DateTime StartDate, DateTime EndDate, string AccNum)
        {

            var result = _db.JournalDetails.Include(x => x.Trans)
                     .Where(x => x.AccNum == AccNum && x.Trans.TransDate >= StartDate && 
                            x.Trans.EntryDate< EndDate)
                     .Select(x => new {
                         Amount = x.Amount,
                         Side = x.Side,
                     }).ToList();

          

            var Trans = new TransSummary();
            Trans.Debit = result.Where(x => x.Side == TransactionSidesEnum.Debit).Sum(x => x.Amount);
            Trans.Credit = result.Where(x => x.Side == TransactionSidesEnum.Credit).Sum(x => x.Amount);
            return Trans;
        }
    }
}
