using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.Model
{
    [Table("Finance_CurrentAsset_CheckLocation")]

    public class CheckLocation
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string CheckLocationEN { get; set; }
        [Required, StringLength(255)]
        public string CheckLocationAR { get; set; }

        public bool IsDefualt { get; set; }
    }
}