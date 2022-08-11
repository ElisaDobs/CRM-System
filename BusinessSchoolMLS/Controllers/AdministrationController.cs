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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class AdministrationController : Controller
    {
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

        public IActionResult SaveStaffMember([FromForm]ApplicationMemberModel applicationMemberModel)
        {
            string mguid = string.Empty;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                }
                ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                NotificationBusinessComponent notificationBusiness = new NotificationBusinessComponent();
                var new_staff_member = applicationBusinessComponent.SaveApplication(applicationMemberModel);
                notificationBusiness.InsertSystemNotification(
                                                                 (int)ApplicationProgressStatus.NEW_STAFF_MEMBER,
                                                                 new_staff_member.MemberID,
                                                                 Session.AppSession["ApplicationId"].ToString()
                                                             );
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mguid, MessageNode.SYS_SAVE_STAFF_MEMBER_ERROR, exception.ToString());
            }
            return RedirectToAction("StaffMembers", "Administration", new { mid = mguid });
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
                if(!string.IsNullOrEmpty(mid))
                {
                    if (Session.AppSession.ContainsKey("MemberID"))
                    {
                        Session.AppSession.Remove("MemberID");
                    }
                    ViewBag.MemGuid = mid;
                    Session.AppSession.Set("MemberID", mid);  
                }
            }
            catch(Exception exception)
            {

            }
            return View();
        }

        [HttpPost]
        public string CancelEditMember()
        {
            string MemberID = string.Empty;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    MemberID = Session.AppSession["MemberID"].ToString();
                    if (Session.AppSession.ContainsKey(string.Format("IDNo_{0}", MemberID)))
                    {
                        Session.AppSession.Remove(string.Format("IDNo_{0}", MemberID));
                    }
                }
            }
            catch(Exception exception)
            {

            }
            return MemberID;
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
            return View();
        }

        public IActionResult UpdateMemberDetails([FromForm]ApplicationMemberModel application)
        {
            string MemberID = string.Empty;
            try
            {
                ApplicationBusinessComponent businessComponent = new ApplicationBusinessComponent();
                businessComponent.UpdateStaffMemberByIDNo(application.IDNo, application.FirstName, application.LastName, application.CellNo, application.EmailAddress, application.RoleID);
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    MemberID = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                }
            }
            catch(Exception exception)
            {

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
    }
}
