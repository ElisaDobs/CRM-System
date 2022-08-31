using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class StudentModel
    {
        public Guid StudentGuid { get; set; }
        public string IDNo { get; set; }
        public string StudentNumber { get; set; }
        public int AcademicYear { get; set; }
        public string FullName { get; set; }
        public string QualificationName { get; set; }
        public int QualificationID { get; set; }
        public string QualificationStatus { get; set; }
        public int StartYear { get; set; }
        public int CompletedYear { get; set; }
    }
}
