using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.ViewModel;
using ERPv1.HR.Model;
using ERPv1.HR.ViewModel.Payroll;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.Services
{
    public class SalaryBatchManager
    {
        private readonly ApplicationDbContext _db;
        private readonly TaxManager _taxManager;
        private readonly IJournalManager _journalManager;

        public SalaryBatchManager(ApplicationDbContext db, TaxManager taxManager,
            IJournalManager journalManager)
        {
            _db = db;
            _taxManager = taxManager;
            _journalManager = journalManager;
        }

        public BatchContainer StartBatch(int BatchId)
        {
            var batch = new BatchContainer();
            batch.SalaryBatch = _db.SalaryBatches.Find(BatchId);
            var employees = _db.Employees.ToList();
            foreach (var employee in employees)
            {
                var SalaryDetails = new EmployeeSalary();
                SalaryDetails.Allowonces = 0;
                SalaryDetails.BasicSalary = employee.BasicSalary;
                SalaryDetails.Commision = 0;
                SalaryDetails.EmployeeId = employee.Id;
                SalaryDetails.GrossIncome = 0;
                SalaryDetails.InsuranceEmployee = employee.InsuranceSalary * 0.11M;
                SalaryDetails.InsuranceEmployer = employee.InsuranceSalary * 0.1875M;
                SalaryDetails.Name = employee.Name;
                SalaryDetails.NetIncome = 0;
                SalaryDetails.Overtime = 0;
                SalaryDetails.Penalties = 0;
                SalaryDetails.StaffAdvance = 0;
                SalaryDetails.Tax = 0;
                SalaryDetails.TotalDeductions = 0;
                batch.EmployeeSalaries.Add(SalaryDetails);
            }
            return batch;
        }


        public void CalcTax(List<EmployeeSalary> salaries)
        {
            foreach (var salary in salaries)
            {
                var totalIncome = salary.BasicSalary + salary.Commision
                    + salary.Overtime + salary.Allowonces;

                salary.Tax = _taxManager.CalcTaxUpdated(totalIncome, 9000) / 12;
            }
        }

        public void SaveSalary(BatchContainer Salaries)
        {

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var Salary = new List<SalaryDetails>();

                    var Batch = _db.SalaryBatches.Find(Salaries.SalaryBatch.Id);
                    Batch.TransId = CreatePayrollJournal(Salaries);
                   

                    foreach (var salary in Salaries.EmployeeSalaries)
                    {
                        var batch = new SalaryDetails();
                        batch.Allowonces = salary.Allowonces;
                        batch.BasicSalary = salary.BasicSalary;
                        batch.Commision = salary.Commision;
                        batch.EmployeeId = salary.EmployeeId;
                        batch.GrossIncome = salary.GrossIncome;
                        batch.InsuranceEmployee = salary.InsuranceEmployee;
                        batch.InsuranceEmployer = salary.InsuranceEmployer;
                        batch.NetIncome = salary.NetIncome;
                        batch.Overtime = salary.Overtime;
                        batch.Penalties = salary.Penalties;
                        batch.StaffAdvance = salary.StaffAdvance;
                        batch.Tax = salary.Tax;
                        batch.BatchId = Batch.Id;
                        batch.TotalDeductions = salary.TotalDeductions;
                        Salary.Add(batch);
                    }
                    _db.SalaryBatches.Update(Batch);
                    _db.SalaryDetails.AddRange(Salary);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    transaction.Rollback();
                }
            }

           
        }


        public string CreatePayrollJournal(BatchContainer batch)
        {
            var journal = new JournalVM();
            var Currency = _db.Currency.Find(1);
            journal.TransDate = batch.PayrollDate;
            journal.TransDesc = $"Payroll for {batch.SalaryBatch.BatchMonth}-{batch.SalaryBatch.BatchYear}";

            decimal TotalGrossIncome = batch.EmployeeSalaries.Sum(x => x.GrossIncome);
            decimal TotalInsuranceEmployee = batch.EmployeeSalaries.Sum(x => x.InsuranceEmployee);
            decimal TotalInsuranceEmployer = batch.EmployeeSalaries.Sum(x => x.InsuranceEmployer);
            decimal TotalTax = batch.EmployeeSalaries.Sum(x => x.Tax);
            decimal TotalPenalties = batch.EmployeeSalaries.Sum(x => x.Penalties);
            decimal TotalNetIncome = batch.EmployeeSalaries.Sum(x => x.NetIncome);
            decimal TotalStaffAdvnace = batch.EmployeeSalaries.Sum(x => x.StaffAdvance);

            // Debit Client Credit Income
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = "4111000003";
            JD_Debit.Side = TransactionSidesEnum.Debit;
            JD_Debit.Debit = TotalGrossIncome;
            JD_Debit.CurrencyId = Currency.Id;
            JD_Debit.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_Debit);

            var JD_InsureanceEmployee = new JournalDetailsVM();
            JD_InsureanceEmployee.AccNum = "2260000001";
            JD_InsureanceEmployee.Side = TransactionSidesEnum.Credit;
            JD_InsureanceEmployee.Credit = TotalInsuranceEmployee;
            JD_InsureanceEmployee.CurrencyId = Currency.Id;
            JD_InsureanceEmployee.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_InsureanceEmployee);

            var JD_InsureanceEmployer = new JournalDetailsVM();
            JD_InsureanceEmployer.AccNum = "2260000002";
            JD_InsureanceEmployer.Side = TransactionSidesEnum.Credit;
            JD_InsureanceEmployer.Credit = TotalInsuranceEmployer;
            JD_InsureanceEmployer.CurrencyId = Currency.Id;
            JD_InsureanceEmployer.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_InsureanceEmployer);

            var JD_Tax = new JournalDetailsVM();
            JD_Tax.AccNum = "2220000002";
            JD_Tax.Side = TransactionSidesEnum.Credit;
            JD_Tax.Credit = TotalTax;
            JD_Tax.CurrencyId = Currency.Id;
            JD_Tax.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_Tax);

            var JD_Pen = new JournalDetailsVM();
            JD_Pen.AccNum = "3110000002";
            JD_Pen.Side = TransactionSidesEnum.Credit;
            JD_Pen.Credit = TotalPenalties;
            JD_Pen.CurrencyId = Currency.Id;
            JD_Pen.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_Pen);


            var JD_Cash = new JournalDetailsVM();
            JD_Cash.AccNum = batch.AccNum;
            JD_Cash.Side = TransactionSidesEnum.Credit;
            JD_Cash.Credit = TotalNetIncome;
            JD_Cash.CurrencyId = Currency.Id;
            JD_Cash.UsedRate = Currency.Rate;
            journal.TransactionDetails.Add(JD_Cash);

            if (TotalStaffAdvnace > 0)
            {
                foreach (var item in batch.EmployeeSalaries)
                {
                    if (item.StaffAdvance > 0)
                    {
                        var EmployeeStaffAdvnace = _db.Employees.Find(item.EmployeeId);
                        var JD_Staff = new JournalDetailsVM();
                        JD_Staff.AccNum = EmployeeStaffAdvnace.StaffAdvanceAccNum;
                        JD_Staff.Side = TransactionSidesEnum.Credit;
                        JD_Staff.Credit = item.StaffAdvance;
                        JD_Staff.CurrencyId = Currency.Id;
                        JD_Staff.UsedRate = Currency.Rate;
                        journal.TransactionDetails.Add(JD_Staff);
                    }
                }
            }

            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }
    }
}
