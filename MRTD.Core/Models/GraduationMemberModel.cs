using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class GraduationMemberModel
    {
       public string FullName { get; set; }

       public string IDNo { get; set; }

       public string CellNo { get; set; }

       public string QualificationName { get; set; }
       
       public string UnitCode { get; set; }

       public int AcademicYear { get; set; }

       public DateTime CompletedDateTime { get; set; }
    }
}
