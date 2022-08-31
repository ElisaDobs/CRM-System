using MRTD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MRTD.Core.Common;
using MRTD.Core.Encryption;
using MRTD.DAL.MRTDDataAccess;
using System.Net;
using System.IO;
using System.Data;
using System.Reflection;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class NotificationBusinessComponent
    {
        public NotificationBusinessComponent(){}

        public bool InsertSystemNotification(int TemplateID, Guid MemberID, string ApplicationID, int AttachmentID = 0)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("TemplateID", TemplateID);
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("ApplicationID", Guid.Parse(ApplicationID));
                model.ApplicationParameter.Set("AttachmentID", AttachmentID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

         public bool InsertWebPushNotification(string PushType, string PushTitle, string PushText, int MemberId)
         {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("PushType", PushType);
                model.ApplicationParameter.Set("PushTitle", PushTitle);
                model.ApplicationParameter.Set("PushText", PushText);
                model.ApplicationParameter.Set("MemberId", MemberId);

                int Count = (int)CommonDataAccess.Process(model);
                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
         }

        public PushWebNotification GetWebPushNotificationByMemberId(int MemberId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberId", MemberId);
                List<PushWebNotification> pushwebnotice = (List<PushWebNotification>)CommonDataAccess.Process<PushWebNotification>(model);

                return pushwebnotice.FirstOrDefault();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}
