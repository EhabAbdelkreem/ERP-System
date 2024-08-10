using CRM.Model;
using ERPv1.CRM.Model;
using ERPv1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Service
{
    public class ClientBalanceManager
    {
        private readonly ApplicationDbContext _db;

        public ClientBalanceManager(ApplicationDbContext db)
        {
            _db =  db;
        }

        public decimal UpdateClientBalance(Contacts client, decimal LocalAmount, bool plus)
        {
            if (plus)
                client.ClientBalance = client.ClientBalance + LocalAmount;
            else
                client.ClientBalance = client.ClientBalance - LocalAmount;
            _db.Contacts.Update(client);
            _db.SaveChanges();
            return client.ClientBalance;

        }

        public void ManageClientBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount, bool Plus)
        {
            var SupplierInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == ClientId && x.AccNum == AccNum
                                                              && x.CurrencyId == CurrencyId).ToList();

            if (SupplierInCurrency.Count > 0)
                UpdateBalanceInCurrency(ClientId, AccNum, CurrencyId, Amount, Plus);
            else
                AddNewBalanceInCurrency(ClientId, AccNum, CurrencyId, Amount);

        }


        public void AddNewBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount)
        {
            _db.ContactBalanceInCurrency.Add(new ContactBalanceInCurrency()
            {
                AccNum = AccNum,
                ContactId = ClientId,
                CurrencyId = CurrencyId,
                Balance = Amount
            });
            _db.SaveChanges();
        }


        public void UpdateBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount, bool Plus)
        {
            var ClientInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == ClientId && x.AccNum == AccNum
                                                              && x.CurrencyId == CurrencyId).FirstOrDefault();
            if (Plus)
                ClientInCurrency.Balance = ClientInCurrency.Balance + Amount;
            else
                ClientInCurrency.Balance = ClientInCurrency.Balance - Amount;


            _db.ContactBalanceInCurrency.Update(ClientInCurrency);
            _db.SaveChanges();
        }



    }
}
