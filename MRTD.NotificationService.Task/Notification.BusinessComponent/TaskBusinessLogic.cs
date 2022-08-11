using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace MRTD.NotificationService.Task.Notification.BusinessComponent
{
    public class TaskBusinessLogic
    {
        private string applicationName;
        public TaskBusinessLogic(string applicationName)
        { 
            this.applicationName = applicationName;
        }

        public List<ApplicationFunctionalityModel> GetFunctionalityByApplicationName()
        {
            try
            {
                ApplicationFunctionalityModel model = new ApplicationFunctionalityModel();
                model.ApplicationMethod = MethodBase.GetCurrentMethod().Name;
                model.ApplicationAlgorithm = MethodBase.GetCurrentMethod().Name;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.DATASET;
                model.ApplicationParameter.Add("ApplicationName", applicationName);

                return (List<ApplicationFunctionalityModel>)CommonDataAccess.Process<ApplicationFunctionalityModel>(model);
            }
            catch(Exception exception)
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
                model.ReturnType = DataReturnType.DATASET;

                return (List<MemberActivityModel>)CommonDataAccess.Process<MemberActivityModel>(model);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}
