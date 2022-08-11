using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class MenuModel
    {
        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public int ActivityID { get; set; }
    }
}
