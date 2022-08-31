using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.Core.Extensions;
using MRTD.DAL.MRTDDataAccess;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class LoginBusinessComponent
    {
        public LoginBusinessComponent() {}

        public int GetMemberIDByMemberGuid(string MemberGuid)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemGuid", MemberGuid);

                return (int)CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public LoginModel ValidLogin(LoginModel loginModel)
        {
           try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = loginModel.MappingParameters();
                var loginResult = (List<LoginModel>)CommonDataAccess.Process<LoginModel>(model);
                return loginResult?.FirstOrDefault();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = forgotPasswordModel.MappingParameters();

                return (string)CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public AppliedMemberModel GetLoginDetailByMemberGuid(string MemberGuid)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemGuid", MemberGuid);

                var lst_applied = (List<AppliedMemberModel>)CommonDataAccess.Process<AppliedMemberModel>(model);

                return lst_applied?.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string GetMemberGuidByMemberID(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                var member_guid = CommonDataAccess.Process(model);

                return member_guid.ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool UpdateLogout(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool IsStudentCheckByMemberGuid(string UserGuid)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("UserGuid", UserGuid);

                return (bool)CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = resetPasswordModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public MemberProfileModel? GetMemberProfileByMemberId(string MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberId", Guid.Parse(MemberID));
                var lst_profile = (List<MemberProfileModel>)CommonDataAccess.Process<MemberProfileModel>(model);
                if (lst_profile != null)
                {
                    return lst_profile.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
