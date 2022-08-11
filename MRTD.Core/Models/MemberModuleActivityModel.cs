using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class MemberModuleActivityModel
    {
        public Guid MemberGuid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ModuleActivityID { get; set; }

        public string ModuleName { get; set; }
        public string UnitCode { get; set; }

        public string ActivityName { get; set; }

        public string ActivityMark { get; set; }
    }
}
