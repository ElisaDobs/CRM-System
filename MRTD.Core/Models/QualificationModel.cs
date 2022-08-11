using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRTD.Core.Models
{
    public class QualificationModel : BaseModel
    {
        [Display(Name = "Name")]
        public string QualificationName { get; set; }

        [Display(Name = "Description")]
        public string QualificationDescription { get; set; }

        [Display(Name = "Unit ID")]
        public string QualificationUnitCode { get; set; }

        [Display(Name = "NQF Level")]
        public int NQLevelID { get; set; }

        [Display(Name = "Program")]
        public int QualTypeID { get; set; }

        [Display(Name = "Credits")]
        public int Credit { get; set; }

        public SelectList QualificationNQLevel;

        public SelectList QualificationTypes;

        public string MemberID { get; set; }

        public int FacultyID { get; set; }

        public int QualificationID { get; set; }
    }
}
