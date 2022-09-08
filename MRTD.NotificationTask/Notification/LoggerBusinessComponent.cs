using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.NotificationTask.Notification
{
    public static class LoggerBusinessComponent
    {
        public static bool InsertLogMessage(string AppId, MessageNode Node, string Message = null)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.CommandType = CommandType.StoredProcedure;
                model.ReturnType = DataReturnType.NonQuery;
                model.ApplicationParameter.Set("MemberID", AppId);
                model.ApplicationParameter.Set("MessageNode", (Int32)Node);
                model.ApplicationParameter.Set("LogMessage", Message);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
