using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;

namespace MRTD.NotificationService.Notification.BusinessComponent
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
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}
