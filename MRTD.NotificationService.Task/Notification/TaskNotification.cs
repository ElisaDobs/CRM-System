using System;
using System.Collections.Generic;
using System.Text;
using MRTD.Core.Models;
using MRTD.Core.Common;
using MRTD.Core.Extensions;
using System.Timers;
using MRTD.NotificationService.Task.Notification.BusinessComponent;

namespace MRTD.NotificationService.Task.Notification
{
    public class TaskNotification
    {
        private TaskBusinessLogic logic = null;
        public TaskNotification()
        {
            logic = new TaskBusinessLogic("Notification Service Task");
            InitialiseSettings();
        }
        public void InitialiseSettings()
        {
            ApplicationSession appSession = new ApplicationSession();
            ApplicationSession appFunctionality = new ApplicationSession();
            appSession.Set("DatabaseConnectionString", Settings.ApplicationSettings.ConnectionString);
            appSession.Set("ACADEMIC_RECORD_LETTER", Settings.ApplicationSettings.ACADEMIC_RECORD_TEMPLETE);
            appSession.Set("MailServer", Settings.SmtpSettings.REG_Host);
            appSession.Set("Port", Settings.SmtpSettings.REG_Port);
            appSession.Set("FromUsername", Settings.SmtpSettings.REG_EMailAddress);
            appSession.Set("FromPassword", Settings.SmtpSettings.REG_Email_Password);
            appSession.Set("FromEmailHead", Settings.SmtpSettings.REG_EmailAddress_Name);
            Session.AppSession = appSession;
            var algorithms = logic?.GetFunctionalityByApplicationName();
            foreach(var algorithm in algorithms)
            {
                appFunctionality.Set(algorithm.ApplicationMethod, algorithm);
            }
            Session.AppFunctionality = appFunctionality;
        }

        public void RunTask()
        {
            try
            {
                var lst_student_to_be_notified = logic?.GetNextActitiesByDate();
                foreach (var student in lst_student_to_be_notified)
                {
                    
                }
            }
            catch(Exception exception)
            {

            }
            finally
            {
                
            }
        }
    }
}
