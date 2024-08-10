using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main
{
    [Table("Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails")]
    public class StoreItemBalanceDetails
    {
        public int Id { get; set; }
        public int StoreItemId { get; set; }
        [ForeignKey("StoreItemId")]
        public StoreItem StoreItem { get; set; }

        public int StoreTransactionId { get; set; }
        [ForeignKey("StoreTransactionId")]
        public StoreTransaction StoreTransaction { get; set; }
       
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; }
        [Column(TypeName ="Date")]
        public DateTime? ExpiryDate { get; set; }



    }
}
