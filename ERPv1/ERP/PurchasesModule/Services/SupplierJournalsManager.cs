using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.Expenses;
using ERPv1.ERP.PurchasesModule.ViewModel.ReturnBack;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services
{
    public class SupplierJournalsManager : ISupplierJournalsManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IJournalManager _journalManager;
        private readonly IUploadManager _uploadManager;

        public SupplierJournalsManager(ApplicationDbContext db, IJournalManager journalManager,
            IUploadManager uploadManager)
        {
            _db = db;
            _journalManager = journalManager;
            _uploadManager = uploadManager;
        }


        public string PurchaseJournal(PurchaseContainer vm, Contacts Contact, IFormFile Invoice)
        {
            var journal = new JournalVM();
            var Currency = _db.Currency.Find(vm.PurchaseSummary.CurrencyId);
            journal.TransDate = vm.PurchaseSummary.PurchaseDate;
            journal.TransDesc = vm.PurchaseSummary.Description;
            if(Invoice != null)
            {
               journal.DocName= _uploadManager.UploadedFile(Invoice, "FinanceFiles");
            }
            foreach (var item in vm.PurchaseDetails)
            {
                var StoreItem = _db.StoreItems.Find(item.StoreItemId);
                var JD_Store = new JournalDetailsVM();
                JD_Store.AccNum = StoreItem.StoreAccNum;
                JD_Store.Side = TransactionSidesEnum.Debit;
                JD_Store.Debit = item.QTY * item.UnitPrice * Currency.Rate;
                JD_Store.CurrencyId = vm.PurchaseSummary.CurrencyId;
                journal.TransactionDetails.Add(JD_Store);
            }
            if (vm.PurchaseSummary.IsVAT)
            {
                var JD_VAT = new JournalDetailsVM();
                JD_VAT.AccNum = "1269000001";
                JD_VAT.Side = TransactionSidesEnum.Debit;
                JD_VAT.Debit = vm.PurchaseSummary.VATAmount * Currency.Rate;
                JD_VAT.CurrencyId = vm.PurchaseSummary.CurrencyId;
                journal.TransactionDetails.Add(JD_VAT);
            }
            var JD_Supplier = new JournalDetailsVM();
            JD_Supplier.AccNum = Contact.SupplierAccNum;
            JD_Supplier.Side = TransactionSidesEnum.Credit;
            JD_Supplier.Credit = vm.PurchaseSummary.TotalAmountWithVAT * Currency.Rate;
            JD_Supplier.CurrencyId = vm.PurchaseSummary.CurrencyId;
            journal.TransactionDetails.Add(JD_Supplier);
            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public string ExpenseJounral (ExpenseVM vm, string ExpenseAccNum,Contacts Contact, Currency Currency)
        {
            var RestAmount = vm.ExpenseDetails.Amount - vm.PaymentDetails.PaymentAmount;
           

            var journal = new JournalVM();
            journal.TransDate = vm.PaymentDetails.PaymentDate;
            journal.TransDesc = vm.PaymentDetails.Description;
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = ExpenseAccNum;
            JD_Debit.Side = TransactionSidesEnum.Debit;
            JD_Debit.Debit = vm.ExpenseDetails.Amount * Currency.Rate;
            JD_Debit.CurrencyId = vm.ExpenseDetails.CurrencyId;
            journal.TransactionDetails.Add(JD_Debit);
            if (RestAmount > 0)
            {
                var JD_SupplierCredit = new JournalDetailsVM();
                JD_SupplierCredit.AccNum = Contact.SupplierAccNum;
                JD_SupplierCredit.Side = TransactionSidesEnum.Credit;
                JD_SupplierCredit.Debit = RestAmount;
                JD_SupplierCredit.CurrencyId = vm.ExpenseDetails.CurrencyId;
                journal.TransactionDetails.Add(JD_SupplierCredit);


                var JD_Credit = new JournalDetailsVM();
                JD_Credit.AccNum = vm.PaymentDetails.PaymentMethod == SupplierPaymentMethod.Safe ? vm.PaymentDetails.SafeAccNum
                             : vm.PaymentDetails.PaymentMethod == SupplierPaymentMethod.Bank ? vm.PaymentDetails.BankAccNum
                             : "2170000001";
                JD_Credit.Side = TransactionSidesEnum.Credit;
                JD_Credit.Debit = vm.PaymentDetails.PaymentAmount * Currency.Rate;
                JD_Credit.CurrencyId = vm.ExpenseDetails.CurrencyId;
                journal.TransactionDetails.Add(JD_Credit);
            }
            else
            {
                var JD_Credit = new JournalDetailsVM();
                JD_Credit.AccNum = vm.PaymentDetails.PaymentMethod == SupplierPaymentMethod.Safe ? vm.PaymentDetails.SafeAccNum
                             : vm.PaymentDetails.PaymentMethod == SupplierPaymentMethod.Bank ? vm.PaymentDetails.BankAccNum
                             : "2170000001";
                JD_Credit.Side = TransactionSidesEnum.Credit;
                JD_Credit.Debit = vm.ExpenseDetails.Amount * Currency.Rate;
                JD_Credit.CurrencyId = vm.ExpenseDetails.CurrencyId;
                journal.TransactionDetails.Add(JD_Credit);
            }

           


            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public string SupplierPaymentJournal(SupplierPaymentContainer vm, Contacts Contact, decimal LocalAmount)
        {
            var journal = new JournalVM();
            journal.TransDate = vm.PaymentDetails.PaymentDate;
            journal.TransDesc = vm.PaymentDetails.Description;
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = Contact.SupplierAccNum;
            JD_Debit.Side = TransactionSidesEnum.Debit;
            JD_Debit.Debit = LocalAmount;
            JD_Debit.CurrencyId = vm.SelectedBalance.CurrencyId;
            journal.TransactionDetails.Add(JD_Debit);


            var JD_Credit = new JournalDetailsVM();
            JD_Credit.AccNum = vm.PaymentDetails.PaymentMethod==SupplierPaymentMethod.Safe?vm.PaymentDetails.SafeAccNum
                             :vm.PaymentDetails.PaymentMethod== SupplierPaymentMethod.Bank?vm.PaymentDetails.BankAccNum
                             : "2170000001";
            JD_Credit.Side = TransactionSidesEnum.Credit;
            JD_Credit.Debit = LocalAmount;
            JD_Credit.CurrencyId = vm.SelectedBalance.CurrencyId;
            journal.TransactionDetails.Add(JD_Credit);


            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }




        public string PurchaseReturnJournal(PurchaseReturnBackContainer vm, Contacts Contact, Currency Currency, bool IsVAT, decimal TotalInLocal, decimal TotalAmountWithVat )
        {
            var journal = new JournalVM();
            
            journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
            journal.TransDesc = "Return Back";
            
            foreach (var item in vm.PurchaseStoreDetails)
            {
                var StoreItem = _db.StoreItems.Find(item.StoreItemId);
                var JD_Store = new JournalDetailsVM();
                JD_Store.AccNum = StoreItem.StoreAccNum;
                JD_Store.Side = TransactionSidesEnum.Credit;
                JD_Store.Credit = item.ReturnedQTY * item.UnitPrice * Currency.Rate;
                JD_Store.CurrencyId = Currency.Id;
                journal.TransactionDetails.Add(JD_Store);
            }
            if (IsVAT)
            {
                var JD_VAT = new JournalDetailsVM();
                JD_VAT.AccNum = "1269000001";
                JD_VAT.Side = TransactionSidesEnum.Credit;
                JD_VAT.Credit = TotalInLocal * 0.14M;
                JD_VAT.CurrencyId = Currency.Id;
                journal.TransactionDetails.Add(JD_VAT);
            }
            var JD_Supplier = new JournalDetailsVM();
            JD_Supplier.AccNum = Contact.SupplierAccNum;
            JD_Supplier.Side = TransactionSidesEnum.Debit;
            JD_Supplier.Debit = TotalAmountWithVat;
            JD_Supplier.CurrencyId = Currency.Id;
            journal.TransactionDetails.Add(JD_Supplier);
            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }
    }
}
