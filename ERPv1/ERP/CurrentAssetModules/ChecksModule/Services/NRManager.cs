using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.Model;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.CheckInBank;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.ChecksInSafe;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Services;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.Services
{
    public class NRManager
    {
        private readonly JournalManager _journal;
        private readonly ApplicationDbContext _db;

        public NRManager(ApplicationDbContext db, JournalManager journal)
        {
            _journal = journal;
            _db = db;
        }

        public void AddNewCheck(ClientPaymentContainer vm , Currency Currency, string TransId)
        {
            var Check = new Model.Check();
            Check.ChkNum = vm.PaymentDetails.CheckNum;
            Check.DueDate = DateTime.Parse(vm.PaymentDetails.DueDate);
            Check.CurrencyId = vm.SelectedBalance.CurrencyId;
            Check.AmountLocal = vm.PaymentDetails.PaymentAmount * Currency.Rate;
            Check.AmountForgin = vm.PaymentDetails.PaymentAmount;
            Check.ContactId = vm.ClientData.ClientId;
            Check.OrginalBank = vm.PaymentDetails.OriginalBank;
            Check.Paid = 0;
            Check.UnPaid = vm.PaymentDetails.PaymentAmount;
            Check.CheckStatusId = _db.CheckStatus.FirstOrDefault(x => x.IsDefault).Id;
            Check.CheckLocationId = _db.CheckLocation.FirstOrDefault(x => x.IsDefualt).Id;
            _db.Check.Add(Check);
            _db.SaveChanges();

            _db.CheckHistory.Add(new Model.CheckHistory()
            {
                ChkNum = Check.ChkNum,
                Description = $" {vm.PaymentDetails.CheckNum } شيك جديد",
                TransDate = vm.PaymentDetails.PaymentDate.ToEgyptionDate(),
                TransID = TransId
            }) ;
            _db.SaveChanges();
        }


        public CheckInSafeContainer GetChecksInSafe()
        {
            var vm = new CheckInSafeContainer();
            var CheckStatus = _db.CheckStatus.FirstOrDefault(x => x.IsDefault).Id;
            vm.CheckDetails = _db.Check.Include(x => x.Contact)
                                .Include(x => x.CheckStatus).Include(x=>x.Currency)
                                .Where(x => x.CheckStatusId == CheckStatus && x.CheckLocationId == 1)
                                .Select(x => new CheckInSafeDetails()
                                {
                                    CheckAmount = x.AmountLocal,
                                    CheckAmountForiegn = x.AmountForgin,
                                    CurrencyAbbr = x.Currency.CurrencyAbbrev,
                                    CheckNum = x.ChkNum,
                                    ClientName = x.Contact.Name,
                                    CheckStatus = x.CheckStatus.CheckStatusAR,
                                    DueDate = x.DueDate.Value.ToShortDateString(),
                                    OrginalBank = x.OrginalBank,
                                    Paid = x.Paid,
                                    Selected = false,
                                    UnPaid = x.UnPaid
                                }).ToList();


            return vm;
        }

        public void MoveToBank(CheckInSafeContainer vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    // Create Hafza
                    CheckHafza Hafaza = AddNewHafza(vm);
                    // Select check Selected = true
                    var SelectedChecks = vm.CheckDetails.Where(x => x.Selected);
                    // Total selected Checks
                    var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);
                    // Journal Transaction
                    string TransactionId = MoveToBankJournal(vm, CheckTotalAmount);
                    // Update Check Table
                    UpdateMovedToBankChecks(vm, Hafaza, SelectedChecks, TransactionId);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    transaction.Rollback();
                }

            }

           
        }


        private void UpdateMovedToBankChecks(CheckInSafeContainer vm, CheckHafza Hafaza,
            IEnumerable<CheckInSafeDetails> SelectedChecks, string TransactionId)
        {
            foreach (var item in SelectedChecks)
            {
                var chk = _db.Check.FirstOrDefault(x => x.ChkNum == item.CheckNum);
                chk.CheckLocationId = 2;
                chk.HafzaId = Hafaza.Id;
                chk.BankAccNum = vm.HafzaDetails.BankAccNum;
               _db.CheckHistory.Add(new CheckHistory()
                {
                    ChkNum = item.CheckNum,
                    TransDate = Hafaza.HafzaDate,
                    TransID = TransactionId,
                    Description = "Check Moved To Bank"
                });
            }
        }

        private string MoveToBankJournal(CheckInSafeContainer vm, decimal CheckTotalAmount)
        {
            var journal = new JournalVM();
            

            journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
            journal.TransDesc = "Check Moved To bank by hazfa name: " + vm.HafzaDetails.HafzaName;
        
            var JD_DebitSide = new JournalDetailsVM();
            JD_DebitSide.AccNum = "1240000002";
            JD_DebitSide.Debit = CheckTotalAmount;
            JD_DebitSide.Side = TransactionSidesEnum.Debit;
            JD_DebitSide.CurrencyId = 1;
            journal.TransactionDetails.Add(JD_DebitSide);

            var JD_CreditSide = new JournalDetailsVM();
            JD_CreditSide.AccNum = "1240000001";
            JD_CreditSide.Credit = CheckTotalAmount;
            JD_CreditSide.Side = TransactionSidesEnum.Credit;
            JD_CreditSide.CurrencyId = 1;
            journal.TransactionDetails.Add(JD_CreditSide);
            string TransactionId = _journal.SaveJournal(journal);
            return TransactionId;
        }

        private CheckHafza AddNewHafza(CheckInSafeContainer vm)
        {
            var Hafaza = new CheckHafza();
            Hafaza.BankAccNum = vm.HafzaDetails.BankAccNum;
            Hafaza.HafzaDate = vm.HafzaDetails.HafzaDate.ToEgyptionDate();
            Hafaza.HafzaName = vm.HafzaDetails.HafzaName;
            _db.CheckHafza.Add(Hafaza);
            _db.SaveChanges();
            return Hafaza;
        }



        public CheckInBankContainer GetCheckInBank()
        {
            var vm = new CheckInBankContainer();

            vm.CheckDetails = _db.Check.Include(x => x.Contact)
                                .Include(x => x.CheckStatus)
                                .Where(x => x.CheckStatusId == 1 && x.CheckLocationId == 2)
                                .Select(x => new CheckInBankDetails()
                                {
                                    CheckAmount = x.AmountLocal,
                                    CheckNum = x.ChkNum,
                                    ClientName = x.Contact.Name,
                                    CheckStatus = x.CheckStatus.CheckStatusAR,
                                    DueDate = x.DueDate.Value.ToShortDateString(),
                                    OrginalBank = x.OrginalBank,
                                    Selected = false,
                                    BankAccountName = x.BankAcc.AccountName,
                                    BankAccountNumber = x.BankAccNum
                                }).ToList();


            return vm;
        }


        public void CollectCheck(CheckInBankContainer vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                  
                    // Select check Selected = true
                    var SelectedChecks = vm.CheckDetails.Where(x => x.Selected);
                    // Total selected Checks
                    var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);
                    // Journal Transaction
                    string TransactionId = CollectionJournal(SelectedChecks);
                    // Update Check Table
                    UpdateCollectedCheckInfo(SelectedChecks, TransactionId);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    transaction.Rollback();
                }

            }


        }

        private void UpdateCollectedCheckInfo(IEnumerable<CheckInBankDetails> SelectedChecks, string TransId)
        {
            foreach (var item in SelectedChecks)
            {
                var chk = _db.Check.FirstOrDefault(x => x.ChkNum == item.CheckNum);
                chk.CheckStatusId = 2;
                chk.CheckLocationId = 3;
                _db.CheckHistory.Add(new CheckHistory()
                {
                    ChkNum = item.CheckNum,
                    TransDate = DateTime.Now,
                    TransID = TransId,
                    Description = "Check Number " + item.CheckNum + " is collected"
                });

            }
        }
        private string CollectionJournal(IEnumerable<CheckInBankDetails> SelectedCheck)
        {
            var journal = new JournalVM();
            var CheckTotalAmount = SelectedCheck.Sum(x => x.CheckAmount);
            var SelectedByBank = SelectedCheck.GroupBy(x => new { x.BankAccountNumber })
                                    .Select(x => new
                                    {
                                        BankAccountNum = x.Key.BankAccountNumber,
                                        TotalAmount = x.Sum(y => y.CheckAmount)
                                    });

            journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
            journal.TransDesc = "Check Colection " ;

            foreach (var item in SelectedByBank)
            {
                var JD_DebitSide = new JournalDetailsVM();
                JD_DebitSide.AccNum = item.BankAccountNum;
                JD_DebitSide.Debit = item.TotalAmount;
                JD_DebitSide.Side = TransactionSidesEnum.Debit;
                JD_DebitSide.CurrencyId = 1;
                journal.TransactionDetails.Add(JD_DebitSide);
            }

            

            var JD_CreditSide = new JournalDetailsVM();
            JD_CreditSide.AccNum = "1240000002";
            JD_CreditSide.Credit = CheckTotalAmount;
            JD_CreditSide.Side = TransactionSidesEnum.Credit;
            JD_CreditSide.CurrencyId = 1;
            journal.TransactionDetails.Add(JD_CreditSide);
            string TransactionId = _journal.SaveJournal(journal);
            return TransactionId;
        }
    }
}
