using MRTD.Core.Common;
using MRTD.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MRTD.Core.Notification
{
    public static class BusinessNotification
    {
        public static void ProcessNotice(MemberActivityModel model, string mailServer, string port, string fromEmail, string fromPassword, string fromEmailHead)
        {
            var fromAddress = new MailAddress(fromEmail, fromEmailHead);

            var toAddress = new MailAddress(model.EmailAddress, model.FullName);

            var smtp = new SmtpClient
            {
                Host = mailServer,
                Port = Int32.Parse(port),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = model.EmailSubject,
                Body = model.EmailBody,
                IsBodyHtml = true
            })
            {
                try
                {
                    if (!string.IsNullOrEmpty(model.EmailAttachment))
                    {
                        using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(model.EmailAttachment)))
                        {
                            message.Attachments.Add(new Attachment(stream, string.Format("{0}.pdf", model.EmailSubject), "application/pdf"));
                            smtp.Send(message);
                        }
                    }
                    else
                    {
                        smtp.Timeout = 120000;
                        smtp.Send(message);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }
    }
}
