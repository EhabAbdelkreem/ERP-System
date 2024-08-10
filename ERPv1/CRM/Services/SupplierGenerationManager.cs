using AutoMapper;
using ERPv1.CRM.Interfaces;
using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.Services
{
    public class SupplierGenerationManager : ISupplierGenerationManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAccountGenerator _accountGenerator;

        public SupplierGenerationManager(ApplicationDbContext db, IMapper mapper, IAccountGenerator accountGenerator)
        {
            _db = db;
            _mapper = mapper;
            _accountGenerator = accountGenerator;
        }

        public IEnumerable<Contacts> GetAllSuppliers()
            => _db.Contacts.Where(x => x.IsSupplier == true).ToList();
        

        public void AddNewSupplier(ContactCreatingViewModel supplier)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    var NewSupplier = _mapper.Map<Contacts>(supplier);
                    NewSupplier.IsSupplier = true;
                    if (supplier.CreateAccount)
                    {
                        var account = new CreateAccountVM();
                        account.AccountName = supplier.Name;
                        account.AccountNameAr = supplier.NameAR;
                        account.AccTypeId = 14;
                        account.BranchId = 1;
                        account.CurrencyId = 1;
                        NewSupplier.SupplierAccNum= _accountGenerator.CreateNewAccount(account);
                    }

                    _db.Contacts.Add(NewSupplier);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

        }


    }
}
