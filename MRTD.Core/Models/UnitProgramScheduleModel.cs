using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class UnitProgramScheduleModel
    {
        public string ModuleName { get; set; }

        public int AcademicYear { get; set; }
        
        public string Credit { get; set; }

        public List<KeyValuePair<string, string>> ModuleActivity { get; set; }
    }
}
