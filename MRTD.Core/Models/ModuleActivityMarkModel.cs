using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ModuleActivityMarkModel : BaseModel
    {
        public int ModuleActivityID { get; set; }

        public int MemberID { get; set; }

        public int ActivityMarkValue { get; set; }

        public int UserID { get; set; }

        public int ModuleID { get; set; }
    }
}
