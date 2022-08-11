using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.Models
{
    public class NoticeMessageQueueModel
    {
        public string MailServer { get; set; }
        public int Port { get; set; }

        public void Send(string queueName)
        {
            
        }
    }
}
