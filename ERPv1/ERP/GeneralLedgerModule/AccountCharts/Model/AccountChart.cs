using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{
    [Table("Finance_GL_AccountChart")]
    public class AccountChart
    {
        [Key, StringLength(50)]
        public string AccNum { get; set; }

        [Required, StringLength(150)]
        public string AccountName { get; set; }

        [StringLength(150)]
        public string AccountNameAr { get; set; }

        public int AccTypeId { get; set; }


        [ForeignKey("AccTypeId")]
        public AccountChartCounter AccType { get; set; }

        public AccountNatureEnum AccountNature { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingBalance { get; set; }


        public bool IsParent { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [StringLength(50)]
        public string ParentAcNum { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
    }
}
