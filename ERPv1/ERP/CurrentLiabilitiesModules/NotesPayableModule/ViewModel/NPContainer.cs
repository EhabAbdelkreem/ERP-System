using ERPv1.ERP.PurchasesModule.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
    public class NPContainer
    {
        public NPContainer()
        {
            CheckCashCollection = new List<NPDetails>();
            CheckUnderCollection = new List<NPDetails>();
            PaymentDetails = new PaymentDetails();
            SelectedNote = new NPDetails();
        }
        public List<NPDetails> CheckUnderCollection { get; set; }
        public List<NPDetails> CheckCashCollection { get; set; }

        public NPDetails SelectedNote { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
    }
}
