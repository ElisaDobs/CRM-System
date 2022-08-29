using System;
using System.Collections.Generic;
using MRTD.Core.Extensions;
using MRTD.Core.Models;
using MRTD.Core.Common;
using MRTD.DAL.MRTDDataAccess;
using System.Reflection;
using System.Data;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class ModuleActivityBusinessComponent
    {
        public ModuleActivityBusinessComponent()
        { }

        public List<SchoolNewsFeedModel> GetAllNewsFeedByClientID(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", ClientId);
                
                return (List<SchoolNewsFeedModel>)CommonDataAccess.Process<SchoolNewsFeedModel>(model); ;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool InsertNewsFeed(NewFeedModel newsFeed)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = newsFeed.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<StudentActivityMarkModel> GetAllMemberUnitMarkByActivityDate(string UnitCode, string ActivityName, string ActivityDate)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("UnitCode", UnitCode);
                model.ApplicationParameter.Set("ActivityName", ActivityName);
                model.ApplicationParameter.Set("ActivityDate", ActivityDate);

                return (List<StudentActivityMarkModel>)CommonDataAccess.Process<StudentActivityMarkModel>(model);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public bool BulkUploadMark(string destinationName, DataTable data)
        {
            try
            {
                ApplicationFunctionalityModel model = new ApplicationFunctionalityModel();
                model.ReturnType = DataReturnType.BulkInsert;
                model.ApplicationAlgorithm = destinationName;
                model.ApplicationParameter = new ApplicationSession();
                model.ApplicationParameter.Set("data", data);
                bool IsUploaded = (bool)CommonDataAccess.Process(model);

                return IsUploaded;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public List<ActivityModel> GetAllActivity()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<ActivityModel>)CommonDataAccess.Process<ActivityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleActivityModel> GetAllModuleActivitiesByModuleID(int ModuleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleID", ModuleID);

                return (List<ModuleActivityModel>)CommonDataAccess.Process<ModuleActivityModel>(model); 
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool SaveModuleActivity(ModuleActivityModel moduleActivityModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
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

        public bool UpdateModuleActivity(
                                            int ModuleActivityID, 
                                            int ModuleID, 
                                            int ActivityID, 
                                            DateTime ActivityDate, 
                                            string ActivityTime, 
                                            int ActivityDuration, 
                                            int ActivityPassMark, 
                                            int ActivityTotalMark, 
                                            string MemberID, 
                                            int AssessorID
                                      )
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = new ApplicationSession();
                model.ApplicationParameter.Set("ModuleActivityID", ModuleActivityID);
                model.ApplicationParameter.Set("ModuleID", ModuleID);
                model.ApplicationParameter.Set("ActivityID", ActivityID);
                model.ApplicationParameter.Set("ActivityDate", ActivityDate);
                model.ApplicationParameter.Set("ActivityTime", ActivityTime);
                model.ApplicationParameter.Set("ActivityDuration", ActivityDuration);
                model.ApplicationParameter.Set("ActivityPassMark", ActivityPassMark);
                model.ApplicationParameter.Set("ActivityTotalMark", ActivityTotalMark);
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("AssessorID", AssessorID);

                int Count = (int)CommonDataAccess.Process(model);

                return Count > 1;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }


        public bool SaveModuleActivityUpload(ModuleActivityUploadModel moduleActivityUploadModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleActivityUploadModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool ProcessMarkImport(Guid ImportID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ImportID", ImportID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool SaveModuleActivityMark(ModuleActivityMarkModel moduleActivityMarkModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = moduleActivityMarkModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberModuleActivityModel> GetAllMemberByModuleActivityID(int ModuleID, int ModuleActivityID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleID", ModuleID);
                model.ApplicationParameter.Set("ModuleActivityID", ModuleActivityID);

                return (List<MemberModuleActivityModel>)CommonDataAccess.Process<MemberModuleActivityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool SaveMessageChat(MessageChatModel messageChatModel)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = messageChatModel.MappingParameters();
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleMemberModel> GetModuleMembersByModuleID(int ModuleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleID", ModuleID);

                return (List<ModuleMemberModel>)CommonDataAccess.Process(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ChatModel> GetAllMessageChatByModuleID(int ModuleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleID", ModuleID);

                return (List<ChatModel>)CommonDataAccess.Process<ChatModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleMemberModel> GetRegisteredByGroupID(int GroupID)
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

        public bool CreateModuleChat(string ChatName, int ModuleID, int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ChatName", ChatName);
                model.ApplicationParameter.Set("ModuleID", ModuleID);
                model.ApplicationParameter.Set("MemberID", MemberID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        
        public bool RemoveParticipantsFromChat(int ChatID, int ParticipantsID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ChatID", ChatID);
                model.ApplicationParameter.Set("ParticipantsID", ParticipantsID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<ModuleChatModel> GetChatsByMemberId(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                var lst_Chats = (List<ModuleChatModel>)CommonDataAccess.Process<ModuleChatModel>(model);

                return lst_Chats;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<MessagePostChatModel> GetMessageChatsByChatID(int ChatID, int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ChatID", ChatID);
                model.ApplicationParameter.Set("MemberID", MemberID);
                var lst_Chats = (List<MessagePostChatModel>)CommonDataAccess.Process<MessagePostChatModel>(model);

                return lst_Chats;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public bool GetNewChatsByChatID(int ChatID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Scalar;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ChatID", ChatID);
                var IsNewChats = (bool)CommonDataAccess.Process(model);

                return IsNewChats;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ActiveModuleChatModel> GetAllStudyUnitActive(string ClientId)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ClientId", Guid.Parse(ClientId));
                var lst_Chats = (List<ActiveModuleChatModel>)CommonDataAccess.Process<ActiveModuleChatModel>(model);

                return lst_Chats;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public bool DeactivateChat(int ChatID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ChatID", ChatID);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<MemberModuleChatModel> GetAllMemberPerChatID(int ChatID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ChatID", ChatID);

                return (List<MemberModuleChatModel>)CommonDataAccess.Process<MemberModuleChatModel>(model);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public bool InsertUnitMessagePost(int MemberID, int ChatID, string PostText)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.NonQuery;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("ChatID", ChatID);
                model.ApplicationParameter.Set("PostText", PostText);
                int Count = (int)CommonDataAccess.Process(model);

                return Count > 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MessagePostModel> GetMessagePostByModuleID(int ModuleID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ModuleID", ModuleID);

                return (List<MessagePostModel>)CommonDataAccess.Process<MessagePostModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<NotifyActivityModel> GetNotifyActivityByDateTime()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;

                return (List<NotifyActivityModel>)CommonDataAccess.Process<NotifyActivityModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<UnitProgramScheduleModel> GetModuleActivityResultsByMember(int MemberID, int AcademicYear)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("AcademicYear", AcademicYear);
                var unit_schedule = (List<ModuleActivityResultModel>)CommonDataAccess.Process<ModuleActivityResultModel>(model);

                return unit_schedule?.ChangeTo<UnitProgramScheduleModel>();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<EventScheduleModel> GetStudentScheduleByMemberID(int MemberID, int Month)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                model.ApplicationParameter.Set("Month", Month);

                return (List<EventScheduleModel>)CommonDataAccess.Process<EventScheduleModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
