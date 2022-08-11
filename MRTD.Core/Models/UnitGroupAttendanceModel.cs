using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class UnitGroupAttendanceModel : BaseModel
    {
        public int GroupID { get; set; }

        public string UserID { get; set; }

        public string MemberID { get; set; }
    }
}
