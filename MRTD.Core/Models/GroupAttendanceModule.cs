using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class GroupAttendanceModule
    {
        public string MemberID { get; set; }
        public string GroupName { get; set; } 

        public string FullName { get; set; }

        public string CellNo { get; set; }

        public string EmailAddress { get; set; }

        public DateTime AttDate { get; set; }

        public TimeSpan AttTime { get; set; }
    }
}
