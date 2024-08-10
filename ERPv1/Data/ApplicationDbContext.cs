using System;
using System.Collections.Generic;
using System.Text;
using CRM.Model;
using ERPv1.CRM.Model;
using ERPv1.ERP.CurrentAssetModules.ChecksModule.Model;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Settings;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.HR.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERPv1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalaryBatch> SalaryBatches { get; set; }
        public DbSet<SalaryDetails> SalaryDetails { get; set; }


        #region CRM
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<ContactBalanceInCurrency> ContactBalanceInCurrency { get; set; }

        #endregion

        #region CuurentLiabilities
        public DbSet<NotesPayable> NotesPayables { get; set; }
        public DbSet<NotesPayableTransactionHistory> NotesPayableTransactionHistory { get; set; }


        #endregion

        #region CurrentAsset
        #region Checks
        public DbSet<Check> Check { get; set; }
        public DbSet<CheckHafza> CheckHafza { get; set; }
        public DbSet<CheckHistory> CheckHistory { get; set; }
        public DbSet<CheckLocation> CheckLocation { get; set; }
        public DbSet<CheckStatus> CheckStatus { get; set; }
        #endregion


        #region Inventory
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<StoreItemWithSN> StoreItemWithSN { get; set; }
        public DbSet<StoreTransaction> StoreTransactions { get; set; }
        public DbSet<StoreItemBalanceDetails> StoreItemBalanceDetails { get; set; }

        #endregion

        #endregion

        #region Purchase
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        public DbSet<ExpenseType> expenseTypes { get; set; }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }
        public DbSet<ExpenseSummary> ExpenseSummary { get; set; }


        #endregion
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<ClientTransaction> ClientTransactions { get; set; }



        #region Sales


        #endregion

        #region GL
        public DbSet<AccountChart> AccountChart { get; set; }
        public DbSet<AccountChartCounter> AccountChartCounter { get; set; }
        public DbSet<Journal> Journal { get; set; }
        public DbSet<JournalDetails> JournalDetails { get; set; }
        public DbSet<FiniacialPeriod> FiniacialPeriods { get; set; }
        public DbSet<HistoricalBalance> HistoricalBalances { get; set; }

        #endregion

        #region Settings
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Branch> Branch { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Seed Check Settings
            builder.Entity<Employee>().HasData(
                new Employee() { Id = 1, BasicSalary = 5000, DepartmentId = 1, InsuranceSalary = 2500, Name = "Ahmed", Phone = "0124558880", Title = "HR Manager" },
                new Employee() { Id = 2, BasicSalary = 10000, DepartmentId = 2, InsuranceSalary = 6000, Name = "Mohamed", Phone = "0124558881", Title = "IT Manager" },
                new Employee() { Id = 3, BasicSalary = 20000, DepartmentId = 3, InsuranceSalary = 7000, Name = "Peter", Phone = "0124558882", Title = "Finance Manager" }
                );
            builder.Entity<SalaryBatch>().HasData(
                new SalaryBatch() { Id = 1, BatchMonth = 12, BatchYear = 2020 }
                );

            builder.Entity<CheckLocation>().HasData(
                new CheckLocation() { Id = 1, CheckLocationEN = "Safe", CheckLocationAR = "الخزنة", IsDefualt = true },
                new CheckLocation() { Id = 2, CheckLocationEN = "Bank", CheckLocationAR = "البنك", IsDefualt = false },
                new CheckLocation() { Id = 3, CheckLocationEN = "BankCollected", CheckLocationAR = "محصل", IsDefualt = false },
                new CheckLocation() { Id = 4, CheckLocationEN = "Client", CheckLocationAR = "مع العميل", IsDefualt = false }
                );

            builder.Entity<CheckStatus>().HasData(
                new CheckStatus() { Id = 1, CheckStatusEN = "Under Collection", CheckStatusAR = "تحت التحصيل", IsDefault = true },
                new CheckStatus() { Id = 2, CheckStatusEN = "Collected", CheckStatusAR = "محصل", IsDefault = true },
                new CheckStatus() { Id = 3, CheckStatusEN = "Bounced", CheckStatusAR = "مرتد", IsDefault = true }
                );
            #endregion


            builder.Entity<SalaryDetails>().HasKey(x => new { x.EmployeeId, x.BatchId });
            builder.Entity<ContactBalanceInCurrency>().HasKey(x => new { x.ContactId, x.CurrencyId, x.AccNum });
            //builder.Entity<AccountChart>().HasIndex(x=>x.AccountName).IsUnique();
            //builder.Entity<AccountChart>().HasIndex(x => x.AccountNameAr).IsUnique();
            builder.Entity<StoreItemBalanceDetails>().HasOne(x => x.StoreTransaction).WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StoreItemWithSN>().HasOne(x => x.StoreTransaction).WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Branch>().HasData(new Branch() { Id = 1, BranchName = "Main" });

            builder.Entity<Department>().HasData(new Department() { Id = 1, DepartmentName = "IT" },
                                                 new Department() { Id = 2, DepartmentName = "HR" });

            builder.Entity<ExpenseType>().HasData(new ExpenseType() { Id = 1, ExpenseTypeName = "Managment Expenses" },
                                                  new ExpenseType() { Id = 2, ExpenseTypeName = "Banking Expenses" });

            builder.Entity<Currency>().HasData(
                new Currency() { Id = 1, CurrencyName = "Egyptain Pound", CurrencyNameAr = "جنية مصر", CurrencyAbbrev = "EGP", Rate = 1, IsDefault = true },
                new Currency() { Id = 2, CurrencyName = "American Dollar", CurrencyNameAr = "دولار أمريكي", CurrencyAbbrev = "USD", Rate = 16.00M, IsDefault = false }
                );

            builder.Entity<FiniacialPeriod>().HasData(
                new FiniacialPeriod() { Id = 1, YearName = "2020-2021", IsActive = true });

            #region Seeding AccountChartCounter

            builder.Entity<AccountChartCounter>().HasData(
                            new AccountChartCounter()
                            {
                                Id = 1,
                                AccountType = "Buildings",
                                AccountCategory = AccountCategoryEnum.FixedAsset,
                                ParentAccNum = "1110000000",
                                Count = 0,
                                BalanceSheet = true,
                                IncomeStatement = false
                            },
                            new AccountChartCounter()
                            {
                                Id = 2,
                                AccountType = "Machines And Equipments",
                                AccountCategory = AccountCategoryEnum.FixedAsset,
                                ParentAccNum = "1120000000",
                                Count = 0,
                                BalanceSheet = true,
                                IncomeStatement = false
                            },
                            new AccountChartCounter()
                            {
                                Id = 3,
                                AccountType = "Furnitiures",
                                AccountCategory = AccountCategoryEnum.FixedAsset,
                                ParentAccNum = "1130000000",
                                Count = 0,
                                BalanceSheet = true,
                                IncomeStatement = false
                            },
                            new AccountChartCounter()
                            {
                                Id = 4,
                                AccountType = "Safe",
                                AccountCategory = AccountCategoryEnum.CurrentAsset,
                                ParentAccNum = "1210000000",
                                Count = 1,
                                BalanceSheet = true,
                                IncomeStatement = false
                            },
                        new AccountChartCounter()
                        {
                            Id = 5,
                            AccountType = "Bank",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1220000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 6,
                            AccountType = "Client",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1230000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 7,
                            AccountType = "Check",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1240000000",
                            Count = 3,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 8,
                            AccountType = "Store",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1250000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 9,
                            AccountType = "Custody",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1261000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 10,
                            AccountType = "StaffAdvances",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1262000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                         new AccountChartCounter()
                         {
                             Id = 11,
                             AccountType = "SupplierAdvances",
                             AccountCategory = AccountCategoryEnum.CurrentAsset,
                             ParentAccNum = "1263000000",
                             Count = 0
                         },
                        new AccountChartCounter()
                        {
                            Id = 12,
                            AccountType = "OtherCurrentAsset",
                            AccountCategory = AccountCategoryEnum.CurrentAsset,
                            ParentAccNum = "1269000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 13,
                            AccountType = "NotePayable",
                            AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                            ParentAccNum = "2170000000",
                            Count = 1,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 14,
                            AccountType = "Suppliers",
                            AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                            ParentAccNum = "2210000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 15,
                            AccountType = "Taxes",
                            AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                            ParentAccNum = "2220000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 16,
                            AccountType = "Creditors",
                            AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                            ParentAccNum = "2230000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 17,
                            AccountType = "Accrued Expenses",
                            AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                            ParentAccNum = "2240000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 18,
                            AccountType = "Advances Income",
                            AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                            ParentAccNum = "2250000000",
                            Count = 1,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 19,
                            AccountType = "Income",
                            AccountCategory = AccountCategoryEnum.Income,
                            ParentAccNum = "3110000000",
                            Count = 0,
                            BalanceSheet = false,
                            IncomeStatement = true
                        },
                        new AccountChartCounter()
                        {
                            Id = 20,
                            AccountType = "Expense",
                            AccountCategory = AccountCategoryEnum.Expnese,
                            ParentAccNum = "4111000000",
                            Count = 0,
                            BalanceSheet = false,
                            IncomeStatement = true
                        },
                         new AccountChartCounter()
                         {
                             Id = 21,
                             AccountType = "Purchases",
                             AccountCategory = AccountCategoryEnum.StorePurchase,
                             ParentAccNum = "4112000000",
                             Count = 0,
                             BalanceSheet = false,
                             IncomeStatement = true
                         },
                        new AccountChartCounter()
                        {
                            Id = 22,
                            AccountType = "Owners",
                            AccountCategory = AccountCategoryEnum.OwnerEquity,
                            ParentAccNum = "5110000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                        new AccountChartCounter()
                        {
                            Id = 23,
                            AccountType = "OwnerWithdraw",
                            AccountCategory = AccountCategoryEnum.OwnerEquity,
                            ParentAccNum = "5120000000",
                            Count = 0,
                            BalanceSheet = true,
                            IncomeStatement = false
                        },
                         new AccountChartCounter()
                         {
                             Id = 24,
                             AccountType = "OtherCurrentLiabilties",
                             AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                             ParentAccNum = "2260000000",
                             Count = 0,
                             BalanceSheet = true,
                             IncomeStatement = false
                         }
                         );
            #endregion


            #region Seeding AccountChart
            builder.Entity<AccountChart>().HasData(
                     new AccountChart()
                     {
                         AccountName = "Buildings",
                         AccountNameAr = "مباني",
                         AccNum = "1110000000",
                         AccTypeId = 1,
                         IsParent = true,
                         CurrencyId = 1,
                         Balance = 0,
                         BranchId = 1,
                         ParentAcNum = "",
                         IsActive = true,
                         AccountNature = AccountNatureEnum.Debit
                     },
                     new AccountChart()
                     {
                         AccountName = "Machines And Equipments",
                         AccountNameAr = "أجهزة و معدات",
                         AccNum = "1120000000",
                         AccTypeId = 2,
                         IsParent = true,
                         CurrencyId = 1,
                         Balance = 0,
                         BranchId = 1,
                         ParentAcNum = "",
                         IsActive = true,
                         AccountNature = AccountNatureEnum.Debit
                     },
                      new AccountChart()
                      {
                          AccountName = "Furnitiures",
                          AccountNameAr = "أثاث",
                          AccNum = "1130000000",
                          AccTypeId = 3,
                          IsParent = true,
                          CurrencyId = 1,
                          Balance = 0,
                          BranchId = 1,
                          ParentAcNum = "",
                          IsActive = true,
                          AccountNature = AccountNatureEnum.Debit
                      },
                    new AccountChart()
                    {
                        AccountName = "Safes",
                        AccountNameAr = "خزن",
                        AccNum = "1210000000",
                        AccTypeId = 4,
                        IsParent = true,
                        CurrencyId = 1,
                        Balance = 0,
                        BranchId = 1,
                        ParentAcNum = "",
                        IsActive = true,
                        AccountNature = AccountNatureEnum.Debit
                    },
                     new AccountChart()
                     {
                         AccountName = "Banks",
                         AccountNameAr = "بنوك",
                         AccNum = "1220000000",
                         AccTypeId = 5,
                         IsParent = true,
                         CurrencyId = 1,
                         Balance = 0,
                         BranchId = 1,
                         ParentAcNum = "",
                         IsActive = true,
                         AccountNature = AccountNatureEnum.Debit
                     },
                     new AccountChart()
                     {
                         AccountName = "Clients",
                         AccountNameAr = "عملاء",
                         AccNum = "1230000000",
                         AccTypeId = 6,
                         IsParent = true,
                         CurrencyId = 1,
                         Balance = 0,
                         BranchId = 1,
                         ParentAcNum = "",
                         IsActive = true,
                         AccountNature = AccountNatureEnum.Debit
                     },
                     new AccountChart()
                     {
                         AccountName = "Clients",
                         AccountNameAr = "عملاء",
                         AccNum = "1230000001",
                         AccTypeId = 6,
                         IsParent = true,
                         CurrencyId = 1,
                         Balance = 0,
                         BranchId = 1,
                         ParentAcNum = "1230000000",
                         IsActive = true,
                         AccountNature = AccountNatureEnum.Debit
                     },
                      new AccountChart()
                      {
                          AccountName = "Checks",
                          AccountNameAr = "شيكات",
                          AccNum = "1240000000",
                          AccTypeId = 7,
                          IsParent = true,
                          CurrencyId = 1,
                          Balance = 0,
                          BranchId = 1,
                          ParentAcNum = "",
                          IsActive = true,
                          AccountNature = AccountNatureEnum.Debit
                      },
                       new AccountChart()
                       {
                           AccountName = "Checks In Safe",
                           AccountNameAr = "شيكات في الخزنة",
                           AccNum = "1240000001",
                           AccTypeId = 7,
                           IsParent = false,
                           CurrencyId = 1,
                           Balance = 0,
                           BranchId = 1,
                           ParentAcNum = "1240000000",
                           IsActive = true,
                           AccountNature = AccountNatureEnum.Debit
                       },
                        new AccountChart()
                        {
                            AccountName = "Checks In Bank",
                            AccountNameAr = "شيكات في البنك",
                            AccNum = "1240000002",
                            AccTypeId = 7,
                            IsParent = false,
                            CurrencyId = 1,
                            Balance = 0,
                            BranchId = 1,
                            ParentAcNum = "1240000000",
                            IsActive = true,
                            AccountNature = AccountNatureEnum.Debit
                        },
                        new AccountChart()
                        {
                            AccountName = "Bounced Checks",
                            AccountNameAr = "شيكات مرتدة",
                            AccNum = "1240000003",
                            AccTypeId = 7,
                            IsParent = false,
                            CurrencyId = 1,
                            Balance = 0,
                            BranchId = 1,
                            ParentAcNum = "1240000000",
                            IsActive = true,
                            AccountNature = AccountNatureEnum.Debit
                        },
                      new AccountChart()
                      {
                          AccountName = "Store",
                          AccountNameAr = "مخزن",
                          AccNum = "1250000000",
                          AccTypeId = 8,
                          IsParent = true,
                          CurrencyId = 1,
                          Balance = 0,
                          BranchId = 1,
                          ParentAcNum = "",
                          IsActive = true,
                          AccountNature = AccountNatureEnum.Debit
                      },
                        new AccountChart()
                        {
                            AccountName = "Custody",
                            AccountNameAr = "عهد",
                            AccNum = "1261000000",
                            AccTypeId = 9,
                            IsParent = true,
                            CurrencyId = 1,
                            Balance = 0,
                            BranchId = 1,
                            ParentAcNum = "",
                            IsActive = true,
                            AccountNature = AccountNatureEnum.Debit
                        },
                        new AccountChart()
                        {
                            AccountName = "StaffAdvances",
                            AccountNameAr = "سلف",
                            AccNum = "1262000000",
                            AccTypeId = 10,
                            IsParent = true,
                            CurrencyId = 1,
                            Balance = 0,
                            BranchId = 1,
                            ParentAcNum = "",
                            IsActive = true,
                            AccountNature = AccountNatureEnum.Debit
                        },
                        new AccountChart()
                        {
                            AccountName = "Suppliers Advances",
                            AccountNameAr = "دفعات مقدمة للموردين",
                            AccNum = "1263000000",
                            AccTypeId = 11,
                            IsParent = true,
                            CurrencyId = 1,
                            Balance = 0,
                            BranchId = 1,
                            ParentAcNum = "",
                            IsActive = true,
                            AccountNature = AccountNatureEnum.Debit
                        },
                         new AccountChart()
                         {
                             AccountName = "OtherCurrentAsset",
                             AccountNameAr = "أصول متداولة أخري",
                             AccNum = "1269000000",
                             AccTypeId = 12,
                             IsParent = true,
                             CurrencyId = 1,
                             Balance = 0,
                             BranchId = 1,
                             ParentAcNum = "",
                             IsActive = true,
                             AccountNature = AccountNatureEnum.Debit
                         },
                           new AccountChart()
                           {
                               AccountName = "NotePayable",
                               AccountNameAr = "شيكات موردين",
                               AccNum = "2170000000",
                               AccTypeId = 13,
                               IsParent = true,
                               CurrencyId = 1,
                               Balance = 0,
                               BranchId = 1,
                               ParentAcNum = "",
                               IsActive = true,
                               AccountNature = AccountNatureEnum.Credit
                           },
                           new AccountChart()
                           {
                               AccountName = "NotePayable",
                               AccountNameAr = "شيكات موردين",
                               AccNum = "2170000001",
                               AccTypeId = 13,
                               IsParent = true,
                               CurrencyId = 1,
                               Balance = 0,
                               BranchId = 1,
                               ParentAcNum = "2170000000",
                               IsActive = false,
                               AccountNature = AccountNatureEnum.Credit
                           },
                          new AccountChart()
                          {
                              AccountName = "Suppliers",
                              AccountNameAr = "موردين",
                              AccNum = "2210000000",
                              AccTypeId = 14,
                              IsParent = true,
                              CurrencyId = 1,
                              Balance = 0,
                              BranchId = 1,
                              ParentAcNum = "",
                              IsActive = true,
                              AccountNature = AccountNatureEnum.Credit
                          },
                           new AccountChart()
                           {
                               AccountName = "Taxes",
                               AccountNameAr = "ضرائب",
                               AccNum = "2220000000",
                               AccTypeId = 15,
                               IsParent = true,
                               CurrencyId = 1,
                               Balance = 0,
                               BranchId = 1,
                               ParentAcNum = "",
                               IsActive = true,
                               AccountNature = AccountNatureEnum.Credit
                           },
                           new AccountChart()
                           {
                               AccountName = "Creditors",
                               AccountNameAr = "دائنون",
                               AccNum = "2230000000",
                               AccTypeId = 16,
                               IsParent = true,
                               CurrencyId = 1,
                               Balance = 0,
                               BranchId = 1,
                               ParentAcNum = "",
                               IsActive = true,
                               AccountNature = AccountNatureEnum.Credit
                           },
                            new AccountChart()
                            {
                                AccountName = "Accrued Expenses",
                                AccountNameAr = "مصروفات مستحقة",
                                AccNum = "2240000000",
                                AccTypeId = 17,
                                IsParent = true,
                                CurrencyId = 1,
                                Balance = 0,
                                BranchId = 1,
                                ParentAcNum = "",
                                IsActive = true,
                                AccountNature = AccountNatureEnum.Credit
                            },
                             new AccountChart()
                             {
                                 AccountName = "Advances Income",
                                 AccountNameAr = "ايرادات مقدمة",
                                 AccNum = "2250000000",
                                 AccTypeId = 18,
                                 IsParent = true,
                                 CurrencyId = 1,
                                 Balance = 0,
                                 BranchId = 1,
                                 ParentAcNum = "",
                                 IsActive = true,
                                 AccountNature = AccountNatureEnum.Credit
                             },
                              new AccountChart()
                              {
                                  AccountName = "Advances Income",
                                  AccountNameAr = "ايرادات مقدمة",
                                  AccNum = "2250000001",
                                  AccTypeId = 18,
                                  IsParent = true,
                                  CurrencyId = 1,
                                  Balance = 0,
                                  BranchId = 1,
                                  ParentAcNum = "2250000000",
                                  IsActive = false,
                                  AccountNature = AccountNatureEnum.Credit
                              },
                               new AccountChart()
                               {
                                   AccountName = "Other Current Liabilities",
                                   AccountNameAr = "التزمات متداولة أخري",
                                   AccNum = "2260000000",
                                   AccTypeId = 24,
                                   IsParent = true,
                                   CurrencyId = 1,
                                   Balance = 0,
                                   BranchId = 1,
                                   ParentAcNum = "",
                                   IsActive = true,
                                   AccountNature = AccountNatureEnum.Credit
                               },
                            new AccountChart()
                            {
                                AccountName = "Incomes",
                                AccountNameAr = "إيرادات",
                                AccNum = "3110000000",
                                AccTypeId = 19,
                                IsParent = true,
                                CurrencyId = 1,
                                Balance = 0,
                                BranchId = 1,
                                ParentAcNum = "",
                                IsActive = true,
                                AccountNature = AccountNatureEnum.Credit
                            },
                            new AccountChart()
                            {
                                AccountName = "Expenses",
                                AccountNameAr = "مصروفات",
                                AccNum = "4111000000",
                                AccTypeId = 20,
                                IsParent = true,
                                CurrencyId = 1,
                                Balance = 0,
                                BranchId = 1,
                                ParentAcNum = "",
                                IsActive = true,
                                AccountNature = AccountNatureEnum.Debit
                            },
                            new AccountChart()
                            {
                                AccountName = "Purchases",
                                AccountNameAr = "مشتريات",
                                AccNum = "4112000000",
                                AccTypeId = 21,
                                IsParent = true,
                                CurrencyId = 1,
                                Balance = 0,
                                BranchId = 1,
                                ParentAcNum = "",
                                IsActive = true,
                                AccountNature = AccountNatureEnum.Debit
                            },
                              new AccountChart()
                              {
                                  AccountName = "Capital",
                                  AccountNameAr = "رأس المال",
                                  AccNum = "5110000000",
                                  AccTypeId = 22,
                                  IsParent = true,
                                  CurrencyId = 1,
                                  Balance = 0,
                                  BranchId = 1,
                                  ParentAcNum = "",
                                  IsActive = true,
                                  AccountNature = AccountNatureEnum.Credit
                              },

                               new AccountChart()
                               {
                                   AccountName = "WithDrawls",
                                   AccountNameAr = "مسحوبات",
                                   AccNum = "5120000000",
                                   AccTypeId = 23,
                                   IsParent = true,
                                   CurrencyId = 1,
                                   Balance = 0,
                                   BranchId = 1,
                                   ParentAcNum = "",
                                   IsActive = true,
                                   AccountNature = AccountNatureEnum.Credit
                               }
                );
            #endregion

        }
    }
}
