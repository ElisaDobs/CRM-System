using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class StudyMaterialModel
    {
         public int UploadID { get; set; }

         public string UploadFileName { get; set; }

         public int UploadTypeID { get; set; }

         public string UploadTypeName { get; set; }

         public string ModuleName { get; set; }
    }
}
