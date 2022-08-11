using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ModuleActivityResultModel
    {
        public int AcademicYear { get; set; }

        public int ModuleID { get; set; }

        public string ModuleName { get; set; }

        public int ModuleActivityID { get; set; }

        public int ActivityID { get; set; }

        public string ActivityName { get; set; }

        public string ActivityMarkValue { get; set; }

        public string ModifiedActivityMarkValue { get; set; }

        public string ActivityDate { get; set; }

        public string Credit { get; set; }

        public int SeqOrder { get; set; }
    }
}
