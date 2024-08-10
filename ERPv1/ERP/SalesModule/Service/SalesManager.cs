using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Service
{
    public class SalesManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ClientBalanceManager _clientBalanceManager;
        private readonly ClientJournalManager _clientJournalManager;
        private readonly ClientTransactionManager _clientTransactionManager;

        public SalesManager(ApplicationDbContext db, IMapper mapper,
                ClientBalanceManager clientBalanceManager,
                ClientJournalManager clientJournalManager,
                ClientTransactionManager clientTransactionManager)
        {
            _db = db;
            _mapper = mapper;
            _clientBalanceManager = clientBalanceManager;
            _clientJournalManager = clientJournalManager;
            _clientTransactionManager = clientTransactionManager;
        }

        public SalesContainer NewSale(int ClientId)
        {
            var vm = new SalesContainer();
            var client = _db.Contacts.Find(ClientId);
            vm.ClientData = new ClientData()
            {
                ClientId = ClientId,
                Balance = client.ClientBalance,
                ClientName = client.Name,
                Phone = client.Phone1
            };

            return vm;
        }

        public FeedBack SaveNewSale(SalesContainer vm)
        {
            var feedback = new FeedBack();
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var ClientData = _db.Contacts.Find(vm.ClientData.ClientId);
                    var Currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
                    var LocalAmount = vm.SalesSummary.TotalWithVAT * Currency.Rate;

                    // Update Balance Contact
                    var BalanceAfter = _clientBalanceManager.UpdateClientBalance(ClientData, LocalAmount, true);

                    // Update Balance In Currency
                    _clientBalanceManager.ManageClientBalanceInCurrency(ClientData.Id, ClientData.ClientAccNum,
                        Currency.Id, vm.SalesSummary.TotalWithVAT, true);

                    // Create New Invoice
                    var InvoiceNum = NewInvoice(vm);
                    // Income Journal  => Debit Client Credit Income
                    var TransId = _clientJournalManager.IncomeJournal(vm, ClientData, null);
                    // Purchase Jounral
                    // Update StoreItemTable
                    // Update StoreItemWithDetails
                    // Store Transaction
                    _clientJournalManager.PurchaseJournal(vm, InvoiceNum);

                    // Client Transaction 
                    _clientTransactionManager.ClientSalesTransaction(vm, ClientData, InvoiceNum, TransId);
                   
                    _db.SaveChanges();
                    transaction.Commit();
                    feedback.Done = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    feedback.Done = false;
                    do
                    {
                        feedback.Errors.Add(ex.Message);
                        ex = ex.InnerException;

                    } while (ex != null);
                }
            }

            return feedback;

        }

        private string NewInvoice(SalesContainer vm)
        {
            var invoice = new Invoices();
            var inv = CreateNewInvoiceNum();
            invoice.InoviceNum = inv.InvNum;
            invoice.InvoiceCount = inv.LastId;
            invoice.ContactId = vm.ClientData.ClientId;
            invoice.CurrencyId = vm.SalesSummary.CurrencyId;
            invoice.Amount = vm.SalesSummary.TotalAmount;
            invoice.InvoiceDate = vm.SalesSummary.InvoiceDate.ToEgyptionDate();
            invoice.IsVAT = vm.SalesSummary.IsVAT;
            invoice.TotalWithVAT = vm.SalesSummary.TotalWithVAT;
            invoice.VATAmount = vm.SalesSummary.VATAmount;
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            return invoice.InoviceNum;
        }

        private InvoiceNum CreateNewInvoiceNum()
        {
            //_db.Invoices.Where(x=> x.InvoiceDate.Year == invoicedate.Year).Max(x=>x.InvoiceCount)

            int i = _db.Invoices.Count() == 0 ? 1 : (_db.Invoices.Max(x => x.InvoiceCount) + 1);
            return new InvoiceNum()
            {
                InvNum = i.ToString("000000"),
                LastId = i
            };

        }





       
        

       


        class InvoiceNum
        {
            public string InvNum { get; set; }
            public int LastId { get; set; }
        }

    }
}
