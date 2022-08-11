using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class TippMessageQueueModel
    {
        public int TemplateID { get; set; }

        public string MSQPath { get; set; }
    }
}
