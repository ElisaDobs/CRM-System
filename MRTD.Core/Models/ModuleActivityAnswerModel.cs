using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ModuleActivityAnswerModel : BaseModel
    {
        public int LookupAnswerID { get; set; }

        public string LookupAnswerName { get; set; }

        public bool LookupSelected { get; set; }

        public int Score { get; set; }
    }
}
