
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MRTD.Core.Common;
using MRTD.Core.Extensions;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;


namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class OnlineModuleActivityBusinessComponent
    {
        public OnlineModuleActivityBusinessComponent() { }

        public bool InsertOnlineModuleActivityQuestion(ModuleActivityQuestionModel moduleActivityQuestionModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleActivityQuestionModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool InsertOnlineActivity(ModuleActivityModel moduleActivityModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleActivityModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleActivityQuestionModel> GetModuleActivityQuestionByModuleActivityID(int ModuleActivityID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleActivityID", ModuleActivityID);
                
                return (List<ModuleActivityQuestionModel>)CommonDataAccess.Process<ModuleActivityQuestionModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertOnlineModuleActivityAnswer(ModuleActivityAnswerModel moduleActivityAnswerModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleActivityAnswerModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleActivityAnswerModel> GetOnlineModuleAnswersByQuestionID(int MemberID, int QuestionID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("QuestionID", QuestionID);

                return (List<ModuleActivityAnswerModel>)CommonDataAccess.Process<ModuleActivityAnswerModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertQuestionAlphanumericAnswer(AlphanumericAnswerModel alphanumericAnswerModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = alphanumericAnswerModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertQuestionLookupAnswer(LookupAnswerModel lookupAnswerModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = lookupAnswerModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public ModuleActivityQuestionModel GetModuleQuestionByQuestionID(int QuestionID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("QuestionID", QuestionID);
                var lst_module_question = (List<ModuleActivityQuestionModel>)CommonDataAccess.Process<ModuleActivityQuestionModel>(model);

                return lst_module_question?.FirstOrDefault(); 
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertOnlineActivityTotalMark(int MemberID, int OnlineModuleActivityID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("OnlineModuleActivityID", OnlineModuleActivityID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<AcademicRecordModel> GetAcademicRecordByMemberID(int QualificationID, int MemberID, int ActivityID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("QualificationID", QualificationID);
                model.ApplicationParameter.Set("ActivityID", ActivityID);

                return (List<AcademicRecordModel>)CommonDataAccess.Process<AcademicRecordModel>(model); 
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<QualificationAcademicRecord> GetAllQualificationAcademicRecordByMemberID(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);

                return (List<QualificationAcademicRecord>)CommonDataAccess.Process<QualificationAcademicRecord>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public StudentModel GetStudentDetailByMemberID(int MemberID, int QualificationID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("QualificationID", QualificationID);
                var lst_detail = (List<StudentModel>)CommonDataAccess.Process<StudentModel>(model);

                return lst_detail?.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ControlFieldModel> GetAllControlField()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                return (List<ControlFieldModel>)CommonDataAccess.Process<ControlFieldModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ActivityTimeTableModel> GetActivityTimeTableByMemberID(int MemberID, int ActivityID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("ActivityID", ActivityID);
                return (List<ActivityTimeTableModel>)CommonDataAccess.Process<ActivityTimeTableModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public object GetStudentGuidByStudentNumber(string StudentNo)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("StudentNo", StudentNo);
                return CommonDataAccess.Process(model); 

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertQuestionLookupAnswer(QuestionLookupAnswerModel questionLookupAnswerModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = questionLookupAnswerModel.MappingParameters();
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
