using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class MemberModuleChatModel
    {
        public int MemberID { get; set; }
	    public string FullName { get; set; }
	    public string ChatName { get; set; }
	    public string ModuleName { get; set; }
	    public bool Active { get; set; }
    }
}
