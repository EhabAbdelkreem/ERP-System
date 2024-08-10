using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Model.Settings
{
    [Table("Finance_CurrentAsset_Inventory_Settings_Brand")]
    public class Brand
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string BrandName { get; set; }
    }
}
