using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.Model
{
    [Table("Finance_CurrentAsset_CheckHafza")]
    public class CheckHafza
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string HafzaName { get; set; }
        [StringLength(50)]
        public string BankAccNum { get; set; }
        public DateTime? HafzaDate { get; set; }
    }
}