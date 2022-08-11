using MRTD.Core.Common;
using MRTD.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MRTD.Core.Encryption;

namespace MRTD.DAL.MRTDDataAccess
{
    public class CommonDataAccess
    {
        public static object Process(ApplicationFunctionalityModel functionalityModel)
        {
            try
            {
                using (DataLayer datalayer = new DataLayer(
                                             TippAcademyEncryptionEngine.Decrypt(Session.AppSession["DatabaseConnectionString"].ToString(),
                                             Session.AppSession["ApplicationId"].ToString())))
                {
                    object result = null;
                    datalayer.Open();
                    switch (functionalityModel.ReturnType)
                    {
                        case DataReturnType.Scalar:
                            result = datalayer.Get(functionalityModel.CommandType, functionalityModel.ApplicationAlgorithm, Data.SetParameters(functionalityModel.ApplicationParameter));
                            break;
                        case DataReturnType.NonQuery:
                            result = datalayer.Execute(functionalityModel.CommandType, functionalityModel.ApplicationAlgorithm, Data.SetParameters(functionalityModel.ApplicationParameter));
                            break;
                        case DataReturnType.BulkInsert:
                            result = datalayer.BulkUpload(functionalityModel.ApplicationAlgorithm, Data.SetParameter(functionalityModel.ApplicationParameter));
                            break;
                    }
                    return result;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static object Process<T>(ApplicationFunctionalityModel functionalityModel)
        {
            try
            {
                using (DataLayer datalayer = new DataLayer(
                                             TippAcademyEncryptionEngine.Decrypt(Session.AppSession["DatabaseConnectionString"].ToString(),
                                             Session.AppSession["ApplicationId"].ToString())))
                {
                    object result = null;
                    datalayer.Open();
                    result = datalayer.GetData<T>(functionalityModel.CommandType, functionalityModel.ApplicationAlgorithm, Data.SetParameters(functionalityModel.ApplicationParameter));
                    return result;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
