using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MRTD.Core.Models
{
    public class SignedAcceptenceLetterModel
    {
        public string MemberID { get; set; }

        public int UploadTypeID { get; set; }

        public IFormFile SignedLetter { get; set; }
    }
}
