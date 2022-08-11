using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MRTD.Core.Models
{
    public class QuestionLookupAnswerModel : BaseModel
    {
        public int MemberID { get; set; }

        public int QuestionID { get; set; }

        public string LookupAnswerName { get; set; }

        public bool IsCorrectAnswer { get; set; }
        
        public int Score { get; set; }
    }
}
