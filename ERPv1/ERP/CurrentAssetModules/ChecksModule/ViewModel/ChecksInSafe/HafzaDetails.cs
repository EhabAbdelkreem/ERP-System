using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.ChecksInSafe
{
    public class HafzaDetails
    {
        public int Id { get; set; }
        [Required]
        public string BankAccNum { get; set; }
        [Required]
        public string HafzaDate { get; set; }
        [Required, StringLength(255)]
        public string HafzaName { get; set; }
    }
}
