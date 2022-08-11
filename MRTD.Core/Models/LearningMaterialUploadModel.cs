using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class LearningMaterialUploadModel
    {
        public int UnitID { get; set; }

        public string MemberID { get; set; }

        public int UploadType { get; set; }

        public IFormFile Upload { get; set; }
    }
}
