using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class ModuleActivityUploadModel : BaseModel
    {
        public int ModuleActivityID { get; set; }

        public int MemberID { get; set; }

        public byte[] ActivityUpload { get; set; }
    }
}
