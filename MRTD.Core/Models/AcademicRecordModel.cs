using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class AcademicRecordModel : BaseModel
    {
        public string UnitCode { get; set; }

        public string ModuleName { get; set; }

        public int AcademicYear { get; set; }

        public int ModuleNQLevel { get; set; }

        public string Mark { get; set; }

        public string Remark { get; set; }
    }
}
