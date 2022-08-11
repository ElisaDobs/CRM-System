using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class FacultyQualModel : BaseModel
    {
        public int QualificationID { get; set; }

        public string QualificationName { get; set; }

        public int QualificationNQLevel { get; set; }

        public string UnitCode { get; set; }

        public string QualificationType { get; set; }

        public int QualificationTypeID { get; set; }

        public int Credit { get; set; }

        public string QualificationDescription { get; set; }
    }
}
