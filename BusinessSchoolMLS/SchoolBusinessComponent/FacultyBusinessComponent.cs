
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using MRTD.Core.Extensions;
using MRTD.Core.Models;
using MRTD.Core.Common;
using MRTD.DAL.MRTDDataAccess;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class FacultyBusinessComponent
    {
        public FacultyBusinessComponent() { }

        public List<GraduationMemberModel> GetAllMemberDueForGraduationByQualificationID(int QualificationID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("QualificationID", QualificationID);

                return (List<GraduationMemberModel>)CommonDataAccess.Process<GraduationMemberModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<QualTypeModel> GetAllQualificationTypes()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                
                return (List<QualTypeModel>)CommonDataAccess.Process<QualTypeModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<SchoolFacultyModel> GetAllBusinessFaculty(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", Guid.Parse(ClientId));

                return (List<SchoolFacultyModel>)CommonDataAccess.Process<SchoolFacultyModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<FacultyQualModel> GetAllFacultyQualificationByFacultyID(int FacultyID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("FacultyID", FacultyID);

                return (List<FacultyQualModel>)CommonDataAccess.Process<FacultyQualModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SaveFacultyQualification(QualificationModel qualificationModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = qualificationModel.MappingParameters();

                CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleModel> GetAllFacultyModuleByQualificationID(int QualificationID, int? Level = null)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("QualificationID", QualificationID);
                model.ApplicationParameter.Set("Level", Level);

                return (List<ModuleModel>)CommonDataAccess.Process<ModuleModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SaveFacultyQualModule(ModuleModel moduleModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleModel.MappingParameters();

                CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void AllocateModuleToQualification(int MemberID, int QualID, int ModuleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("QualID", QualID);
                model.ApplicationParameter.Set("ModuleID", ModuleID);

                CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<NQLevelModel> GetAllNQLevel()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                
                return (List<NQLevelModel>)CommonDataAccess.Process<NQLevelModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ProspectiveStudentModel> GetCurrentYearProspectiveStudentByFacultyID(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", ClientId);
                return (List<ProspectiveStudentModel>)CommonDataAccess.Process<ProspectiveStudentModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberEnrollStatusModel> GetStudentFlowStatusByMemberID(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);

                return (List<MemberEnrollStatusModel>)CommonDataAccess.Process<MemberEnrollStatusModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<EnrollmentStatusModel> GetEnrollmentStatusModelByAcademicYear(string ClientId, int AcademicYear)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", Guid.Parse(ClientId));
                model.ApplicationParameter.Set("AcademicYear", AcademicYear);

                return (List<EnrollmentStatusModel>)CommonDataAccess.Process<EnrollmentStatusModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleModel> GetModuleByMemberIdAndLevel(int MemberID, int ModuleLevel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("ModuleLevel", ModuleLevel);

                return (List<ModuleModel>)CommonDataAccess.Process<ModuleModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool UpdateQualificationByQualificationID(QualificationModel qualificationModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = qualificationModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleModel> GetAllStudyUnits(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", Guid.Parse(ClientId));
                
                return (List<ModuleModel>)CommonDataAccess.Process<ModuleModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<RequiredUploadedDocModel> GetAllUnitUploadType()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<RequiredUploadedDocModel>)CommonDataAccess.Process<RequiredUploadedDocModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool UpdateModuleByModuleID(ModuleModel moduleModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertUnitGroup(UnitGroupModel unitGroupModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = unitGroupModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<UnitGroup> GetAllUnitGroupByUnitID(int ModuleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleID", ModuleID);
                
                return (List<UnitGroup>)CommonDataAccess.Process<UnitGroup>(model); 
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool InsertUnitGroupAttendance(UnitGroupAttendanceModel unitGroupAttendanceModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = unitGroupAttendanceModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleMemberModel> GetAttendanceStudentsByGroupID(int GroupID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("GroupID", GroupID);

                return (List<ModuleMemberModel>)CommonDataAccess.Process<ModuleMemberModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<GroupAttendanceModule> GetGroupAttendantsByGroupID(int GroupID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("GroupID", GroupID);

                return (List<GroupAttendanceModule>)CommonDataAccess.Process<GroupAttendanceModule>(model); 
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public ModuleGroupModel GetModuleGroupByGroupID(int GroupID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("GroupID", GroupID);
                var lst_module_groups = (List<ModuleGroupModel>)CommonDataAccess.Process<ModuleGroupModel>(model);

                return lst_module_groups?.FirstOrDefault(); 
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<RegisteredUnitModel> GetRegisteredUnitByMemberID(string MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);

                return (List<RegisteredUnitModel>)CommonDataAccess.Process<RegisteredUnitModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<StudyMaterialModel> GetUnitLearningMaterialByUnitID(int UnitID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("UnitID", UnitID);

                return (List<StudyMaterialModel>)CommonDataAccess.Process<StudyMaterialModel>(model); //GetUnitLearningMaterialByUnitID Uni_GetUnitLearningMaterialByUnitID
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleModel> GetAllInstitutionModule(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", Guid.Parse(ClientId));
                
                return (List<ModuleModel>)CommonDataAccess.Process<ModuleModel>(model);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public bool RemoveStudentFromUnitGroup(int GroupID, int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("GroupID", GroupID);
                model.ApplicationParameter.Set("MemberID", MemberID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<StudentModel> GetProgramStudentByProgramId(int ProgramID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ProgramId", ProgramID);

                var students = (List<StudentModel>)CommonDataAccess.Process<StudentModel>(model);

                return students;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}
