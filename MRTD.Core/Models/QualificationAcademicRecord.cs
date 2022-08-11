using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class QualificationAcademicRecord
    {
        public int QualificationID { get; set; }

        public string QualificationName { get; set; }

        public string UnitCode { get; set; }

        public string QualType { get; set; }

        public int AcademicYear { get; set; }

        public string Status { get; set; }

        public int NQLevel { get; set; }
    }
}
