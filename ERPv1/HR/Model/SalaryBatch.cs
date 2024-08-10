using ERPv1.ERP.GeneralLedgerModule.JournalModeule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.Model
{
    [Table("HR_SalaryBatch")]
    public class SalaryBatch
    {
        public int Id { get; set; }
        public int BatchMonth { get; set; }
        public int BatchYear { get; set; }
        public string TransId { get; set; }
    }
}
