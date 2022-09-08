using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace MRTD.Core.Common
{
    public class Settings
    {
        public class AppSettings
        {
            public string ConnectionString { get; set; }

            public string AcademicRecordTemplate { get; set; }

            public string FromEmail { get; set; }

            public string FromEmailName { get; set; }

            public string SMTPServer { get; set; }

            public string SMTPServerPort { get; set; }

            public string FromUsername { get; set; }

            public string FromPassword { get; set; }

            public string RequiredDocumentLetter { get; set; }

            public string NotificationTemplate { get; set; }

            public string ApplicationId { get; set; }

            public string QueueName { get; set; }

            public string Uploads { get; set; }

            public string ClientId { get; set; }

            public string AjaxRedirect { get; set; }
        }
        public static class ConfigurationSettings
        {

            public static string GetConfig(string propertyName)
            {
                string appSettings = string.Empty;

                string fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"\\MRTD.NotificationService\\appsettings.json");

                if (File.Exists(fileDirectory))
                {
                    appSettings = File.ReadAllText(fileDirectory);
                }
                else
                {
                    fileDirectory = Environment.CurrentDirectory;
                    //fileDirectory = Directory.GetParent(fileDirectory).Parent.Parent.FullName;
                    appSettings = File.ReadAllText(Path.Combine(fileDirectory, "appsettings.json"));
                }

                var appSetting = JsonConvert.DeserializeObject<AppSettings>(appSettings);

                var properties = appSetting.GetType().GetProperty(propertyName);

                if (properties != null)
                {
                    return properties.GetValue(appSetting).ToString();
                }

                return string.Empty;
            }
        }

        public static class SmtpSettings
        {
            public static string REG_EMailAddress = ConfigurationSettings.GetConfig("FromEmail");

            public static string REG_EmailAddress_Name = ConfigurationSettings.GetConfig("FromEmailName");

            public static string REG_Email_Password = ConfigurationSettings.GetConfig("FromPassword");

            public static string REG_Host = ConfigurationSettings.GetConfig("SMTPServer");

            public static int REG_Port = Int32.Parse(ConfigurationSettings.GetConfig("SMTPServerPort"));

            public static string REG_Required_Documents_letter = ConfigurationSettings.GetConfig("RequiredDocumentLetter");

            public static string REG_Template_Notification_Body = ConfigurationSettings.GetConfig("NotificationTemplate");
        }

        public static class ApplicationSettings
        {
            public static string ConnectionString = ConfigurationSettings.GetConfig("ConnectionString");

            public static string ApplicationId = ConfigurationSettings.GetConfig("ApplicationId");

            public static string QueueName = ConfigurationSettings.GetConfig("QueueName");

            public static string ACADEMIC_RECORD_TEMPLETE = Path.Combine(
                                                                            Directory.GetCurrentDirectory(),
                                                                            ConfigurationSettings.GetConfig("AcademicRecordTemplate")
                                                                        );
        }
    }

    public enum MessageNode
    {
        SYS_MODULE_ACTIVITY_LOAD_START = 1,
        SYS_MODULE_ACTIVITY_LOAD_END = 2,
        SYS_MODULE_ACTIVITY_LOAD_ERROR = 3,

        SYS_MODULE_ACTIVITY_CREATE_START = 4,
        SYS_MODULE_ACTIVITY_CREATE_END = 5,
        SYS_MODULE_ACTIVITY_CREATE_ERROR = 6,

        SYS_FACULTY_ENROLLMENT_STATUS_COUNT_START = 7,
        SYS_FACULTY_ENROLLMENT_STATUS_COUNT_END = 8,
        SYS_FACULTY_ENROLLMENT_STATUS_COUNT_ERROR = 9,

        SYS_FACULTY_GET_REGISTERED_UNIT_BY_MEMBER_ID_START = 10,
        SYS_FACULTY_GET_REGISTERED_UNIT_BY_MEMBER_ID_END = 11,
        SYS_FACULTY_GET_REGISTERED_UNIT_BY_MEMBER_ID_ERROR = 12,

        SYS_ACADEMIC_RECORD_DOWN_LOAD_START = 13,
        SYS_ACADEMIC_RECORD_DOWN_LOAD_END = 14,
        SYS_ACADEMIC_RECORD_DOWN_LOAD_ERROR = 15,

        SYS_NOTIFICATION_E_MAIL = 16,

        SYS_SAVE_STAFF_MEMBER_START = 17,
        SYS_SAVE_STAFF_MEMBER_END = 18,
        SYS_SAVE_STAFF_MEMBER_ERROR = 19,

        SYS_GET_ALL_INSTITUTION_MODULE_START = 20,
        SYS_GET_ALL_INSTITUTION_MODULE_END = 21,
        SYS_GET_ALL_INSTITUTION_MODULE_ERROR = 22,

        SYS_APPLICATION_START = 23,
        SYS_APPLICATION_END = 24,
        SYS_APPLICATION_ERROR = 25,

        SYS_MRTD_NOTIFICATION_START = 26,
        SYS_MRTD_NOTIFICATION_END = 27,
        SYS_MRTD_NOTIFICATION_ERROR = 28,

        SYS_MODULE_CHAT_POST_ERROR = 29,

        SYS_LOGIN_ERROR = 30,
        SYS_RESET_PASSWORD_ERROR = 31,
        SYS_FORGOT_PASSWORD_ERROR = 32,
        SYS_LOGOUT_ERROR = 33,

        SYS_GET_POST_MESSAGE_ERROR = 34,
        SYS_GRADUATION_TASK_ERROR = 35,

        SYS_MODULE_ACTIVITY_REPORT_ERROR = 36,
        SYS_STUDENT_ACADEMIC_RECORD_ERROR = 37,
        SYS_SEARCH_STUDENT_ERROR = 38,

        SYS_APPLICATION_VERIFY_ERROR = 39,
        SYS_APPLICATION_APPROVE_ENROLLMENT_ERROR = 40,

        SYS_REQUIRED_DOCUMENT_ERROR = 41,
        SYS_UPLOAD_PAYMENT_ERROR = 42,
        SIGNED_LETTER_CODE_ERROR = 43,

        SYS_MODULE_ACTIVITY_UPDATE_ERROR = 44,

        SYS_PROCESS_NOTIFICATION_ERROR = 45
    }
    public enum DataReturnType
    {
        Fill = 1,
        Scalar = 2,
        NonQuery = 3,
        BulkInsert = 4
    }
    public static class Session
    {
        public static ApplicationSession UserSession;

        public static ApplicationSession AppSession;

        public static ApplicationSession AppFunctionality;
    }

    public static class Data
    {
        public static List<SqlParameter> SetParameters(ApplicationSession args)
        {
            var parameters = new List<SqlParameter>();

            foreach (var key in args)
            {
                parameters.Add(new SqlParameter(string.Format("@{0}", key.Key), key.Value));
            }

            return parameters;
        }

        public static DataTable SetParameter(ApplicationSession args)
        {
            var datatable = default(DataTable);
            foreach(var key in args)
            {
                datatable = (DataTable)key.Value;
            }

            return datatable;
        }
    }

    public class ApplicationSession : Dictionary<string, object>
    {
        public ApplicationSession() : base()
        {
        }
        public void Set(string key, object value)
        {
            Add(key, value);
        }

        public object Get(string key)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }

            return null;
        }
    }
}

