using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class MemberViewModel
    {
        public string MUID { get; set; }
        public string UID { get; set; }
        public UnitGroupModel Group { get; set; }
        public string ModuleName { get; set; }
        public int ChatID { get; set; }
        public int QualID { get; set; }
        public int ActivityID { get; set; }
        public int ModuleID { get; set; }
    }
}
