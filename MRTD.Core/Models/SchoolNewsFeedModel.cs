using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class SchoolNewsFeedModel
    {
        public int FeedID { get; set; }

        public string FeedHeader { get; set; }

        public string ModuleName { get; set; }

        public string FullName { get; set; }

        public DateTime DatetimeStamp { get; set; }
    }
}
