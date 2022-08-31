using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class UnitUploadFile
    {
         public int ModuleID { get; set; }
         public List<MemberMarkModel> MemberMarks { get; set; }
    }
}
