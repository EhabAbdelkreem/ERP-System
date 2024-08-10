using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using System.ComponentModel.DataAnnotations;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel
{
    public class StoreItemCreationVM
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string BarCode { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string NameAr { get; set; }
        public int ProductTypeId { get; set; }

        public int UnitMeasureId { get; set; }
       
        public int BrandId { get; set; }
       
        public bool WithSN { get; set; }
        public StoreSystem StoreSystem { get; set; }

    }
}