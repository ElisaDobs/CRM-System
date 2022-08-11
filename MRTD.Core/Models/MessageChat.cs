using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class MessageChatModel : BaseModel
    {
        public int ModuleID { get; set; }

        public string MemberID { get; set; }

        public int ChatID { get; set; }

        public string MessageText { get; set; }
    }
}
