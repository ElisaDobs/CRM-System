using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class AppBusinessLogic
    {
        private string ApplicationId;
        public AppBusinessLogic(string ApplicationId)
        {
            this.ApplicationId = ApplicationId;
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
                model.ApplicationParameter.Add("ApplicationId", ApplicationId);

                var list_functionalities = (List<ApplicationFunctionalityModel>)CommonDataAccess.Process<ApplicationFunctionalityModel>(model);

                return list_functionalities;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
