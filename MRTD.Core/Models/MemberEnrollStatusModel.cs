using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class MemberEnrollStatusModel
    {
        public int ErollmentStatusID { get; set; }

		public string ErollmentStatusName { get; set; }

        public bool EnrollStatus { get; set; }

        public bool ToActioned { get; set; }
    }
}
