using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class DocumentVerifyModel : BaseModel
    {
        public string ProspectiveID { get; set; }

        public int Approved { get; set; }

        public string ApproverID { get; set; }

        public string EnrollmentStatusID { get; set; }

        public string DisApproveComments { get; set; }
    }
}
