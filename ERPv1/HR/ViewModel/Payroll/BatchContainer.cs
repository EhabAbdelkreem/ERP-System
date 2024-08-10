using ERPv1.HR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.ViewModel.Payroll
{
    public class BatchContainer
    {
        public BatchContainer()
        {
            SalaryBatch = new SalaryBatch();
            EmployeeSalaries = new List<EmployeeSalary>();
        }
        public string AccNum { get; set; }
        public string PayrollDate { get; set; }  
        public SalaryBatch SalaryBatch { get; set; }
        public List<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
