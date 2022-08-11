using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class DownLoadModel
    {
        public string _member_guid;
        public string MemberID {
            get
            {
                return _member_guid;
            }
            set
            {
                _member_guid = value;
            }
        }

        public string AdminMemberID
        {
            get;
            set;
        }
        public int QualificationID { get; set; }

        public int ModuleID { get; set; }

        public int ActivityID { get; set; }
    }
}
