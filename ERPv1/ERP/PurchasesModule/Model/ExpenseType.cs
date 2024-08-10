using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Model
{
    [Table("Finance_Expense_ExpenseType")]
    public class ExpenseType
    {
        public int Id { get; set; }
        [Required, StringLength(75)]
        public string ExpenseTypeName { get; set; }
    }
}
