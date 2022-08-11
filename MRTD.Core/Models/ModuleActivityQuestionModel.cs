using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRTD.Core.Models
{
    public class ModuleActivityQuestionModel : BaseModel
    {   
        public int ActivityQuestionID { get; set; }

        public string MemberID { get; set; }

        public int ModuleActivityID { get; set; }

        [Display(Name = "Question")]
        public string QuestionName { get; set; }
        
        [Display(Name = "Parent Question")]
        public int ParentID { get; set; }

        [Display(Name="Control Type")]
        public int FieldTypeID { get; set; }

        public SelectList QuestionParent;

        public SelectList FielTypeName;
    }
}
