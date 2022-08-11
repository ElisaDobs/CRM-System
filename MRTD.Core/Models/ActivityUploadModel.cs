using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MRTD.Core.Models
{
    public class ActivityUploadModel
    {
        public int ModuleActivityID { get; set; }

        public int MemberID { get; set; }

        public IFormFile ActivityUpload { get; set; }
    }
}
