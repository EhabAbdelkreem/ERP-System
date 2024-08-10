using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model
{
    [Table("Finance_GL_Journal")]
    public class Journal
    {
        [Key, Required, StringLength(15)]
        public string TransId { get; set; }

        [Required]
        public DateTimeOffset EntryDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime TransDate { get; set; }

        [Required]
        public string TransDesc { get; set; }

        public string DocName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransCount { get; set; }

        public SystemModulesEnum SystemModule { get; set; }

        public string UserName { get; set; }
    }
}
