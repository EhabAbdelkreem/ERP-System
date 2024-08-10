using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model
{
    [Table("Finance_GL_JournalDetails")]
    public class JournalDetails
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        public string TransId { get; set; }

        [ForeignKey("TransId")]
        public Journal Trans { get; set; }

        [StringLength(15)]
        public string AccNum { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999999999999.99)]
        public decimal Amount { get; set; }
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountLocal { get; set; }

        public TransactionSidesEnum Side { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999999999999.99)]
        public decimal BalanceAfter { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999999999999.99)]
        public decimal UsedRate { get; set; }
    }
}
