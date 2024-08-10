using ERPv1.Data;
using ERPv1.ERP.SalesModule.ViewModel.ClientStatment;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Service
{
    public class ClientReports
    {
        private readonly ApplicationDbContext _db;

        public ClientReports(ApplicationDbContext db)
        {
            _db = db;
        }


        public void UpdateStatement(ClientStatmentContainer vm)
        {
            var Start = vm.StatmentParams.StartDate.ToEgyptionDate();
            var End = vm.StatmentParams.EndDate.ToEgyptionDate().AddDays(1);

            vm.Transactions = GetTransactions(vm.StatmentParams,Start,End);
            vm.StatmentParams.StartBalance = GetStartBalance(vm.StatmentParams, Start);
            if (vm.Transactions.Count > 0)
            {
                vm.StatmentParams.EndBalance = vm.Transactions.Last().BalanceAfter;
            }
            else
            {
                vm.StatmentParams.EndBalance = 0;
            }

        }


        public decimal GetStartBalance(StatmentParams STParm, DateTime Start)
        {
            var transactions = _db.ClientTransactions.Include(x => x.TransactionDetails)
                                .Where(x => x.ClientId == STParm.ClientId
                                && x.PaymentDate < Start).OrderBy(x => x.Id).ToList();
            if (transactions.Count > 0)
            {
                return transactions.Last().BalanceAfter;
            }
            else
            {
                return 0;
            }

        }

        public List<StatmentTransactions> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End)
        {

            var statment = _db.ClientTransactions.Include(x => x.TransactionDetails)
                            .Where(x => x.ClientId == STParm.ClientId
                            && x.PaymentDate >= Start
                            && x.PaymentDate < End)
                            .OrderBy(x => x.Id)
                            .Select(x => new StatmentTransactions()
                            {
                                TransDate = x.PaymentDate.ToString("dd/MM/yyyy"),
                                Description = x.TransactionDetails.TransDesc,
                                Debit = x.PaymentMenthod == Model.ClientPaymentMethod.Credit?x.PaymentAmount:0,
                                Credit = x.PaymentMenthod != Model.ClientPaymentMethod.Credit ? x.PaymentAmount : 0,
                                BalanceAfter = x.BalanceAfter,
                                InvoiceNum = x.InvoiceNum
                            }).ToList();

            return statment;

           
        }




    }
}
