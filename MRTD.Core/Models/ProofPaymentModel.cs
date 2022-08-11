using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MRTD.Core.Models
{
    public class ProofPaymentModel
    {
        public string MemberID { get; set; }

        public int UploadTypeID { get; set; }
        
        public IFormFile ProofOfPayemnt { get; set; }
    }
}
