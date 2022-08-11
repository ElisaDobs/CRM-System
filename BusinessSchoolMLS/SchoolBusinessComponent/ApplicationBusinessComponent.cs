
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MRTD.Core.Extensions;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class ApplicationBusinessComponent
    {
        public ApplicationBusinessComponent() { } 

        public string GetUploadFilePathByUploadID(int UploadID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession(); 
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("UploadID", UploadID);
                var result = CommonDataAccess.Process(model);

                return result?.ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public ResultObjectModel SaveApplication(ApplicationMemberModel applicationMemberModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = applicationMemberModel.MappingParameters();
                var result = (List<ResultObjectModel>)CommonDataAccess.Process<ResultObjectModel>(model);

                return result?.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public MemberProfileModel GetStaffMemberByIDNo(string IDNo)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("IDNo", IDNo);
                var staffModel = (List<MemberProfileModel>)CommonDataAccess.Process<MemberProfileModel>(model);

                return staffModel?.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberProfileModel> GetAllStaffMemberByClientId(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", Guid.Parse(ClientId));
                var members = (List<MemberProfileModel>)CommonDataAccess.Process<MemberProfileModel>(model);

                return members;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public bool UpdateStaffMemberByIDNo(string IDNo, string FirstName, string LastName, string CellNo, string EmailAddress, int RoleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("IDNo", IDNo);
                model.ApplicationParameter.Set("FirstName", FirstName);
                model.ApplicationParameter.Set("LastName", LastName);
                model.ApplicationParameter.Set("CellNo", CellNo);
                model.ApplicationParameter.Set("EmailAddress", EmailAddress);
                model.ApplicationParameter.Set("RoleID", RoleID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<GenderModel> GetAllGender()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<GenderModel>)CommonDataAccess.Process<GenderModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<PrerequisiteModel> GetAllPrereqByQualificationID(int QualificationID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("QualificationID", QualificationID);

                return (List<PrerequisiteModel>)CommonDataAccess.Process<PrerequisiteModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertApplicationUpload(ApplicantRequiredDocument requiredDocumentModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = requiredDocumentModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);
                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<FinancialAccountModel> GetYearFinancialStatementByMemberID(int MemberID, int AcademicYear)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("AcademicYear", AcademicYear);

                return (List<FinancialAccountModel>)CommonDataAccess.Process<FinancialAccountModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<RequiredUploadedDocModel> GetUploadedDocumentByMemberID(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);

                return (List<RequiredUploadedDocModel>)CommonDataAccess.Process<RequiredUploadedDocModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public object GetRequiredDocumentByUploadID(int UploadID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("UploadID", UploadID);

                return CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public object UpdateEnrollmentStatusByMemberID(DocumentVerifyModel documentVerifyModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = documentVerifyModel.MappingParameters();

                return CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<TitleModel> GetAllTitles()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<TitleModel>)CommonDataAccess.Process<TitleModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<LanguageModel> GetLanguages()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                
                return (List<LanguageModel>)CommonDataAccess.Process<LanguageModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<RaceModel> GetAllRaces()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<RaceModel>)CommonDataAccess.Process<RaceModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<NationalityModel> GetAllNationalities()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<NationalityModel>)CommonDataAccess.Process<NationalityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<HighestQualModel> GetHighestQualModels()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<HighestQualModel>)CommonDataAccess.Process<HighestQualModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<AssessorModel> GetModuleActivityAssessor()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<AssessorModel>)CommonDataAccess.Process<AssessorModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int GetEnrollmentStatusByMemberID(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);

                return (int)CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
