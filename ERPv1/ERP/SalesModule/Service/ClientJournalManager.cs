using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Service
{
    public class ClientJournalManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IUploadManager _uploadManager;
        private readonly IJournalManager _journalManager;

        public ClientJournalManager(ApplicationDbContext db, IUploadManager uploadManager,
            IJournalManager journalManager)
        {
            _db = db;
            _uploadManager = uploadManager;
            _journalManager = journalManager;
        }

        public string IncomeJournal(SalesContainer vm, Contacts Contact, IFormFile Invoice)
        {
            var journal = new JournalVM();
            var Currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
            journal.TransDate = vm.SalesSummary.InvoiceDate;
            journal.TransDesc = vm.SalesSummary.Description;
            if (Invoice != null)
            {
                journal.DocName = _uploadManager.UploadedFile(Invoice, "FinanceFiles");
            }
            // Debit Client Credit Income
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = Contact.ClientAccNum;
            JD_Debit.Side = TransactionSidesEnum.Debit;
            JD_Debit.Debit = vm.SalesSummary.TotalWithVAT;
            JD_Debit.CurrencyId = vm.SalesSummary.CurrencyId;
            JD_Debit.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_Debit);


            if (vm.SalesSummary.IsVAT)
            {
                var JD_VAT = new JournalDetailsVM();
                JD_VAT.AccNum = "2220000001";
                JD_VAT.Side = TransactionSidesEnum.Credit;
                JD_VAT.Credit = vm.SalesSummary.VATAmount;
                JD_VAT.CurrencyId = vm.SalesSummary.CurrencyId;
                JD_VAT.UsedRate = Currency.Rate;
                journal.TransactionDetails.Add(JD_VAT);
            }
            var JD_Income = new JournalDetailsVM();
            JD_Income.AccNum = "3110000001";
            JD_Income.Side = TransactionSidesEnum.Credit;
            JD_Income.Credit = vm.SalesSummary.TotalAmount;
            JD_Income.CurrencyId = vm.SalesSummary.CurrencyId;
            JD_Income.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_Income);
            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public void PurchaseJournal(SalesContainer vm, string InvoiceNum)
        {
            var journal = new JournalVM();
            var Currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
            journal.TransDate = vm.SalesSummary.InvoiceDate;
            journal.TransDesc = vm.SalesSummary.Description;
            journal.TransactionDetails.AddRange(PurchaseJournalDetails(vm, Currency, InvoiceNum));
            var TransId = _journalManager.SaveJournal(journal);

        }



        public List<JournalDetailsVM> PurchaseJournalDetails(SalesContainer vm, Currency Currency, string InvoiceNum)
        {
            List<JournalDetailsVM> JDs = new List<JournalDetailsVM>();
            foreach (var item in vm.SalesItemDetails)
            {
                var StoreItem = _db.StoreItems.Find(item.StoreItemId);
                var StoreItemBalances = _db.StoreItemBalanceDetails.Include(x => x.StoreTransaction)
                    .Include(x=> x.StoreTransaction.PurchaseDetails.Currency)
                    .Where(x => x.StoreItemId == item.StoreItemId && x.CurrentBalance > 0).ToList();

                decimal Total = 0;
                switch (StoreItem.StoreSystem)
                {
                    case StoreSystem.FIFO:
                        Total = GetTotal(StoreItemBalances.OrderBy(x => x.Id).ToList(), StoreItem, item);
                        break;
                    case StoreSystem.LIFO:
                        Total = GetTotal(StoreItemBalances.OrderByDescending(x => x.Id).ToList(), StoreItem, item);
                        break;
                    case StoreSystem.Averge:
                        Total = GetTotal(StoreItemBalances.OrderBy(x => x.Id).ToList(), StoreItem, item);
                        break;
                    default:
                        break;
                }
                // Purchase Journal Detail And Update Store Balance Details
                var JD_Debit = new JournalDetailsVM();
                JD_Debit.AccNum = StoreItem.PurchaseAccNum;
                JD_Debit.Side = TransactionSidesEnum.Debit;
                JD_Debit.Debit = Total;
                JD_Debit.CurrencyId = vm.SalesSummary.CurrencyId;
                JD_Debit.UsedRate = Currency.Rate;
                JDs.Add(JD_Debit);
                var JD_Credit = new JournalDetailsVM();
                JD_Credit.AccNum = StoreItem.StoreAccNum;
                JD_Credit.Side = TransactionSidesEnum.Credit;
                JD_Credit.Credit = Total;
                JD_Credit.CurrencyId = vm.SalesSummary.CurrencyId;
                JD_Credit.UsedRate = Currency.Rate;
                JDs.Add(JD_Credit);
                
                // Update Store Item Balance And Qty
                StoreItem.Qty = StoreItem.Qty - item.Qty;
                StoreItem.Balance = StoreItem.Balance - Total;
                _db.StoreItems.Update(StoreItem);

                // StoreTransaction
                var StoreTrans = new StoreTransaction();
                StoreTrans.InvoiceNum = InvoiceNum;
                StoreTrans.Qty = item.Qty;
                StoreTrans.QtyBalanceAfter = StoreItem.Balance;
                StoreTrans.StoreItemId = item.StoreItemId;
                StoreTrans.StoreTransType = StoreTransType.Sales;
                StoreTrans.UnitPrice = item.UnitPrice;
                _db.StoreTransactions.Add(StoreTrans);
                _db.SaveChanges();

            }
            return JDs;
        }


        private decimal GetTotal(List<StoreItemBalanceDetails> OrderedList,
            StoreItem StoreItem, SalesItemDetails item)
        {
            decimal Total = 0;
            decimal Qty = item.Qty;

            foreach (var balance in OrderedList)
            {
                if (Qty == balance.CurrentBalance && Qty > 0)
                {
                    if (StoreItem.StoreSystem != StoreSystem.Averge)
                        Total = Total + (Qty * balance.StoreTransaction.UnitPrice * balance.StoreTransaction.PurchaseDetails.Currency.Rate);
                    Qty = 0;
                    balance.CurrentBalance = 0;
                    _db.StoreItemBalanceDetails.Update(balance);
                    _db.SaveChanges();
                }
                else if (Qty < balance.CurrentBalance && Qty > 0)
                {
                    if (StoreItem.StoreSystem != StoreSystem.Averge)
                        Total = Total + (Qty * balance.StoreTransaction.UnitPrice * balance.StoreTransaction.PurchaseDetails.Currency.Rate);
                    balance.CurrentBalance = balance.CurrentBalance - Qty;
                    Qty = 0;
                    _db.StoreItemBalanceDetails.Update(balance);
                    _db.SaveChanges();
                }
                else if (Qty > balance.CurrentBalance && Qty > 0)
                {
                    if (StoreItem.StoreSystem != StoreSystem.Averge)
                        Total = Total + (balance.CurrentBalance * balance.StoreTransaction.UnitPrice * balance.StoreTransaction.PurchaseDetails.Currency.Rate);
                    Qty = Qty - balance.CurrentBalance;
                    balance.CurrentBalance = 0;
                    _db.StoreItemBalanceDetails.Update(balance);
                    _db.SaveChanges();
                }

                if (StoreItem.StoreSystem == StoreSystem.Averge)
                    Total = StoreItem.Balance / StoreItem.Qty;
            }



            return Total;
        }

        public string ClientPaymentJournal(ClientPaymentContainer vm, Contacts Contact, decimal LocalAmount)
        {
            // Safe - Bank - Check => Debit
            // Client => Credit

            var journal = new JournalVM();
            journal.TransDate = vm.PaymentDetails.PaymentDate;
            journal.TransDesc = vm.PaymentDetails.Description;
           
            
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = vm.PaymentDetails.PaymentMethod == ClientPaymentMethod.Safe ? vm.PaymentDetails.SafeAccNum
                             : vm.PaymentDetails.PaymentMethod == ClientPaymentMethod.Bank ? vm.PaymentDetails.BankAccNum
                             : "1240000001";
            JD_Debit.Side = TransactionSidesEnum.Debit;
            JD_Debit.Debit = LocalAmount;
            JD_Debit.CurrencyId = vm.SelectedBalance.CurrencyId;
            journal.TransactionDetails.Add(JD_Debit);


            var JD_Credit = new JournalDetailsVM();
            JD_Credit.AccNum = Contact.ClientAccNum;
            JD_Credit.Side = TransactionSidesEnum.Credit;
            JD_Credit.Credit = LocalAmount;
            JD_Credit.CurrencyId = vm.SelectedBalance.CurrencyId;
            journal.TransactionDetails.Add(JD_Credit);
            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }



    }
}

