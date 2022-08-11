using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ApplicantRequiredDocument : BaseModel
    {
        public string UploadFileName { get; set; }

	    public int MemberID { get; set; }

	    public int UploadTypeID { get; set; }

        public int UnitID { get; set; }

	    public byte[] UploadDocument { get; set; }

        public string UploadPath { get; set; }
    }
}
