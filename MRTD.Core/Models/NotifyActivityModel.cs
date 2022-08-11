using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class NotifyActivityModel
    {
        public int MemberID { get; set; }

        public string FullName { get; set; }

        public string CellNo { get; set; }

        public string EmailAddress { get; set; }

        public string ModuleName { get; set; }

        public string ActivityDate { get; set; }

        public string ActivityTime { get; set; }

        public string ActivityName { get; set; }
    }
}
