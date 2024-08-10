using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Services
{
    public class StoreItemAccountManager : IStoreItemAccountManager
    {
        private readonly IAccountGenerator _accountGenerator;

        public StoreItemAccountManager(IAccountGenerator accountGenerator)
        {
            _accountGenerator = accountGenerator;
        }

        public StoreAccountsHelper GenerateStoreItemAccounts(StoreItemCreationVM vm)
        {
            var Accounts = new StoreAccountsHelper();

            // Store AccNum
            var storeAcc = new CreateAccountVM();
            storeAcc.AccountName = "Store- " + vm.Name;
            storeAcc.AccountNameAr = "مخزون- " + vm.NameAr;
            storeAcc.AccTypeId = 8;
            storeAcc.BranchId = 1;
            storeAcc.CurrencyId = 1;
            Accounts.StoreAccNum = _accountGenerator.CreateNewAccount(storeAcc);

            // PurchaseAccNum
            var purchaseAcc = new CreateAccountVM();
            purchaseAcc.AccountName = "Purchase- " + vm.Name;
            purchaseAcc.AccountNameAr = "مشتريات- " + vm.NameAr;
            purchaseAcc.AccTypeId = 21;
            purchaseAcc.BranchId = 1;
            purchaseAcc.CurrencyId = 1;
            Accounts.PurchaseAccNum = _accountGenerator.CreateNewAccount(purchaseAcc);
            return Accounts;
        }

    }
}
