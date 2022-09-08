using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class UnitGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public DateTime AttDate { get; set; }
        public DateTime AttEndDate { get; set; }
        public TimeSpan AttTime { get; set; }
        public TimeSpan AttEndTime { get; set; }
        public string ModuleName { get; set; }
    }
}
