using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.Model
{
    [Table("Finance_CurrentAsset_Checks")]
    public class Check
    {
        public int Id { get; set; }


        [Required, StringLength(255)]
        public string ChkNum { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DueDate { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }


        [Required, Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountLocal { get; set; }


        [Required, Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountForgin { get; set; }
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public Contacts Contact { get; set; }
        [StringLength(255)]
        public string OrginalBank { get; set; }
        // Collection Part

        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Paid { get; set; }


        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnPaid { get; set; }

        public int CheckStatusId { get; set; }
        [ForeignKey("CheckStatusId")]
        public CheckStatus CheckStatus { get; set; }

        public int CheckLocationId { get; set; }
        [ForeignKey("CheckLocationId")]
        public CheckLocation CheckLocation { get; set; }

        // Current Bank
        [StringLength(50)]
        public string BankAccNum { get; set; }

        [ForeignKey("BankAccNum")]
        public AccountChart BankAcc { get; set; }

        public int? HafzaId { get; set; }
        [ForeignKey("HafzaId")]
        public CheckHafza CheckHafza { get; set; }

    }
}