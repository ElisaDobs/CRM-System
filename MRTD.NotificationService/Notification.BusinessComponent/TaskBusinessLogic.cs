using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MRTD.NotificationService.Notification.BusinessComponent
{
    public class TaskBusinessLogic
    {
        private string applicationId;
        public TaskBusinessLogic(string applicationId)
        {
            this.applicationId = applicationId;
        }

        public List<ApplicationFunctionalityModel> GetFunctionalityByApplicationId()
        {
            try
            {
                ApplicationFunctionalityModel model = new ApplicationFunctionalityModel();
                model.ApplicationMethod = MethodBase.GetCurrentMethod().Name;
                model.ApplicationAlgorithm = MethodBase.GetCurrentMethod().Name;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.ApplicationParameter.Set("ApplicationId", applicationId);
                var lstApplication = (List<ApplicationFunctionalityModel>)CommonDataAccess.Process<ApplicationFunctionalityModel>(model);

                return lstApplication;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberActivityModel> GetNextActitiesByDate()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.Fill;
                return (List<MemberActivityModel>)CommonDataAccess.Process<MemberActivityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberActivityModel> GraduateLearnerQualification()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.Fill;

                return (List<MemberActivityModel>)CommonDataAccess.Process<MemberActivityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberActivityModel> GetNotSentMessageByChatId(int ChatId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.Fill;
                model.ApplicationParameter.Set("ChatID", ChatId);
                return (List<MemberActivityModel>)CommonDataAccess.Process<MemberActivityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public byte[] GetRequiredDocumentByUploadID(int UploadID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.Scalar;
                model.ApplicationParameter.Set("UploadID", UploadID);
                byte[] document = (byte[])CommonDataAccess.Process(model);
                return document;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<NotificationModel> GetAllUnSentNotifications()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.Fill;
                return (List<NotificationModel>)CommonDataAccess.Process<NotificationModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool ProcessGraduateCompletedAllQualificationUnit()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.NonQuery;
                int Count = (int)CommonDataAccess.Process(model);
                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<TippMessageQueueModel> GetAllMessageQueue()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.Fill;
                return (List<TippMessageQueueModel>)CommonDataAccess.Process<TippMessageQueueModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
