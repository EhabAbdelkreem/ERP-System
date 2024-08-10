using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{
    [Table("Finance_GL_FiniacialPeriod")]
    public class FiniacialPeriod
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string YearName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
