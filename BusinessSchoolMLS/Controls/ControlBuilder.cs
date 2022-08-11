using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessSchoolMLS.Controls
{
    public class ControlBuilder : IHtmlBuilder
    {
        private readonly IHtmlControl htmlControl;
        public ControlBuilder(IHtmlControl htmlControl)
        {
            this.htmlControl = htmlControl;
        }

        public string Get()
        {
            return htmlControl.GetControl();
        }
    }
}
