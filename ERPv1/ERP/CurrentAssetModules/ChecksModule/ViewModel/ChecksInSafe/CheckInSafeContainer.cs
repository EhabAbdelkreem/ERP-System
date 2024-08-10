using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.ChecksModule.ViewModel.ChecksInSafe
{
    public class CheckInSafeContainer
    {
        public CheckInSafeContainer()
        {
            HafzaDetails = new HafzaDetails();
            CheckDetails = new List<CheckInSafeDetails>();
        }
        public HafzaDetails HafzaDetails { get; set; }
        public List<CheckInSafeDetails> CheckDetails { get; set; }
    }
}
