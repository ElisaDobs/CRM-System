using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using MRTD.Core.Models;
using MRTD.Core.Common;
using MRTD.Core.Encryption;
using MRTD.Core.Upload;
using BusinessSchoolMLS.SchoolBusinessComponent;
using Microsoft.Extensions.Primitives;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.IO;
using iTextSharp.text;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ApplicationBusinessComponent applicationBusinessComponent;
        private readonly AdministratorBusinessComponent administratorBusinessComponent;
        private readonly LoginBusinessComponent loginBusinessComponent;
        private readonly  NotificationBusinessComponent notificationBusiness;
        public AdministrationController()
        {
            applicationBusinessComponent = new ApplicationBusinessComponent();
            administratorBusinessComponent = new AdministratorBusinessComponent();
            loginBusinessComponent = new LoginBusinessComponent();
            notificationBusiness  = new NotificationBusinessComponent();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult CreateMember(ApplicationMemberModel applicationMemberModel)
        {
            try
            {
                string MemberID = Request.Query["mid"];
                if (!string.IsNullOrEmpty(MemberID))
                {
                    if (Session.AppSession.ContainsKey("MemberID"))
                    {
                        Session.AppSession.Remove("MemberID");
                    }
                    Session.AppSession.Set("MemberID", MemberID);
                    ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                    FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                    AdministratorBusinessComponent administratorBusinessComponent = new AdministratorBusinessComponent();
                    var lst_all_gender = applicationBusinessComponent.GetAllGender().Select(sex => new SelectListItem { Value = sex.GenderID.ToString(), Text = sex.GenderName });
                    var lst_all_faculties = facultyBusinessComponent.GetAllBusinessFaculty(Session.AppSession["ClientId"].ToString()).Select(fac => new SelectListItem { Value = fac.FacultyID.ToString(), Text = fac.FacultyName });
                    var lst_all_roles = administratorBusinessComponent.GetStaffRoleModels().Select(role => new SelectListItem { Value = role.RoleID.ToString(), Text = role.RoleName });
                    applicationMemberModel.Genders = new SelectList(lst_all_gender, "Value", "Text");
                    applicationMemberModel.SchoolFaculties = new SelectList(lst_all_faculties, "Value", "Text");
                    applicationMemberModel.StaffRoles = new SelectList(lst_all_roles, "Value", "Text");
                    return View(applicationMemberModel);
                }
            }
            catch(Exception exception)
            {

            }
            return View(applicationMemberModel);
        }

        public IActionResult FinancialAccount(string mid, string aid, string sid = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.StudentGuid = sid;
                    if (!string.IsNullOrEmpty(aid))
                    {
                        ViewBag.AcademicYear = int.Parse(aid);
                    }
                    else
                    {
                        ViewBag.AcademicYear = DateTime.Now.Year;
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return View();
        }

        public IActionResult FinancialAccountByAcademicYear(string mguid, string StudentNo, string txtAcademicYear)
        {
            string student_guid = string.Empty;
            try
            {
                if (StudentNo != null)
                {
                    OnlineModuleActivityBusinessComponent onlineModuleActivityBusinessComponent = new OnlineModuleActivityBusinessComponent();
                    student_guid = onlineModuleActivityBusinessComponent.GetStudentGuidByStudentNumber(StudentNo).ToString();
                }
                else
                {
                    student_guid = mguid;
                }
            }
            catch(Exception exception)
            {

            }
            return RedirectToAction("FinancialAccount", "Administration", new { mid = mguid, aid =txtAcademicYear, sid = student_guid });
        }

        public string GetPushWebNotificationByMemberId(string mid)
        {
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                NotificationBusinessComponent notification = new NotificationBusinessComponent();
                int MemberId = loginBusinessComponent.GetMemberIDByMemberGuid(mid);
                PushWebNotification push = notification.GetWebPushNotificationByMemberId(MemberId);

                return JsonConvert.SerializeObject(push);
            }
            catch(Exception)
            {
                return JsonConvert.SerializeObject(new PushWebNotification { PushType = "", PushText = "", PushTitle = "" });
            }
        }

        public IActionResult SaveStaffMember([FromForm]ApplicationMemberModel applicationMemberModel)
        {
            string mguid = string.Empty;
            int mmeber_id = 0;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                }
                mmeber_id = loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                var new_staff_member = applicationBusinessComponent.SaveApplication(applicationMemberModel);
                notificationBusiness.InsertSystemNotification(
                                                                 (int)ApplicationProgressStatus.NEW_STAFF_MEMBER,
                                                                 new_staff_member.MemberID,
                                                                 Session.AppSession["ApplicationId"].ToString()
                                                             );
                notificationBusiness.InsertWebPushNotification("info", "Create Tipp Connect User", "Tipp Connect User has been created!", mmeber_id);
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mguid, MessageNode.SYS_SAVE_STAFF_MEMBER_ERROR, exception.ToString());
                notificationBusiness.InsertWebPushNotification("error", "Tipp Connect User", "Tipp Connect User has not been created!", mmeber_id);
            }
            return RedirectToAction("StaffMembers", "Administration", new { mid = mguid });
        }


        public string DeleteTippConnectUser(string IDNo)
        {
            string mguid = string.Empty;
            int member_id = 0;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                }
                member_id = loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                applicationBusinessComponent.RemoveUserFromTippConnect(IDNo, member_id);
                notificationBusiness.InsertWebPushNotification("info", "Delete Tipp Connect User", "Tipp Connect User has been deleted!", member_id);
            }
            catch (Exception exception)
            {
                notificationBusiness.InsertWebPushNotification("error", "Tipp Connect User", "Tipp Connect User failed to be removed!", member_id);
            }
            return JsonConvert.SerializeObject(new { StatusCode = 200, Message = "User is removed from the system." });
        }

        public IActionResult DocumentVerify(string mid, string uid)
        {
            try
            {
                if (!string.IsNullOrEmpty(uid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.UID = uid;
                }
            }
            catch(Exception exception)
            {

            }
            return View();
        }

        public IActionResult EditStaffMember(string mid)
        {
            try
            {
                ApplicationMemberModel applicationMemberModel = new ApplicationMemberModel();
                if (!string.IsNullOrEmpty(mid))
                {
                    if (Session.AppSession.ContainsKey("MemberID"))
                    {
                        Session.AppSession.Remove("MemberID");
                    }
                    ViewBag.MemGuid = mid;
                    Session.AppSession.Set("MemberID", mid);
                    
                    FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                    AdministratorBusinessComponent administratorBusinessComponent = new AdministratorBusinessComponent();
                    var lst_all_gender = applicationBusinessComponent.GetAllGender().Select(sex => new SelectListItem { Value = sex.GenderID.ToString(), Text = sex.GenderName });
                    var lst_all_faculties = facultyBusinessComponent.GetAllBusinessFaculty(Session.AppSession["ClientId"].ToString()).Select(fac => new SelectListItem { Value = fac.FacultyID.ToString(), Text = fac.FacultyName });
                    var lst_all_roles = administratorBusinessComponent.GetStaffRoleModels().Select(role => new SelectListItem { Value = role.RoleID.ToString(), Text = role.RoleName });
                    applicationMemberModel.Genders = new SelectList(lst_all_gender, "Value", "Text");
                    applicationMemberModel.SchoolFaculties = new SelectList(lst_all_faculties, "Value", "Text");
                    applicationMemberModel.StaffRoles = new SelectList(lst_all_roles, "Value", "Text");
                }
                return View(applicationMemberModel);
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SYS_SAVE_STAFF_MEMBER_ERROR, exception.ToString());
            }
            return RedirectToAction("Login", "Login");
        }
        public IActionResult StaffMembers(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    if (Session.AppSession.ContainsKey("MemberID"))
                    {
                        Session.AppSession.Remove("MemberID");
                    }
                    Session.AppSession.Set("MemberID", mid);
                }
            }
            catch(Exception exception)
            {

            }
            return RedirectToAction("EditStaffMember", "Administration", new { mid = mid });
        }

        public IActionResult UpdateMemberDetails([FromForm]ApplicationMemberModel application)
        {
            string MemberID = string.Empty;
            int member_id = 0;
            LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
            ApplicationBusinessComponent businessComponent = new ApplicationBusinessComponent();
            NotificationBusinessComponent notificationBusinessComponent = new NotificationBusinessComponent();
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    MemberID = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                }
                member_id = loginBusinessComponent.GetMemberIDByMemberGuid(MemberID);
                businessComponent.UpdateStaffMemberByIDNo(application.IDNo, application.FirstName, application.LastName, application.CellNo, application.EmailAddress, application.RoleID, application.GenderID);
                notificationBusinessComponent.InsertWebPushNotification("info", "Update Tipp Connect User", "Tipp Connect User has been updated!", member_id);
            }
            catch(Exception exception)
            {
                notificationBusinessComponent.InsertWebPushNotification("error", "Update Tipp Connect User", "Tipp Connect User update has failed!", member_id);
            }
            return RedirectToAction("StaffMembers", "Administration", new { mid = MemberID });
        }
        public IActionResult SearchStaffMember(string IDNo)
        {
            string MemberID = string.Empty;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    MemberID = Session.AppSession["MemberID"].ToString();
                    if (!string.IsNullOrEmpty(IDNo))
                    {
                        Session.AppSession.Set(string.Format("IDNo_{0}", MemberID), IDNo);
                    }
                    Session.AppSession.Remove("MemberID");
                }
            }
            catch(Exception exception)
            {

            }
            return RedirectToAction("EditStaffMember", "Administration", new { mid = MemberID });
        }
        public FileResult DownloadDocuments()
        {
            byte[] file_bite = null;
            var form_keys = default(KeyValuePair<string, StringValues>);
            try
            {
                ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                form_keys = HttpContext.Request.Form.FirstOrDefault();
                string path = applicationBusinessComponent.GetUploadFilePathByUploadID(int.Parse(form_keys.Key));
                file_bite = UploadBusinessComponent.GetDocumentByPathName(path);
            }
            catch(Exception exception)
            {

            }
            return File(file_bite, "application/pdf", form_keys.Value.ToString());
        }

        public IActionResult ApproveMemberEnrollement([FromForm]DocumentVerifyModel documentVerifyModel)
        {
            try
            {
                ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                NotificationBusinessComponent notification = new NotificationBusinessComponent();
                if (Int32.Parse(documentVerifyModel.EnrollmentStatusID) == 7)
                {
                    notification.InsertSystemNotification(
                                                            (int)ApplicationProgressStatus.LETTER_ACCEPTANCE_OF_LEARNER_OF_CODE_OF_CONDUCT,
                                                            Guid.Parse(documentVerifyModel.ProspectiveID),
                                                            Session.AppSession["ApplicationId"].ToString(),
                                                            (int)UploadLetter.LETTER_ACCEPTANCE_OF_LEARNER_OF_CODE_OF_CONDUCT_UPLOAD_ID
                                                         );
                }
                else if (Int32.Parse(documentVerifyModel.EnrollmentStatusID) == 11)
                {
                    notification.InsertSystemNotification(
                                                            (int)ApplicationProgressStatus.REGISTRATION_LETTER,
                                                            Guid.Parse(documentVerifyModel.ProspectiveID),
                                                            Session.AppSession["ApplicationId"].ToString(),
                                                            (int)UploadLetter.REGISTRATION_LETTER_UPLOAD_ID
                                                         );
                }
                else
                {
                    notification.InsertSystemNotification(
                                                            (int)ApplicationProgressStatus.PROOF_OF_PAYMENT,
                                                            Guid.Parse(documentVerifyModel.ProspectiveID),
                                                            Session.AppSession["ApplicationId"].ToString()
                                                         );
                }
                applicationBusinessComponent.UpdateEnrollmentStatusByMemberID(documentVerifyModel);
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(documentVerifyModel.ApproverID, MessageNode.SYS_APPLICATION_APPROVE_ENROLLMENT_ERROR, exception.ToString());
            }
            return RedirectToAction("Prospective", "SchoolFaculty", new { mid = documentVerifyModel.ApproverID, sid= documentVerifyModel.ProspectiveID});
        }


        public IActionResult ModuleAllocation(string mid, string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.UID = uid;
            }

            return View();
        }

        [HttpGet]
        public string GetQualificationModulesByMemberID(string mid, int lid)
        {
            List<ModuleModel> lst_all_modules = null;

            LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();

            FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();

            int MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(mid);

            lst_all_modules = facultyBusinessComponent.GetModuleByMemberIdAndLevel(MemberID, lid);

            return JsonConvert.SerializeObject(lst_all_modules);
        }

        [Obsolete]
        public IActionResult DownLoadFinanceStatement(string mid, int qid)
        {
            string templateText = string.Empty,
                   IdentityNo = string.Empty;
            byte[] bytes = null;
            try
            {
                int MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(mid);
                IdentityNo = loginBusinessComponent.GetMemberProfileByMemberId(mid).IDNo;
                templateText = administratorBusinessComponent.GetStudentFincialStatementByMemberID(MemberID, qid, (int)ApplicationProgressStatus.FINANCIAL_STATEMENT);
                var all_academic_finances = applicationBusinessComponent.GetYearFinancialStatementByMemberID(MemberID, qid);
                string statements = "";
                string statement_totals = "<tr><td colspan=\"4\" align=\"right\"><b>Credit</b></td>";
                foreach (var finance in all_academic_finances)
               {
                    statements += "<tr>";
                    statements += "<td>" + finance.FinancialDate.ToString("yyyy-MM-dd") + "</td>";
                    statements += "<td>" + finance.ReferenceNo + "</td>";
                    statements += "<td>" + finance.Allocation + "</td>";
                    statements += "<td>" + finance.Description + "</td>";
                    statements += "<td>" + string.Format("R{0:#.00}", finance.Debit) + "</td>";
                    statements += "<td>" + finance.Credit + "</td>";
                    statements += "<td>" + string.Format("R{0:#.00}", finance.Balance) + "</td>";
                    statements += "</tr>";
                }
                templateText = templateText.Replace("#FINANCESTATEMENT#", statements);
                statement_totals += "<td>" + string.Format("R{0:#.00}", all_academic_finances.Sum(a => a.Credit)) + "</td>";
                statement_totals += "<td><b>Balance</b></td>;";
                statement_totals += "<td>" + string.Format("R{0:#.00}", all_academic_finances.Sum(a => a.Balance)) + "</td>";
                templateText = templateText.Replace("#DETAILS#", statement_totals);

                StringReader sr = new StringReader(templateText);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    bytes = memoryStream.ToArray();
                    memoryStream.Close();
                }
            }
            catch(Exception exception)
            {

            }
            return File(bytes, "application/pdf", string.Format("Statement-{0}.pdf", IdentityNo));
        }

        public IActionResult FinancialAccountStudent(string mid, int pid)
        {
            if(!string.IsNullOrEmpty(mid))
            {
                Session.AppSession.Remove("ProgramID");
                Session.AppSession.Add("ProgramID", pid);
            }

            return View();
        }
    }
}
