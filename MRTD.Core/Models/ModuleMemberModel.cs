using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ModuleMemberModel
    {
        public Guid MemberGuid { get; set; }

        public int MemberID { get; set; }

        public string FullName { get; set; }

        public string CellNo { get; set; }
        public string EmailAddress { get; set; }
        public int ModuleID { get; set; }
        public bool LoggedIn { get; set; }
    }
}
