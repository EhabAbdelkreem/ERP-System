using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Model
{
    [Table("Finance_Expense_ExpenseItem")]
    public class ExpenseItem
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public string AccNum { get; set; }
        [ForeignKey("AccNum")]
        public AccountChart AccountDetail { get; set; }

        public int ExpenseTypeId { get; set; }
        [ForeignKey("ExpenseTypeId")]
        public ExpenseType ExpenseType { get; set; }
    }
}
