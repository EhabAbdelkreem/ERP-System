using AutoMapper;
using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using ERPv1.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.Services
{
  
    public class ClientGenerationManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAccountGenerator _accountGenerator;
        public ClientGenerationManager(ApplicationDbContext db, IMapper mapper,
            IAccountGenerator accountGenerator)
        {
            _db = db;
            _mapper = mapper;
            _accountGenerator = accountGenerator;
        }



        public IEnumerable<Contacts> GetAllClients()
          => _db.Contacts.Where(x => x.IsClient == true).ToList();


        public FeedBack AddNewClient(ContactCreatingViewModel Client)
        {
            var feedback = new FeedBack();
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                
                try
                {

                    var NewClient = _mapper.Map<Contacts>(Client);
                    NewClient.IsClient = true;
                    if (Client.CreateAccount)
                    {
                        var account = new CreateAccountVM();
                        account.AccountName = Client.Name;
                        account.AccountNameAr = Client.NameAR;
                        account.AccTypeId = 6;
                        account.BranchId = 1;
                        account.CurrencyId = 1;
                        NewClient.SupplierAccNum = _accountGenerator.CreateNewAccount(account);
                    }

                    _db.Contacts.Add(NewClient);
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

    }
}
