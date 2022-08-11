using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class AlphanumericAnswerModel : BaseModel
    {
        public int MemberID { get; set; }

        public string AlphanumericAnswer { get; set; }

        public int ActivityQuestionID { get; set; }
    }
}
