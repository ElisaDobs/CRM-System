using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class MessagePostChatModel
    {
        public int PostID { get; set; }
	    public string PostText { get; set; }
	    public int MemberID { get; set; }
        public string FullName { get; set; }
	    public bool IsRightAlign { get; set; }
        public DateTime DatetimeStamp { get; set; }
    }
}
