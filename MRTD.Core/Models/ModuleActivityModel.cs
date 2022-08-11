using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRTD.Core.Models
{
    public class ModuleActivityModel : BaseModel
    {
        public int ModuleActivityID { get; set; }

        [Display(Name = "Module Name")]
        public int ModuleID { get; set; }

        [Display(Name = "Activity Name")]
        public int ActivityID { get; set; }

        [Display(Name = "Activity Date")]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "Activity Time")]
        public string ActivityTime { get; set; }

        [Display(Name = "Activity Duration")]
        public int ActivityDuration { get; set; }

        [Display(Name = "Activity Pass Mark")]
        public int ActivityPassMark { get; set; }

        [Display(Name = "Activity Mark Total")]
        public int ActivityTotalMark { get; set; }

        [Display(Name = "Is Online")]
        public bool IsOline { get; set; }

        [Display(Name = "Activity Assessor")]
        public int AssessorID { get; set; }

        public SelectList ActivitList;

        public SelectList ModuleList;

        public SelectList ActivityAssessors;

        public string MemberID { get; set; }

        public string ActivityName { get; set; }
    }
}
