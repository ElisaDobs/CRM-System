using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRTD.Core.Models
{
    public class ModuleModel : BaseModel
    {
        [Display(Name = "Name")]
        public string ModuleName { get; set; }

        [Display(Name = "Description")]
        public string ModuleDescription { get; set; }

        [Display(Name = "Unit ID")]
        public string ModuleUnitCode { get; set; }

        [Display(Name = "Academic Level")]
        public int AcademicLevelID { get; set; }

        [Display(Name = "NQF Level")]
        public int NQLevelID { get; set; }

        [Display(Name = "Credits")]
        public int Credit { get; set; }

        [Display(Name = "Module Price")]
        public decimal StockPrice { get; set; }

        public SelectList AcademicLevels;

        public SelectList ModuleNQLevels;

        public string MemberID { get; set; }

        public string QualificationID { get; set; }

        public string FacultyID { get; set; }

        public string ModuleID { get; set; }
    }
}
