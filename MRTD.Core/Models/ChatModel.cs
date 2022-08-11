using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ChatModel
    {
        public string FullName { get; set; }

        public Guid MemberGuid { get; set; }

        public string MessageText { get; set; }

        public DateTime DateTimeStamp { get; set; }
    }
}
