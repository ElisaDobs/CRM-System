using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ModuleGroupModel
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public int ModuleID { get; set; }

        public DateTime AttDate { get; set; }

        public TimeSpan AttTime { get; set; }

        public int MemberID { get; set; }
    }
}
