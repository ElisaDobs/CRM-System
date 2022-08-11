using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class MessagePostModel
    {
        public string MemberID { get; set; }

        public string FullName { get; set; }

        public string PostText { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public bool LoggedIn { get; set; } 
    }
}
