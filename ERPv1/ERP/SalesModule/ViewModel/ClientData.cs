using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel
{
    public class ClientData
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
    }
}
