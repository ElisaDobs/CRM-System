using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class LogMessageBusinessComponent
    {
        public static bool InsertLogMessage(string mem_guid, MessageNode messageNode, string logMessage = null)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", mem_guid);
                model.ApplicationParameter.Set("MessageNode", (Int32)messageNode);
                model.ApplicationParameter.Set("LogMessage", logMessage);
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
