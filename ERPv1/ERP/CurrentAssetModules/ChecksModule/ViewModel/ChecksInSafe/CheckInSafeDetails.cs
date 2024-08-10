using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.ChecksInSafe
{
    public class CheckInSafeDetails
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
        public string ClientName { get; set; }
        public string CheckNum { get; set; }
        public decimal CheckAmount { get; set; }
        public string DueDate { get; set; }
        public string OrginalBank { get; set; }
        public string CheckStatus { get; set; }
        public decimal? Paid { get; set; }
        public decimal? UnPaid { get; set; }
        public string CurrencyAbbr { get; set; }
        public decimal CheckAmountForiegn { get; set; }
    }
}
