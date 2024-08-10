using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Services
{
    public class NotesPayableManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IJournalManager _journalManager;

        public NotesPayableManager(ApplicationDbContext db, IMapper mapper,
            IJournalManager journalManager)
        {
            _db = db;
            _mapper = mapper;
            _journalManager = journalManager;
        }

        public void SaveNewNP(NotesPayableCreationVM vm)
        {
            var notes = _mapper.Map<NotesPayable>(vm);
            notes.CheckStatus = NotesPayableStatusEnum.UnderCollection;
            _db.NotesPayables.Add(notes);
            _db.SaveChanges();
        }


        public void SaveNewNPHistory(NotesPayableCreationVM vm, string TransId)
        {
            var His = new NotesPayableTransactionHistory()
            {
                ActionDate = vm.WritingDate.ToEgyptionDate(),
                ChkNum = vm.ChkNum,
                Description = $"اضافة شيك جديد رقم {vm.ChkNum}",
                PaidAmount = 0,
                 StatusAfterAction= NotesPayableStatusEnum.UnderCollection,
                 TransId = TransId
            };
            _db.NotesPayableTransactionHistory.Add(His);
            _db.SaveChanges();
        }

        public NPContainer GetAllNP()
        {
            var vm = new NPContainer();
            vm.CheckUnderCollection = GetNPWithStatus(NotesPayableStatusEnum.UnderCollection);
            vm.CheckCashCollection = GetNPWithStatus(NotesPayableStatusEnum.CashCollection);
            return vm;
        }


        public List<NPDetails> GetNPWithStatus(NotesPayableStatusEnum status)
        {
            var vm =_mapper.Map<List<NPDetails>>(_db.NotesPayables.Include(x => x.Currency)
                  .Include(x => x.Supplier).Include(x=>x.BankAccount)
                    .Where(x => x.CheckStatus == status).ToList());
                
            return vm;
        }

        public void MoveCheckToCashPayment(NPDetails np)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
                    check.CheckStatus = NotesPayableStatusEnum.CashCollection;
                    _db.NotesPayables.Update(check);
                    _db.SaveChanges();

                    var His = new NotesPayableTransactionHistory()
                    {
                        ActionDate = DateTime.Now,
                        ChkNum = np.ChkNum,
                        Description = $"تحصيل نقدي لشيك رقم {np.ChkNum}",
                        PaidAmount = 0,
                        StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                        TransId = string.Empty
                    };
                    _db.NotesPayableTransactionHistory.Add(His);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var error =ex.Message;
                    transaction.Rollback();
                }
            }
 
        }
       
        public void CollectNP(NPDetails np)
        {
            // update Status => collected
            // history => collected
            // journal transaction debit NP credit bankAccount


            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
                    check.CheckStatus = NotesPayableStatusEnum.Collected;
                    _db.NotesPayables.Update(check);
                    _db.SaveChanges();

                    

                    var journal = new JournalVM();
                    journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
                    journal.TransDesc = $"تحصيل شيك رقم {np.ChkNum} من حساب بنك {np.BankAccountName}";
                    var JD_Debit = new JournalDetailsVM();
                    JD_Debit.AccNum = "2170000001";
                    JD_Debit.Side = TransactionSidesEnum.Debit;
                    JD_Debit.Debit = np.AmountLocal;
                    JD_Debit.CurrencyId = np.CurrencyId;
                    journal.TransactionDetails.Add(JD_Debit);


                    var JD_Credit = new JournalDetailsVM();
                    JD_Credit.AccNum = np.BankAccountNum;
                    JD_Credit.Side = TransactionSidesEnum.Credit;
                    JD_Credit.Debit = np.AmountLocal;
                    JD_Credit.CurrencyId = np.CurrencyId;
                    journal.TransactionDetails.Add(JD_Credit);


                    var TransId = _journalManager.SaveJournal(journal);

                    var His = new NotesPayableTransactionHistory()
                    {
                        ActionDate = DateTime.Now,
                        ChkNum = np.ChkNum,
                        Description = $"تحصيل لشيك رقم {np.ChkNum}",
                        PaidAmount = 0,
                        StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                        TransId = TransId
                    };
                    _db.NotesPayableTransactionHistory.Add(His);




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



        public void CollectCashNP(NPDetails np, PaymentDetails paymentDetails)
        {
            // update Status => collected
            // history => collected
            // journal transaction debit NP credit bankAccount

            var curreny = _db.Currency.Find(np.CurrencyId);
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
                    check.Paid = (check.Paid.HasValue?check.Paid.Value:0) + paymentDetails.PaymentAmount;
                    if(check.Paid.Value == check.AmountForgin)
                        check.CheckStatus = NotesPayableStatusEnum.Collected;
                    else
                        check.CheckStatus = NotesPayableStatusEnum.CashCollection;
                  



                    var journal = new JournalVM();
                    journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
                    journal.TransDesc = $"تحصيل شيك رقم {np.ChkNum} من حساب بنك {np.BankAccountName}";
                    var JD_Debit = new JournalDetailsVM();
                    JD_Debit.AccNum = "2170000001";
                    JD_Debit.Side = TransactionSidesEnum.Debit;
                    JD_Debit.Debit = paymentDetails.PaymentAmount * curreny.Rate;
                    JD_Debit.CurrencyId = np.CurrencyId;
                    journal.TransactionDetails.Add(JD_Debit);


                    var JD_Credit = new JournalDetailsVM();
                    JD_Credit.AccNum = paymentDetails.PaymentMethod == SupplierPaymentMethod.Safe ? paymentDetails.SafeAccNum
                                     : paymentDetails.PaymentMethod == SupplierPaymentMethod.Bank ? paymentDetails.BankAccNum
                                     : "2170000001";
                    JD_Credit.Side = TransactionSidesEnum.Credit;
                    JD_Credit.Debit = paymentDetails.PaymentAmount * curreny.Rate;
                    JD_Credit.CurrencyId = np.CurrencyId;
                    journal.TransactionDetails.Add(JD_Credit);


                    var TransId = _journalManager.SaveJournal(journal);

                    if (check.CheckStatus == NotesPayableStatusEnum.Collected)
                    {
                        var His = new NotesPayableTransactionHistory()
                        {
                            ActionDate = DateTime.Now,
                            ChkNum = np.ChkNum,
                            Description = $"تحصيل لشيك رقم {np.ChkNum}",
                            PaidAmount = 0,
                            StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                            TransId = TransId
                        };
                        _db.NotesPayableTransactionHistory.Add(His);
                    }
                    else
                    {
                        var His = new NotesPayableTransactionHistory()
                        {
                            ActionDate = DateTime.Now,
                            ChkNum = np.ChkNum,
                            Description = $"تحصيل نقدي لشيك رقم {np.ChkNum} بمبلغ {paymentDetails.PaymentAmount}",
                            PaidAmount = paymentDetails.PaymentAmount,
                            StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                            TransId = TransId
                        };
                        _db.NotesPayableTransactionHistory.Add(His);
                    }

                    if (paymentDetails.PaymentMethod == SupplierPaymentMethod.Check)
                    {
                        var newCheck = new NotesPayableCreationVM()
                        {
                            AmountForgin = paymentDetails.PaymentAmount,
                            AmountLocal = paymentDetails.PaymentAmount * curreny.Rate,
                            BankAccountNum = paymentDetails.BankAccNum,
                            ChkNum = paymentDetails.CheckNum,
                            CurrencyId = np.CurrencyId,
                            DueDate = paymentDetails.PaymentDueDate,
                            WritingDate = paymentDetails.WritingDate,
                            SupplierId = np.SupplierId

                        };
                        SaveNewNP(newCheck);
                        SaveNewNPHistory(newCheck,TransId);
                    }
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


    }
}
