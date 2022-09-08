using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Configuration;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.NotificationService.Notification.BusinessComponent;
using MRTD.NotificationService.TaskMessageQueue;
using MRTD.Core.Encryption;
using System.IO;

namespace MRTD.NotificationService
{
    public partial class NotificationService : ServiceBase
    {
        //private System.Timers.Timer tmrTaskNotification;
        private System.Timers.Timer tmrTaskNotificationFromTippConnect;
        private TaskBusinessLogic taskBusinessLogic = new TaskBusinessLogic(ConfigurationManager.AppSettings["ApplicationId"].ToString());
        public NotificationService()
        {
            InitializeComponent();
            //tmrTaskNotification = new System.Timers.Timer(5000);
            tmrTaskNotificationFromTippConnect = new System.Timers.Timer(1000);
            InitialiseApplication();
        }

        private void LogFile(string message)
        {
            using (StreamWriter stream = new StreamWriter(new FileStream(@"C:\Notification\Error.txt", FileMode.Create)))
            {
                stream.WriteLine(message);
            }
        }

        private void InitialiseApplication()
        {
            try
            {
                ApplicationSession appSession = new ApplicationSession();
                appSession.Set("DatabaseConnectionString", ConfigurationManager.AppSettings["ConnectionString"].ToString());
                appSession.Set("ACADEMIC_RECORD_LETTER", ConfigurationManager.AppSettings["RequiredDocumentLetter"].ToString());
                appSession.Set("MailServer", ConfigurationManager.AppSettings["SMTPServer"].ToString());
                appSession.Set("Port", ConfigurationManager.AppSettings["SMTPServerPort"].ToString());
                appSession.Set("FromUsername", ConfigurationManager.AppSettings["FromEmail"].ToString());
                appSession.Set("FromPassword", ConfigurationManager.AppSettings["FromPassword"].ToString());
                appSession.Set("FromEmailHead", ConfigurationManager.AppSettings["FromEmailName"].ToString());
                appSession.Set("ApplicationId", ConfigurationManager.AppSettings["ApplicationId"].ToString());
                appSession.Set("rungraduationtime", ConfigurationManager.AppSettings["rungraduationtime"]);
                Session.AppSession = appSession;
                Session.AppFunctionality = new ApplicationSession();
                var algorithms = taskBusinessLogic.GetFunctionalityByApplicationId();
                algorithms.ForEach(delegate(ApplicationFunctionalityModel functionalityModel)
                {
                    Session.AppFunctionality.Set(functionalityModel.ApplicationMethod, functionalityModel);
                });
            }
            catch(Exception exception)
            {
                LogFile(exception.ToString());
                LoggerBusinessComponent.InsertLogMessage(ConfigurationManager.AppSettings["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
            }
            
        }
        /*private void tmrTaskNotification_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var lstMessageQueue = taskBusinessLogic.GetAllMessageQueue();
                lstMessageQueue?.ForEach(delegate (TippMessageQueueModel queueModel) {
                    TaskNotificationQueue.ReceiveMessage(ConfigurationManager.AppSettings["tipp_server_msq"] + @"\" + queueModel.MSQPath);
                });
            }
            catch(Exception exception)
            {
                LoggerBusinessComponent.InsertLogMessage(ConfigurationManager.AppSettings["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
            }
        }
        */
        protected void tmrTaskNotificationFromTippConnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var notification = taskBusinessLogic.GetAllUnSentNotifications();
                notification?.ForEach(delegate (NotificationModel notificationModel) 
                {
                    MemberActivityModel memberActivityModel = new MemberActivityModel()
                    {
                        EmailAddress = notificationModel.EmailAddress,
                        EmailBody = notificationModel.MessageBody.Replace("#PASSWORD#", 
                                    TippAcademyEncryptionEngine.Decrypt(
                                                                          notificationModel.Password, 
                                                                          notificationModel.ApplicationId.ToString().ToUpper())
                                                                       ),
                        EmailSubject = notificationModel.MailSubject
                    };
                    if (notificationModel.AttachmentID != 0)// If there's an attachment.
                    {
                        byte[] attachmentDocument = taskBusinessLogic.GetRequiredDocumentByUploadID(notificationModel.AttachmentID);
                        memberActivityModel.EmailAttachment = Convert.ToBase64String(attachmentDocument);
                    }
                    TaskNotificationQueue.SendMessageFromQueue(memberActivityModel);
                });
            }
            catch(Exception exception)
            {
                LoggerBusinessComponent.InsertLogMessage(ConfigurationManager.AppSettings["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
            }
        }
        protected override void OnStart(string[] args)
        {
            //tmrTaskNotification.Enabled = false;
            tmrTaskNotificationFromTippConnect.Enabled = false;
            try
            {
                //tmrTaskNotification.Elapsed += tmrTaskNotification_Elapsed;
                //tmrTaskNotification.Start();
                tmrTaskNotificationFromTippConnect.Elapsed += tmrTaskNotificationFromTippConnect_Elapsed;
                tmrTaskNotificationFromTippConnect.Start();
            }
            catch(Exception exception)
            {
                LoggerBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
            }
            finally
            {
                //tmrTaskNotification.Enabled = true;
                tmrTaskNotificationFromTippConnect.Enabled = true;
            }
        }
        protected override void OnStop()
        {
            LoggerBusinessComponent.InsertLogMessage(ConfigurationManager.AppSettings["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_END);
            try
            {
                //tmrTaskNotification.Enabled = false;
                //tmrTaskNotification.Stop();
                tmrTaskNotificationFromTippConnect.Enabled = false;
                tmrTaskNotificationFromTippConnect.Stop();
            }
            catch(Exception exception)
            {
                LoggerBusinessComponent.InsertLogMessage(ConfigurationManager.AppSettings["ApplicationId"].ToString(), MessageNode.SYS_MRTD_NOTIFICATION_ERROR, exception.ToString());
            }
        }
    }
}
