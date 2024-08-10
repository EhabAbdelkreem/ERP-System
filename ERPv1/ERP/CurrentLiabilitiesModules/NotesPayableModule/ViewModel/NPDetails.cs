using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
    public class NPDetails
    {
        
        public string ChkNum { get; set; }

        public string WritingDate { get; set; }

        public string DueDate { get; set; }

        public decimal AmountLocal { get; set; }

        public decimal AmountForgin { get; set; }

     
        public int CurrencyId { get; set; }
        
        public string CurrencyAbbrev { get; set; }

        public string BankAccountNum { get; set; }
        public string BankAccountName { get; set; }
        public int SupplierId { get; set; }       
        public string SupplierName { get; set; }

        public decimal Paid { get; set; }
    }
}
