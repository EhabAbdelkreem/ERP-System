using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Infrastructure
{
    public class FeedBack
    {
        public FeedBack()
        {
            Errors = new List<string>();
        }
        public bool Done { get; set; }
        public List<string> Errors { get; set; }
    }
}
