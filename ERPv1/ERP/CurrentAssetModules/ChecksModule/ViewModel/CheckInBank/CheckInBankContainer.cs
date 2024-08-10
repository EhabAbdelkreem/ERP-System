using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.CheckInBank
{
    public class CheckInBankContainer
    {
        public CheckInBankContainer()
        {
            CheckDetails = new List<CheckInBankDetails>();
        }
        public List<CheckInBankDetails> CheckDetails { get; set; }
    }
}
