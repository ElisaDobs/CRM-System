using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRTD.Core.Models
{
    public class NewFeedModel : BaseModel
    {
        public int FeedID { get; set; }
        [Display(Name = "News Feed Heading")]
        public string FeedHeader { get; set; }
        [Display(Name = "Feed Description")]
        public string FeedText { get; set; }
        [Display(Name = "Study Unit")]
        public int ModuleID { get; set; }
        public string MemberID { get; set; }

        public SelectList ModuleList;
    }
}
