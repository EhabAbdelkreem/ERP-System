using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.ERPSettings.Model
{
    [Table("Finance_Settings_Branch")]
    public class Branch
    {
        public int Id { get; set; }
        [Required,StringLength(255)]
        public string BranchName { get; set; }
    }
}
