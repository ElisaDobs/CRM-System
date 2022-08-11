using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class RegisteredUnitModel
    {
        public string ModuleName { get; set; }

        public string UnitCode { get; set; }

        public int ModuleID { get; set; }

        public int ModuleNQLevel { get; set; }

		public int Credit { get; set; }
    }
}
