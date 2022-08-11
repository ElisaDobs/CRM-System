using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTD.Core.Models
{
    public class NotificationModel
    {
        public int NotificationID { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public Guid ApplicationId { get; set; }

        public string Password { get; set; }

        public string MessageBody { get; set; }

        public string MailSubject { get; set; }

        public int AttachmentID { get; set; }

        public string MSQPath { get; set; }
    }
}
