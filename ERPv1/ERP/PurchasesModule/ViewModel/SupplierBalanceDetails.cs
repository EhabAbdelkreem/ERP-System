﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel
{
    public class SupplierBalanceDetails
    {
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public decimal LocalAmount { get; set; }
        public decimal Rate { get; set; }
        public string CurrencyAbbr { get; set; }
    }
}