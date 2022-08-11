using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class FinancialAccountModel
    {
        public DateTime FinancialDate { get; set; }

        public string ReferenceNo { get; set; }

        public string Allocation { get; set; }

        public string Description { get; set; }

        public double Debit { get; set; }

        public double Credit { get; set; }

        public double Balance { get; set; }
    }
}
