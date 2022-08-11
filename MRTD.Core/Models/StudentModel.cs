using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class StudentModel
    {
        public string StudentNumber { get; set; }

        public int AcademicYear { get; set; }

        public string FullName { get; set; }

        public string QualificationName { get; set; }

        public string QualificationStatus { get; set; }
    }
}
