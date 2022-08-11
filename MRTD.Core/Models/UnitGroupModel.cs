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

        [Display(Name = "Module")]
        public int ModuleID { get; set; }

        [Display(Name = "Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Time")]
        public DateTime AttTime { get; set; }

        public string MemberID { get; set; }

        public SelectList ModuleList;
    }
}
