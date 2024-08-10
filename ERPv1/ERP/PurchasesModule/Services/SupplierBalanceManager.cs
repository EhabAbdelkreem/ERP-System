using CRM.Model;
using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.PurchasesModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services
{
    public class SupplierBalanceManager : ISupplierBalanceManager
    {
        private readonly ApplicationDbContext _db;

        public SupplierBalanceManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public decimal UpdateSupplierBalance(Contacts supplier, decimal LocalAmount, bool plus)
        {
            if (plus)
                supplier.SupplierBalance = supplier.SupplierBalance + LocalAmount;
            else
                supplier.SupplierBalance = supplier.SupplierBalance - LocalAmount;
            _db.Contacts.Update(supplier);
            _db.SaveChanges();
            return supplier.SupplierBalance;

        }

        public void ManageSupplierBalanceInCurrency(int SupplierId, string AccNum, int CurrencyId, decimal Amount, bool Plus)
        {
            var SupplierInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == SupplierId && x.AccNum == AccNum
                                                              && x.CurrencyId == CurrencyId).ToList();

            if (SupplierInCurrency.Count > 0)
                UpdateBalanceInCurrency(SupplierId, AccNum, CurrencyId, Amount, Plus);
            else
                AddNewBalanceInCurrency(SupplierId, AccNum, CurrencyId, Amount);

        }


        public void AddNewBalanceInCurrency(int SupplierId, string AccNum, int CurrencyId, decimal Amount)
        {
            _db.ContactBalanceInCurrency.Add(new ContactBalanceInCurrency()
            {
                AccNum = AccNum,
                ContactId = SupplierId,
                CurrencyId = CurrencyId,
                Balance = Amount
            });
            _db.SaveChanges();
        }


        public void UpdateBalanceInCurrency(int SupplierId, string AccNum, int CurrencyId, decimal Amount, bool Plus)
        {
            var SupplierInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == SupplierId && x.AccNum == AccNum
                                                              && x.CurrencyId == CurrencyId).FirstOrDefault();
            if (Plus)
                SupplierInCurrency.Balance = SupplierInCurrency.Balance + Amount;
            else
                SupplierInCurrency.Balance = SupplierInCurrency.Balance - Amount;


            _db.ContactBalanceInCurrency.Update(SupplierInCurrency);
            _db.SaveChanges();
        }


    }
}
