using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ActivityTimeTableModel
    {
        public int ModuleActivityID { get; set; }

        public string Course { get; set; }

        public int AcademicYear { get; set; }

        public string DueDate { get; set; }

        public int Duration { get; set; }

        public string Time { get; set; }

        public int PassMark { get; set; }

        public int TotalMark { get; set; }

        public bool IsOnline { get; set; }
    }
}
