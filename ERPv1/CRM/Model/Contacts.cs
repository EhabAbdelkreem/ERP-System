using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.Model
{
    [Table("CRM_Contacts")]
    public class Contacts
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string NameAR { get; set; }
        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]
        public string Phone1 { get; set; }
        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]
        public string Phone2 { get; set; }
        [StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(50)]
        public string TaxationCard { get; set; }


        public bool IsClient { get; set; }
        [StringLength(50)]
        public string ClientAccNum { get; set; }
        [ForeignKey("ClientAccNum")]
        public AccountChart ClientAccountDetails { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClientBalance { get; set; }
       


        public bool IsSupplier { get; set; }
        
        [StringLength(50)]
        public string SupplierAccNum { get; set; }
        [ForeignKey("SupplierAccNum")]
        public AccountChart SupplierAccountDetails { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SupplierBalance { get; set; }

    }
}
