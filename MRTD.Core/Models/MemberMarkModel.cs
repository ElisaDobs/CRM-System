using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class MemberMarkModel
    {
        public string IDNo { get; set; }

        public string UnitName { get; set; }

        public string UnitActivityName { get; set; }

        public int ActivityMark { get; set; }

        public Guid ImportID { get; set; }

        public int UserID { get; set; }

        public string ActivityDate {get; set;}
    }
}
