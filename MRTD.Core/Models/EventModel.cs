using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class EventScheduleModel
    {
        public int EventDay { get; set; }

        public string EventName { get; set; }

        public string EventDesc { get; set; }

        public string EventTime { get; set; }

		public int EventDuration { get; set; }
    }
}
