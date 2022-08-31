using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRTD.Core.Models
{
    public class UnitGroupModel : BaseModel
    {
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Unit Name")]
        public int ModuleID { get; set; }

        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Attendance Time")]
        public DateTime AttTime { get; set; }

        public string MemberID { get; set; }

        public SelectList ModuleList;
    }
}
