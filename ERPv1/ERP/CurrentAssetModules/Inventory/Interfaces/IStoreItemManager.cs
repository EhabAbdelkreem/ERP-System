using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces
{
    public interface IStoreItemManager
    {
        void CreateStoreItem(StoreItemCreationVM store);
        Task<IEnumerable<StoreItem>> GetAllStoreItems();
        StoreItem GetStoreItemById(int Id);
    }
}