using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class RequiredDocumentModel : BaseModel
    {
        public string MemberID { get; set; }

        public IFormFile IDDocument { get; set; }

        public IFormFile MatricCertificate { get; set; }
    }
}
