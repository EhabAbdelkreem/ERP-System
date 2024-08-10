using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces
{
    public interface IStoreItemAccountManager
    {
        StoreAccountsHelper GenerateStoreItemAccounts(StoreItemCreationVM vm);
    }
}