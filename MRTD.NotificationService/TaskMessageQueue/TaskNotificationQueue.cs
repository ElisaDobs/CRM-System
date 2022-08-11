using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using MRTD.Core.Models;
using MRTD.Core.Common;
using MRTD.Core.Encryption;
using System.Xml.Serialization;
using MRTD.Core.Notification;
using MRTD.NotificationService.Notification.BusinessComponent;

namespace MRTD.NotificationService.TaskMessageQueue
{
    public static class TaskNotificationQueue
    {
        public static void SendMessage(string queueName, MemberActivityModel model)
        {
            try
            {
                MessageQueueTransaction mqTran = new MessageQueueTransaction();
                MessageQueue notify = new MessageQueue();
                notify.Path = queueName;
                mqTran.Begin();
                notify.Formatter = new XmlMessageFormatter(new Type[] { typeof(MemberActivityModel) });
                notify.Send(model, mqTran);
                mqTran.Commit();
            }
            catch (Exception exception)
            {
                LoggerBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
            }
        }
        public static void ReceiveMessage(string queueName)
        {
            using (var messageQueue = new MessageQueue(queueName) { Formatter = new XmlMessageFormatter(new[] { typeof(MemberActivityModel) }) })
            using (var enumerator = messageQueue.GetMessageEnumerator2())
            {
                while (enumerator.MoveNext())
                {
                    try
                    {
                        MemberActivityModel message = (MemberActivityModel)enumerator.Current.Body;
                        if (message != null)
                        {
                            BusinessNotification.ProcessNotice(message, Session.AppSession["MailServer"].ToString(), 
                                                               Session.AppSession["Port"].ToString(), 
                                                               Session.AppSession["FromUsername"].ToString(),
                                                               TippAcademyEncryptionEngine.Decrypt(Session.AppSession["FromPassword"].ToString(),
                                                               Session.AppSession["ApplicationId"].ToString()),
                                                               Session.AppSession["FromEmailHead"].ToString());
                        }
                        enumerator.RemoveCurrent();
                        enumerator.Reset();
                    }
                    catch (Exception exception)
                    {
                        LoggerBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
                    }
                }
            }
        }
    }
}
