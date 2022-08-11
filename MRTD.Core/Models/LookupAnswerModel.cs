using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class LookupAnswerModel : BaseModel
    {
        public int MemberID { get; set; }

        public int QuestionID { get; set; }

        public int LookupAnswerID { get; set; }
    }
}
