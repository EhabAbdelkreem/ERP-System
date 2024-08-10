using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.Services;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Service
{
    public class ClientPayamentManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ClientBalanceManager _clientBalanceManager;
        private readonly ClientTransactionManager _clientTransactionManager;
        private readonly ClientJournalManager _clientJournalManager;
        private readonly NRManager _NRManager;

        public ClientPayamentManager(ApplicationDbContext db, ClientBalanceManager clientBalanceManager
            ,ClientTransactionManager clientTransactionManager, ClientJournalManager clientJournalManager 
            ,NRManager NRManager)
        {
            _db = db;
            _clientBalanceManager = clientBalanceManager;
            _clientTransactionManager = clientTransactionManager;
            _clientJournalManager = clientJournalManager;
            _NRManager = NRManager;
        }


        public ClientPaymentContainer NewPayment(int ClientId)
        {
            var vm = new ClientPaymentContainer();
            var client = _db.Contacts.Find(ClientId);
            vm.ClientData.ClientId = ClientId;
            vm.ClientData.ClientName = client.Name;
            vm.ClientData.Phone = client.Phone1;
            vm.ClientData.Balance = client.ClientBalance;

            vm.ClientBalanceDetails = _db.ContactBalanceInCurrency.Include(x => x.Currency)
                                        .Where(x => x.ContactId == ClientId
                                        && x.AccNum == client.ClientAccNum)
                                        .Select(x => new ClientBalanceDetails()
                                        {
                                            Amount = x.Balance,
                                            CurrencyAbbr = x.Currency.CurrencyAbbrev,
                                            CurrencyId = x.CurrencyId,
                                            LocalAmount = x.Balance * x.Currency.Rate,
                                            Rate = x.Currency.Rate
                                        }).ToList();

            return vm;

        }


        public void SaveClientPayment (ClientPaymentContainer vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var Currecny = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
                    var LocalAmount = vm.PaymentDetails.PaymentAmount * Currecny.Rate;
                    var Client = _db.Contacts.Find(vm.ClientData.ClientId);
                    // Update ClientBalance Contact
                    var BalanceAfter = _clientBalanceManager.UpdateClientBalance(Client, LocalAmount, false);
                    // Update ClientBalance In Currency
                    _clientBalanceManager.ManageClientBalanceInCurrency(Client.Id, Client.ClientAccNum, Currecny.Id
                        , vm.PaymentDetails.PaymentAmount, false);
                    // Journal
                    var TransId = _clientJournalManager.ClientPaymentJournal(vm, Client, LocalAmount);

                    // Add Client Transaction
                    _clientTransactionManager.ClientPaymentTransaction(vm, Client, TransId, BalanceAfter);
                    // if Check => add new check
                    if (vm.PaymentDetails.PaymentMethod == ClientPaymentMethod.Check)
                    {
                        _NRManager.AddNewCheck(vm, Currecny, TransId);
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
