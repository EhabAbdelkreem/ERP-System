using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.ViewModel
{
    public class TaxBrackets
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal TaxPrecentage { get; set; }
        public bool IsLastBraket { get; set; }

        public decimal MinRange { get; set; }
        public decimal MaxRange { get; set; }
        public int StartBrakcet { get; set; }

        public bool WithMaxValue { get; set; }
    }
}
