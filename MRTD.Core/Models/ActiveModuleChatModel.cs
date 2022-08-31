using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class ActiveModuleChatModel
    {
        public int ChatID { get; set; }
        public int ModuleID { get; set; }
        public string ChatName { get; set; }
        public bool IsChatOpen {get; set; }
		public string ModuleName { get; set; }
        public string FullName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
