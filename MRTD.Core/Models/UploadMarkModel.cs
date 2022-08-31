using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class UploadMarkModel
    {
        public string UserID { get; set; }
        public int ModuleID { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
