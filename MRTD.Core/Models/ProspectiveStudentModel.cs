using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ProspectiveStudentModel
    {
        public Guid MemberGuid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string CellNo { get; set; }

        public string StudentNumber { get; set; }

        public string IDNo { get; set; }

        public string ErollmentStatusName { get; set; }

        public int AcademicYear { get; set; }

        public string QualificationName { get; set; }
    }
}
