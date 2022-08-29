using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using MRTD.Core.Models;
using MRTD.Core.Common;
using BusinessSchoolMLS.SchoolBusinessComponent;
using MRTD.Core.Upload;
using MRTD.DAL.Excel;
using System.Data;
using MRTD.Core.Extensions;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class ModuleActivityController : Controller
    {
        private readonly ModuleActivityBusinessComponent _moduleActivityBusinessComponent;
        private readonly LoginBusinessComponent _loginBusinessComponent;
        private readonly NotificationBusinessComponent _notificationBusinessComponent;
        public ModuleActivityController()
        {
            _moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
            _notificationBusinessComponent = new NotificationBusinessComponent();
            _loginBusinessComponent = new LoginBusinessComponent();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Activities(ModuleActivityModel moduleActivityModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["mid"].ToString()))
                {
                    if (!string.IsNullOrEmpty(HttpContext.Request.Query["qid"]))
                    {
                        ViewBag.QualID = Int32.Parse(HttpContext.Request.Query["qid"]);
                        ViewBag.FacultyID = Int32.Parse(HttpContext.Request.Query["fid"]);
                    }
                    else
                    {
                        if (Session.UserSession.ContainsKey(HttpContext.Request.Query["mid"].ToString()))
                        {
                            var user_session = (FacultyQualificationModel)(Session.UserSession[HttpContext.Request.Query["mid"].ToString()]);
                            ViewBag.QualID = user_session.QualificationID;
                            ViewBag.FacultyID = user_session.FacultyID;
                            Session.UserSession.Remove(HttpContext.Request.Query["mid"].ToString());
                        }
                    }
                    ViewBag.ModuleID = Int32.Parse(HttpContext.Request.Query["modid"]);
                    ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                    FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                    ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                    var lst_all_activities = moduleActivityBusinessComponent.GetAllActivity().Select(act => new SelectListItem() { Value = act.ActivityID.ToString(), Text = act.ActivityName });
                    moduleActivityModel.ActivitList = new SelectList(lst_all_activities, "Value", "Text");
                    var lst_all_modules = facultyBusinessComponent.GetAllFacultyModuleByQualificationID((int)ViewBag.QualID)
                                                                  .Select(mod => new SelectListItem { Value = mod.ModuleID.ToString(), Text = mod.ModuleName });
                    moduleActivityModel.ModuleList = new SelectList(lst_all_modules, "Value", "Text");
                    var lst_all_assesors = applicationBusinessComponent.GetModuleActivityAssessor().Select(mem => new SelectListItem() { Value = mem.AssessorID.ToString(), Text = mem.FullName });
                    moduleActivityModel.ActivityAssessors = new SelectList(lst_all_assesors, "Value", "Text");
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(HttpContext.Request.Query["mid"], MessageNode.SYS_MODULE_ACTIVITY_LOAD_ERROR, exception.ToString());
            }
            return View(moduleActivityModel);
        }

        public IActionResult UpdateUnitActivity([FromForm] ModuleActivityModel moduleActivityModel)
        {
            string mguid = string.Empty;
            int member_id = 0;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                }
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                _moduleActivityBusinessComponent.UpdateModuleActivity(
                                                                        moduleActivityModel.ModuleActivityID,
                                                                        moduleActivityModel.ModuleID,
                                                                        moduleActivityModel.ActivityID,
                                                                        moduleActivityModel.ActivityDate,
                                                                        moduleActivityModel.ActivityTime,
                                                                        moduleActivityModel.ActivityDuration,
                                                                        moduleActivityModel.ActivityPassMark,
                                                                        moduleActivityModel.ActivityTotalMark,
                                                                        mguid,
                                                                        moduleActivityModel.AssessorID
                                                                     );
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Connect Unit Activity", "Unit Activity has been successfully updated.", member_id);
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(moduleActivityModel.MemberID, MessageNode.SYS_MODULE_ACTIVITY_UPDATE_ERROR, exception.ToString());
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Connect Unit Activity", "Unit Activity has been unsuccessfully updated.", member_id);
            }
            return RedirectToAction("Activities", new { mid = mguid, modid = moduleActivityModel.ModuleID });
        }

        public IActionResult AddModuleActivity([FromForm]ModuleActivityModel moduleActivityModel)
        {
            string mguid = string.Empty;
            int member_id = 0;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                }
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                _moduleActivityBusinessComponent.SaveModuleActivity(moduleActivityModel);
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Connect Unit Activity", "Unit Activity has been successfully created.", member_id);
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(moduleActivityModel.MemberID, MessageNode.SYS_MODULE_ACTIVITY_CREATE_ERROR, exception.ToString());
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Connect Unit Activity", "Unit Activity has been unsuccessfully created.", member_id);
            }

            return RedirectToAction("Activities", new { mid = moduleActivityModel.MemberID, modid = moduleActivityModel.ModuleID });
        }

        public IActionResult ModuleActivitityUpload(string mid, string maid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.ModuleActivityID = Convert.ToInt32(maid);
                }
            }
            catch(Exception exception)
            {

            }
            return View();
        }

        public IActionResult ModuleActivityMember(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    if (Session.AppSession.ContainsKey(string.Format("Activity_{0}", mid)))
                    {
                        MemberMarkModel model = (MemberMarkModel)Session.AppSession[string.Format("Activity_{0}", mid)];
                        ViewBag.UnitCode = model?.UnitName;
                        ViewBag.ActivityName = model?.UnitActivityName;
                        ViewBag.ActivityDate = model?.ActivityDate;
                        Session.AppSession.Remove(string.Format("Activity_{0}", mid));
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return View();
        }

        public async Task<IActionResult> SaveModuleActivityUpload([FromForm]ActivityUploadModel activityUploadModel)
        {
            string mguid = string.Empty;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                    LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                    ModuleActivityUploadModel moduleActivityUploadModel = new ModuleActivityUploadModel();
                    await activityUploadModel.ActivityUpload.CopyToAsync(memoryStream);
                    moduleActivityUploadModel.ActivityUpload = memoryStream.ToArray();
                    moduleActivityUploadModel.MemberID = activityUploadModel.MemberID;
                    moduleActivityUploadModel.ModuleActivityID = activityUploadModel.ModuleActivityID;
                    mguid = loginBusinessComponent.GetMemberGuidByMemberID(activityUploadModel.MemberID);
                    moduleActivityBusinessComponent.SaveModuleActivityUpload(moduleActivityUploadModel);
                }
            }
            catch (Exception exception)
            {

            }
            return RedirectToAction("ActivityTimeTable", "Activity", new { mid = mguid, actid = 1 });
        }
        public IActionResult UploadBulkMark(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    if (Session.AppSession.ContainsKey(string.Format("DataUpload_{0}", mid)))
                    {
                        DataTable data = (DataTable)Session.AppSession[string.Format("DataUpload_{0}", mid)];
                        Guid importGuid = (Guid)Session.AppSession[string.Format("Import_{0}", mid)];
                        ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                        moduleActivityBusinessComponent.BulkUploadMark("[dbo].[ActivityMarkImport]", data); //Bulk Insert
                        moduleActivityBusinessComponent.ProcessMarkImport(importGuid);
                        Session.AppSession.Remove(string.Format("DataUpload_{0}", mid));
                        Session.AppSession.Remove(string.Format("Import_{0}", mid));
                        if (data.Rows.Count > 0)
                        {
                            Session.AppSession.Set(string.Format("Activity_{0}", mid), data.ToList<MemberMarkModel>()?.FirstOrDefault());
                        }
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return RedirectToAction("ModuleActivityMember", "ModuleActivity", new { mid = mid });
        }
        public async Task<string> UploadUnitActivityMark([FromForm]UploadMarkModel model)
        {
            string dataUpload = string.Empty;
            try
            {
                string filePath = string.Empty;
                LoginBusinessComponent component = new LoginBusinessComponent();
                using (var memoryStream = new MemoryStream())
                {
                    await model.FormFile.CopyToAsync(memoryStream);
                    filePath = UploadBusinessComponent.UploadFile(Session.AppSession["Uploads"].ToString(), memoryStream.ToArray(), "UploadMark.csv");
                }
                Guid importID = Guid.NewGuid();
                int UserID = component.GetMemberIDByMemberGuid(model.UserID);
                DataTable excelUpload = ExcelHelper.ProcessExcelData<MemberMarkModel>(filePath, importID, UserID);
                dataUpload = GetMarkUploadGrid(excelUpload?.ToList<MemberMarkModel>());
                Session.AppSession.Set(string.Format("DataUpload_{0}", model.UserID), excelUpload);
                Session.AppSession.Set(string.Format("Import_{0}", model.UserID), importID);
            }
            catch (Exception exception)
            {

            }
            return dataUpload;
        }

        private string GetMarkUploadGrid(List<MemberMarkModel> marks)
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                marks.ForEach(delegate (MemberMarkModel model)
                {
                    strBuilder.Append("<tr>");
                    strBuilder.Append("<td>" + model.IDNo + "</td>");
                    strBuilder.Append("<td>" + model.UnitName + "</td>");
                    strBuilder.Append("<td>" + model.UnitActivityName + "</td>");
                    strBuilder.Append("<td>" + model.ActivityDate + "</td>");
                    strBuilder.Append("<td>" + model.ActivityMark + "</td>");
                    strBuilder.Append("</tr>");
                });
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return strBuilder.ToString();
        }

        public IActionResult ModuleActivitityMemberMark(string mid, string uid, string modid, string maid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.UserGuid = uid;
                    ViewBag.ModuleID = modid;
                    ViewBag.ModuleActivityID = Convert.ToInt32(maid);
                }
            }
            catch (Exception exception)
            {

            }
            return View();
        }

        public IActionResult CreatChat(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                }
            }
            catch (Exception exception)
            {

            }
            return View();
        }

        public IActionResult CloseChat()
        {
            try
            {
                string MemberID = HttpContext.Request.Form["MemberID"].ToString();
                string chatsToBeClosed = HttpContext.Request.Form["CloseChat"].ToString();
                if (!string.IsNullOrEmpty(chatsToBeClosed))
                {
                    string[] chats = chatsToBeClosed.Split(',');
                    ModuleActivityBusinessComponent component = new ModuleActivityBusinessComponent();
                    foreach (string key in chats)
                    {
                        component.DeactivateChat(Convert.ToInt32(key));
                    }
                }
            }
            catch (Exception exception)
            {

            }

            return RedirectToAction("CreatChat", "ModuleActivity", new { mid = HttpContext.Request.Form["MemberID"].ToString() });
        }

        public IActionResult ManageChat(string mid, int cid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.ChatID = cid;
                }
            }
            catch (Exception exception)
            {

            }

            return View();
        }

        public IActionResult RemoveFromChat()
        {
            try
            {
                string MemberID = HttpContext.Request.Form["MemberID"].ToString();
                string ChatID = HttpContext.Request.Form["ChatID"].ToString();
                string chatsToBeClosed = HttpContext.Request.Form["RemoveFromChat"].ToString();
                if (!string.IsNullOrEmpty(chatsToBeClosed))
                {
                    string[] chats = chatsToBeClosed.Split(',');
                    ModuleActivityBusinessComponent component = new ModuleActivityBusinessComponent();
                    foreach (string key in chats)
                    {
                        component.RemoveParticipantsFromChat(Convert.ToInt32(ChatID), Convert.ToInt32(key));
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return RedirectToAction("ManageChat", "ModuleActivity", new { mid = HttpContext.Request.Form["MemberID"].ToString(), cid = HttpContext.Request.Form["ChatID"].ToString() });
        }

        public IActionResult SaveChat()
        {
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                int unitID = Convert.ToInt32(HttpContext.Request.Form["UnitID"].ToString());
                string chatName = HttpContext.Request.Form["txtChatName"].ToString();
                int MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(HttpContext.Request.Form["mid"].ToString());
                moduleActivityBusinessComponent.CreateModuleChat(chatName, unitID, MemberID);
            }
            catch (Exception exception)
            {

            }
            return RedirectToAction("CreatChat", "ModuleActivity", new { mid = HttpContext.Request.Form["mid"].ToString() });
        }

        public IActionResult MemberChat(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                }
            }
            catch (Exception exception)
            {

            }
            return View();
        }

        public IActionResult SaveModuleActivityMark([FromForm]ModuleActivityMarkModel moduleActivityMarkModel)
        {
            ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
            LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
            int maidP = moduleActivityMarkModel.ModuleActivityID;
            string midP = loginBusinessComponent.GetMemberGuidByMemberID(moduleActivityMarkModel.UserID);
            try
            {
                moduleActivityBusinessComponent.SaveModuleActivityMark(moduleActivityMarkModel);
            }
            catch (Exception exception)
            {

            }
            return RedirectToAction("ModuleActivityMember", "ModuleActivity", new { mid = midP, modid = moduleActivityMarkModel.ModuleID, maid = maidP });
        }

        public IActionResult ModuleActivityMessageChat(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                }
            }
            catch(Exception exception)
            {

            }
            return View();
        }

        public IActionResult CreateNewsFeed(NewFeedModel newFeed)
        {
            try
            {
                FacultyBusinessComponent faculty = new FacultyBusinessComponent();
                var modules = faculty.GetAllInstitutionModule(Session.AppSession["ClientId"].ToString());
                List<SelectListItem> listItems = modules.Select(module => new SelectListItem() { Value = module.ModuleID, Text = module.ModuleName }).ToList();
                ViewBag.MemGuid = HttpContext.Request.Query["mid"].ToString();
                newFeed.ModuleList = new SelectList(listItems, "Value", "Text");
            }
            catch(Exception exception)
            {

            }
            return View(newFeed);
        }

        public IActionResult SaveNewsFeed([FromForm]NewFeedModel feedModel)
        {
            try
            {
                ModuleActivityBusinessComponent businessComponent = new ModuleActivityBusinessComponent();
                businessComponent.InsertNewsFeed(feedModel);
            }
            catch(Exception exception)
            {

            }
            return RedirectToAction("CreateNewsFeed", "ModuleActivity", new { mid = feedModel.MemberID });
        }

        public string GetAllMemberByModuleID(string module_id)
        {
            var response = default(List<ModuleMemberModel>);
            try
            {
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                response = moduleActivityBusinessComponent.GetModuleMembersByModuleID(Convert.ToInt32(module_id));
            }
            catch(Exception exception)
            {

            }
            return JsonConvert.SerializeObject(response);
        }

        [HttpGet]
        public string GetAllChatByModuleID(string module_id)
        {
            ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
            var response = moduleActivityBusinessComponent.GetAllMessageChatByModuleID(Convert.ToInt32(module_id));
            return JsonConvert.SerializeObject(response);
        }
        private StringBuilder GetMessageChatsByMemberID(string MemberGuid, int ChatID)
        {
            StringBuilder returnResult = new StringBuilder();
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                string timespan = string.Empty;
                string previoustime_right = string.Empty;
                string previoustime_left = string.Empty;
                int MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(MemberGuid);
                var listChats = moduleActivityBusinessComponent.GetMessageChatsByChatID(ChatID, MemberID);
                returnResult.Append("<table style=\"width: 100%;\">");
                foreach (var chat in listChats)
                {
                    string name_time_or_time = string.Empty;
                    timespan = chat.DatetimeStamp.ToString("HH:mm");
                    if (chat.IsRightAlign)
                    {
                        if (!previoustime_right.Equals(timespan))
                            name_time_or_time += "<div style=\"font-size: 12px;\">" + timespan + "</div>";
                        previoustime_right = timespan;
                        returnResult.Append("<tr><td style=\"width: 50%;\"></td>");
                        returnResult.Append("<td align=\"right\" valign=\"middle\" style=\"height:25px; width: 50%;\">" + name_time_or_time + chat.PostText + "</td></tr>");
                    }
                    else
                    {
                        if (!previoustime_left.Equals(timespan))
                            name_time_or_time += "<div style=\"font-size: 12px;\">" + chat.FullName.Split(' ')[0] + timespan + "</div>";
                        previoustime_left = timespan;
                        returnResult.Append("<tr><td valign=\"middle\" style=\"height:25px; width: 50%;\" align=\"left\">" + name_time_or_time + chat.PostText + "</td>");
                        returnResult.Append("<td style=\"width: 50%;\"></td></tr>");
                    }
                }
                returnResult.Append("</table>");
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(MemberGuid, MessageNode.SYS_GET_POST_MESSAGE_ERROR, exception.ToString());
            }
            return returnResult;
        }
        [HttpPost]
        public bool GetNewChatsByChatID([FromForm]MessageChatModel model)
        {
            bool IsNewChat = false;
            try
            {
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                IsNewChat = moduleActivityBusinessComponent.GetNewChatsByChatID(model.ChatID);
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(model.MemberID))
                {
                    LogMessageBusinessComponent.InsertLogMessage(model.MemberID, MessageNode.SYS_GET_POST_MESSAGE_ERROR, exception.ToString());
                }
                else
                {
                    LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_GET_POST_MESSAGE_ERROR, exception.ToString());
                }
            }
            return IsNewChat;
        }
        [HttpPost]
        public string GetMessageChatByChatID([FromForm]MessageChatModel model)
        {
            StringBuilder returnResult = new StringBuilder();
            try
            {
                returnResult = GetMessageChatsByMemberID(model.MemberID, model.ChatID);
            }
            catch (Exception exception)
            {
                if (!string.IsNullOrEmpty(model.MemberID))
                {
                    LogMessageBusinessComponent.InsertLogMessage(model.MemberID, MessageNode.SYS_GET_POST_MESSAGE_ERROR, exception.ToString());
                }
                else
                {
                    LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_GET_POST_MESSAGE_ERROR, exception.ToString());
                }
            }
            return returnResult.ToString();
        }
        [HttpPost]
        public string SaveUnitMessagePost([FromForm]MessageChatModel model)
        {
            StringBuilder returnResult = new StringBuilder();
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                var MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(model.MemberID);
                var response = moduleActivityBusinessComponent.InsertUnitMessagePost(MemberID, model.ChatID, model.MessageText);
                if (response)
                {
                    var listChats = moduleActivityBusinessComponent.GetMessageChatsByChatID(model.ChatID, MemberID);
                    returnResult = GetMessageChatsByMemberID(model.MemberID, model.ChatID);
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(model.MemberID, MessageNode.SYS_MODULE_CHAT_POST_ERROR, exception.ToString());
            }
            return returnResult.ToString();
        }

        [HttpGet]
        public string GetUnitMessagePostByModuleID(int module_id)
        {
            var response = default(List<MessagePostModel>);
            try
            {
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                response = moduleActivityBusinessComponent.GetMessagePostByModuleID(module_id);
            }
            catch(Exception exception)
            {

            }
            return JsonConvert.SerializeObject(response);
        }
        [HttpGet]
        public string GetStudentScheduleByMemberID(string MemberID, int Month)
        {
            string response = null;
            try
            {
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                int MemID = loginBusinessComponent.GetMemberIDByMemberGuid(MemberID);
                response = JsonConvert.SerializeObject(moduleActivityBusinessComponent.GetStudentScheduleByMemberID(MemID, Month));
            }
            catch(Exception exception)
            {

            }
            return response;
        }
    }
}
