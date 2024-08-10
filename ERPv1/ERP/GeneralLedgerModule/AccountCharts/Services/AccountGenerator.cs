using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Services
{
    public class AccountGenerator : IAccountGenerator
    {
        private readonly IMapper _map;
        private readonly ApplicationDbContext _db;

        public AccountGenerator(ApplicationDbContext db, IMapper map)
        {
            _map = map;
            _db = db;
        }

        public string CreateNewAccount(CreateAccountVM newAccount)
        {
            var account = _map.Map<AccountChart>(newAccount);
            var currentcounter = _db.AccountChartCounter.Find(newAccount.AccTypeId);

            // Creating Account Number
            account.AccNum = (decimal.Parse(currentcounter.ParentAccNum) + currentcounter.Count + 1).ToString();

            // Parent Account
            account.ParentAcNum = currentcounter.ParentAccNum;

            //Account Nature
            var ParentAccount = _db.AccountChart.FirstOrDefault(x => x.AccNum.Trim()
                                        == currentcounter.ParentAccNum.Trim());
            account.AccountNature = ParentAccount.AccountNature;

            // Other filed
            account.Balance = 0;
            account.StartingBalance = 0;
            account.IsActive = true;
            account.IsParent = false;

            // Update AccountChartCounter
            currentcounter.Count = currentcounter.Count + 1;

            // Save New Account
            _db.AccountChart.Add(account);
            // Update Account Chart Counter
            _db.AccountChartCounter.Update(currentcounter);

            _db.SaveChanges();

            return account.AccNum;
        }


        public string GenerateAccount(CreateAccountVM newAccount)
        {
            var account = _map.Map<AccountChart>(newAccount);
            var currentcounter = _db.AccountChartCounter.Find(newAccount.AccTypeId);

            // Creating Account Number
            account.AccNum = (decimal.Parse(currentcounter.ParentAccNum) + currentcounter.Count + 1).ToString();

            // Parent Account
            account.ParentAcNum = currentcounter.ParentAccNum;

            //Account Nature
            var ParentAccount = _db.AccountChart.FirstOrDefault(x => x.AccNum.Trim()
                                        == currentcounter.ParentAccNum.Trim());
            account.AccountNature = ParentAccount.AccountNature;

            // Other filed
            account.Balance = 0;
            account.StartingBalance = 0;
            account.IsActive = true;
            account.IsParent = false;

            // Update AccountChartCounter

            currentcounter.Count = currentcounter.Count + 1;

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    // Save New Account
                    _db.AccountChart.Add(account);
                    // Update Account Chart Counter
                    _db.AccountChartCounter.Update(currentcounter);


                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }


            return account.AccNum;
        }




    }
}
