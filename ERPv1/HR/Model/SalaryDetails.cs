using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.Model
{
    [Table("HR_SalaryDetails")]
    public class SalaryDetails
    {
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        public SalaryBatch SalaryBatch { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal Allowonces { get; set; }
        public decimal Overtime { get; set; }
        public decimal Commision { get; set; }
        public decimal InsuranceEmployer { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal InsuranceEmployee { get; set; }
        public decimal Tax { get; set; }
        public decimal StaffAdvance { get; set; }
        public decimal Penalties { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetIncome { get; set; }

    }
}
