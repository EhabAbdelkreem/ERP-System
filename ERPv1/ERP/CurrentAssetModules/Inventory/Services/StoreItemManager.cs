using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Services
{
    public class StoreItemManager : IStoreItemManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IStoreItemAccountManager _storeItemAccountManager;

        public StoreItemManager(ApplicationDbContext db, IMapper mapper,
            IStoreItemAccountManager storeItemAccountManager)
        {
            _db = db;
            _mapper = mapper;
            _storeItemAccountManager = storeItemAccountManager;
        }

        public async Task<IEnumerable<StoreItem>> GetAllStoreItems() => await
            _db.StoreItems.Include(x => x.Brand).Include(x => x.ProductType).Include(x => x.UnitMeasure)
            .ToListAsync();

        public StoreItem GetStoreItemById(int Id) =>
             _db.StoreItems.Include(x => x.Brand).Include(x => x.ProductType).Include(x => x.UnitMeasure)
            .FirstOrDefault(x => x.Id == Id);

        public void CreateStoreItem(StoreItemCreationVM store)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var StoreItem = _mapper.Map<StoreItem>(store);
                    var Accounts = _storeItemAccountManager.GenerateStoreItemAccounts(store);
                    StoreItem.StoreAccNum = Accounts.StoreAccNum;
                    StoreItem.PurchaseAccNum = Accounts.PurchaseAccNum;
                    _db.StoreItems.Add(StoreItem);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var errors = ex.Message;
                    transaction.Rollback();
                }
            }




        }

    }
}
