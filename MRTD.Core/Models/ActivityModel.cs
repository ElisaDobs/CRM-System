using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ActivityModel : BaseModel
    {
        public int ActivityID { get; set; }
        
        public string ActivityName { get; set; }

        public string ActivityDescription { get; set; }
    }
}
