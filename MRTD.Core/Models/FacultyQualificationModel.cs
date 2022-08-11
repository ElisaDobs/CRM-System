using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class FacultyQualificationModel
    {
        public int QualificationID { get; set; }

        public int FacultyID { get; set; }

        public int ModuleID { get; set; }
        public string MemberGuid { get; set; }
    }
}
