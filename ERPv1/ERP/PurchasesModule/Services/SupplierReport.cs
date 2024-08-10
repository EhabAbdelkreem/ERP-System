using ERPv1.Data;
using ERPv1.ERP.SalesModule.ViewModel.SupplierStatment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services
{
    public class SupplierReport
    {
        private readonly ApplicationDbContext _db;

        public SupplierReport(ApplicationDbContext db)
        {
            _db = db;
        }

        public decimal GetStartBalance(StatmentParams STParm, DateTime Start)
        {
            var transactions = _db.SupplierTransactions.Include(x => x.TransactionDetails)
                                .Where(x => x.SupplierId == STParm.SupplierId
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

            var statment = _db.SupplierTransactions.Include(x => x.TransactionDetails)
                            .Where(x => x.SupplierId == STParm.SupplierId
                            && x.PaymentDate >= Start
                            && x.PaymentDate < End)
                            .OrderBy(x => x.Id)
                            .Select(x => new StatmentTransactions()
                            {
                                TransDate = x.PaymentDate.ToString("dd/MM/yyyy"),
                                Description = x.TransactionDetails.TransDesc,
                                Debit = x.PaymentMenthod != Model.SupplierPaymentMethod.Credit? x.PaymentAmount:0,
                                Credit = x.PaymentMenthod == Model.SupplierPaymentMethod.Credit ? x.PaymentAmount : 0,
                                BalanceAfter = x.BalanceAfter,
                                PurchaseId = x.PurchaseId
                            }).ToList();

            return statment;

        }
    }
}
