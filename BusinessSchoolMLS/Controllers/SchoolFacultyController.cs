using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using MRTD.Core.Models;
using MRTD.Core.Common;
using Microsoft.Extensions.Primitives;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using BusinessSchoolMLS.SchoolBusinessComponent;
using System.Text;
using iTextSharp.text;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class SchoolFacultyController : Controller
    {
        private readonly FacultyBusinessComponent _facultyBusinessComponent;
        private readonly LoginBusinessComponent _loginBusinessComponent;
        private readonly NotificationBusinessComponent _notificationBusinessComponent;
        private readonly ModuleActivityBusinessComponent _moduleActivityBusinessComponent;
        private readonly ApplicationBusinessComponent _applicationBusinessComponent;
        private readonly AdministratorBusinessComponent _administratorBusinessComponent;
        
        public SchoolFacultyController()
        {
            _facultyBusinessComponent = new FacultyBusinessComponent();
            _loginBusinessComponent = new LoginBusinessComponent();
            _notificationBusinessComponent = new NotificationBusinessComponent();
            _applicationBusinessComponent = new ApplicationBusinessComponent();
            _moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
            _administratorBusinessComponent = new AdministratorBusinessComponent();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SchoolFaculty(string mid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                if (Session.AppSession["ClientId"].ToString().ToUpper() != "FD49002B-C33F-4615-9E2C-D49E11AF4394")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("FacultyQualification", "SchoolFaculty", new { mid= mid, fid = 8});
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult FacultyQualification(string mid, string fid)
        {
            QualificationModel qualificationModel = new QualificationModel();
            try
            {
                if (!string.IsNullOrEmpty(mid) )
                {
                    ViewBag.FacultyID = Convert.ToInt32(fid);
                    var lst_all_nqlevel = _facultyBusinessComponent.GetAllNQLevel().Select(level => new SelectListItem { Value = level.NQLevelID.ToString(), Text = level.NQLevelName });
                    var lst_all_qual_types = _facultyBusinessComponent.GetAllQualificationTypes()
                                             ?.Select(type => new SelectListItem { Value = type.QualTypeID.ToString(), Text = type.QualTypeName });
                    qualificationModel.QualificationNQLevel = new SelectList(lst_all_nqlevel, "Value", "Text");
                    qualificationModel.QualificationTypes = new SelectList(lst_all_qual_types, "Value", "Text");
                }
            }
            catch(Exception)
            {

            }
            return View(qualificationModel);
        }

        public IActionResult AddQualification([FromForm]QualificationModel qualificationModel)
        {
            string mguid = string.Empty;
            int member_id = 0,
                fac_id = 0;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                }
                 fac_id = qualificationModel.FacultyID;
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                _facultyBusinessComponent.SaveFacultyQualification(qualificationModel);
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Connect Program", "Tipp Connect Program has been successfully created!", member_id);

            }
            catch(Exception)
            {
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Connect Program", "Tipp Connect Program has unsuccessfully created!", member_id);
            }
            return RedirectToAction("FacultyQualification", "SchoolFaculty", new { mid = mguid, fid = fac_id });
        }

        public IActionResult EditProgram([FromForm]QualificationModel qualificationModel)
        {
            string mguid = string.Empty;
            int fac_id = 0,
                member_id = 0;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                }
                fac_id = qualificationModel.FacultyID;
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                _facultyBusinessComponent.UpdateQualificationByQualificationID(qualificationModel);
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Connect Program", "Tipp Connect Program has unsuccessfully updated.", member_id);
            }
            catch(Exception)
            {
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Connect Program", "Tipp Connect Program has been unsuccessfully created!", member_id);
            }
            return RedirectToAction("FacultyQualification", "SchoolFaculty", new { mid = mguid, fid = fac_id });
        }

        public IActionResult FacultyModule(ModuleModel moduleModel)
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["mid"]))
            {
                ViewBag.MemGuid = HttpContext.Request.Query["mid"].ToString();
                ViewBag.FacultyID = Int32.Parse(HttpContext.Request.Query["fid"].ToString());
                ViewBag.QualID = Int32.Parse(HttpContext.Request.Query["qid"].ToString());
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                var lst_all_nqlevel = facultyBusinessComponent.GetAllNQLevel().Select(level => new SelectListItem { Value = level.NQLevelID.ToString(), Text = level.NQLevelName });
                moduleModel.ModuleNQLevels = new SelectList(lst_all_nqlevel, "Value", "Text");
                return View(moduleModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult CreateGroup(string mid)
        {
            UnitGroupModel unitGroupModel = null;
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                    unitGroupModel = new UnitGroupModel();
                    var lst_all_groups = facultyBusinessComponent.GetAllStudyUnits(Session.AppSession["ClientId"].ToString()).Select(unit => new SelectListItem { Value = unit.ModuleID.ToString(), Text = unit.ModuleName });
                    unitGroupModel.ModuleList = new SelectList(lst_all_groups, "Value", "Text");
                }
            }
            catch(Exception exception)
            {

            }
            return View(unitGroupModel);
        }

        public IActionResult SaveUnitGroup([FromForm]UnitGroupModel unitGroupModel)
        {
            FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
            facultyBusinessComponent.InsertUnitGroup(unitGroupModel);
            return RedirectToAction("CreateGroup", "SchoolFaculty", new { mid = unitGroupModel.MemberID });
        }

        public IActionResult SaveUnitGroupMembers([FromForm]UnitGroupAttendanceModel unitGroupAttendanceModel)
        {
            int member_id = 0;
            try
            {
                string all_form_keys = HttpContext.Request.Form["MemberID"];
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(unitGroupAttendanceModel.UserID);
                foreach (string MemID in all_form_keys.Split(','))
                {
                    UnitGroupAttendanceModel unitGroup = new UnitGroupAttendanceModel()
                    {
                        GroupID = unitGroupAttendanceModel.GroupID,
                        UserID = unitGroupAttendanceModel.UserID,
                        MemberID = MemID
                    };
                    _facultyBusinessComponent.InsertUnitGroupAttendance(unitGroup);
                }
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Unit Group Student", "Group Student(s) are successfully added.", member_id);
            }
            catch(Exception exception)
            {
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Unit Group Student", "Group Student(s) are unsuccssfully added.", member_id);
            }
            return RedirectToAction("GroupAttendants", "SchoolFaculty", new { mid = unitGroupAttendanceModel.UserID, gid = unitGroupAttendanceModel.GroupID });
        }

        public IActionResult GroupAttendants(string mid, int gid)
        {
            if(!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.GroupID = gid;
            }
            return View();
        }

        public IActionResult AddModule([FromForm]ModuleModel moduleModel)
        {
            if (!string.IsNullOrEmpty(moduleModel.MemberID))
            {
                string mguid = moduleModel.MemberID;
                int fac_id = int.Parse(moduleModel.FacultyID);
                int qual_id = Int32.Parse(moduleModel.QualificationID);
                var faculty_component = new FacultyBusinessComponent();
                faculty_component.SaveFacultyQualModule(moduleModel);

                return RedirectToAction("FacultyModule", "SchoolFaculty", new { mid = mguid, fid = fac_id, qid = qual_id });
            }
            else
            {
                return RedirectToAction("Login", "Login" );
            }
        }

        public IActionResult EditModule([FromForm]ModuleModel moduleModel)
        {
            string mguid = moduleModel.MemberID;
            int fac_id = Convert.ToInt32(moduleModel.FacultyID),
                qual_id = Convert.ToInt32(moduleModel.QualificationID);

            FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();

            facultyBusinessComponent.UpdateModuleByModuleID(moduleModel);

            return RedirectToAction("FacultyModule", "SchoolFaculty", new { mid = mguid, fid = fac_id, qid = qual_id });
        }

        public IActionResult ProspectiveStudent(string mid, int fid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.FacultyID = fid;

                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch(Exception exception)
            {
                return RedirectToAction("Index", "DisplayError");
            }
        }

        public IActionResult Prospective(string mid, string sid, int fid)
        {
            if(!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.StudentID = sid;
                ViewBag.FacultyID = fid;
            }

            return View();
        }

        public IActionResult Library(string mid, int uid = 0)
        {
            if(!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.UnitID = uid;
            }

            return View();
        }

        public IActionResult StudyMaterial(string mid, int uid = 0)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.UnitID = uid;
            }

            return View();
        }

        public IActionResult LearningProgram(string mid, int pid, int fid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.ProgramID = pid;
                ViewBag.FacultyID = fid;
            }

            return View();
        }
        
        public IActionResult FindLearningMaterial(string MemberID, int UnitID)
        {
            return RedirectToAction("StudyMaterial", "SchoolFaculty", new { mid= MemberID, uid=UnitID });
        }

        public FileResult DownloadLearningMaterial(int upid, string file_name)
        {
            ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
            byte[] file_bite = (byte[])applicationBusinessComponent.GetRequiredDocumentByUploadID(upid);
            return File(file_bite, "application/pdf", file_name);
        }

        [HttpGet]
        public string GetAllStudyUnits()
        {
            List<ModuleModel> all_studies = null;
            try
            {
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                all_studies = facultyBusinessComponent.GetAllStudyUnits(Session.AppSession["ClientId"].ToString());
            }
            catch(Exception exception)
            {

            }
            return JsonConvert.SerializeObject(all_studies);
        }

        public async Task<string> UploadMaterialStudyUnit([FromForm]LearningMaterialUploadModel learningMaterialUploadModel)
        {
            dynamic lst_learing_materials = null;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    ApplicantRequiredDocument applicantRequiredDocument = new ApplicantRequiredDocument();
                    await learningMaterialUploadModel.Upload.CopyToAsync(memoryStream);
                    applicantRequiredDocument.MemberID = _loginBusinessComponent.GetMemberIDByMemberGuid(learningMaterialUploadModel.MemberID);
                    applicantRequiredDocument.UploadDocument = memoryStream.ToArray();
                    applicantRequiredDocument.UploadTypeID = learningMaterialUploadModel.UploadType;
                    applicantRequiredDocument.UnitID = learningMaterialUploadModel.UnitID;
                    applicantRequiredDocument.UploadFileName = learningMaterialUploadModel.Upload.FileName;
                    _applicationBusinessComponent.InsertApplicationUpload(applicantRequiredDocument);
                }
                lst_learing_materials = _facultyBusinessComponent.GetUnitLearningMaterialByUnitID(learningMaterialUploadModel.UnitID);
            }
            catch(Exception exception)
            {

            }
            return JsonConvert.SerializeObject(lst_learing_materials);
        }

        public string DeleteStudyLearningMaterial(string mid, int mod_id, int uid)
        {
            dynamic lst_learing_materials = null;
            int member_id = 0;
            try
            {
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(mid);
                _facultyBusinessComponent.RemoveUnitLearingMaterial(member_id, uid);
                lst_learing_materials = _facultyBusinessComponent.GetUnitLearningMaterialByUnitID(mod_id);
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Learning Material", "Learning Material is successfully removed.", member_id);
            }
            catch(Exception exception)
            {
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Learning Material", "Learning Material is unsuccessfully removed.", member_id);
            }
            return JsonConvert.SerializeObject(lst_learing_materials);
        }
        [Obsolete]
        public IActionResult DownloadProgramSchedule(string mid)
        {
            byte[] bytes = null;
            string IdentityNo = string.Empty;
            try
            {
                int MemberID = _loginBusinessComponent.GetMemberIDByMemberGuid(mid);
                var memberProfile = _loginBusinessComponent.GetMemberProfileByMemberId(mid);
                IdentityNo = memberProfile.IDNo;
                List<UnitProgramScheduleModel> unitProgramScheduleModels = _moduleActivityBusinessComponent.GetModuleActivityResultsByMember(MemberID, DateTime.Now.Year);
                var template = _administratorBusinessComponent.GetTemplateByTempleteID((int)ApplicationProgressStatus.PROGRAM_SCHEDULE);
                string templateText = template.TemplateText,
                       theader = "<br /><table><thead><tr bgcolor=\"#377091\" color=\"#ffffff\"><th>Unit Name</th><th>Credit</th><th>Year</th>";
                templateText = templateText.Replace("#STUDENTADDRESS#", "ID No. " + memberProfile.IDNo + "<br />" 
                                                   + "Name. " + memberProfile.FirstName + " " + memberProfile.LastName 
                                                   + "<br />" + memberProfile.PhysicalAddress.Replace(",", "<br />"));
                UnitProgramScheduleModel max_activity_obj = unitProgramScheduleModels.FirstOrDefault();
                foreach(var remainder_header in max_activity_obj.ModuleActivity)
                {
                    theader += $"<th>{remainder_header.Key}</th>";
                }
                theader += "</thead><tbody>";
                foreach (var program_unit_schedule in unitProgramScheduleModels)
                {
                    theader += "<tr>";
                    theader += $"<td>{program_unit_schedule.ModuleName}</td>";
                    theader += $"<td>{program_unit_schedule.Credit}</td>";
                    theader += $"<td>{program_unit_schedule.AcademicYear}</td>";
                    foreach(var tbody in max_activity_obj.ModuleActivity)
                    {
                        var obj_key = program_unit_schedule.ModuleActivity.Where(pair => pair.Key == tbody.Key).Select(pair => pair);
                        if (obj_key.Any())
                        {
                            theader += $"<td>{obj_key.FirstOrDefault().Value.ToString()}</td>";
                        }
                        else
                        {
                            theader += $"<td>----</td>";
                        }
                    }
                    theader += "</tr>";
                }
                theader += "</tbody></table>";
                templateText = templateText.Replace("#PROGRAMSCHEDULE#", theader);
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
            return File(bytes, "application/pdf", string.Format("{0}.pdf", IdentityNo));
        }

        [HttpGet]
        public string GetEnrollemntStatusByAcademicYear(string mid, int year)
        {
            string returnResult = string.Empty;
            try
            {
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                var lst_all_status = facultyBusinessComponent.GetEnrollmentStatusModelByAcademicYear(Session.AppSession["ClientId"].ToString(), year);
                var seriesNames = lst_all_status.Select(series => series.EnrollmentStatusName);
                var seriesValues = lst_all_status.Select(series => series.EnrollmentStatusCount);
                returnResult = JsonConvert.SerializeObject(new { Categories = seriesNames, Series = seriesValues });
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid,MessageNode.SYS_FACULTY_ENROLLMENT_STATUS_COUNT_ERROR,exception.ToString());
            }
            return returnResult;
        }

        [HttpGet]
        public string GetAllInstitutionModule()
        {
            string returnResult = string.Empty;
            try
            {
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                var all_instituition = facultyBusinessComponent.GetAllInstitutionModule(Session.AppSession["ClientId"].ToString());
                returnResult = JsonConvert.SerializeObject(all_instituition.Select(a => new { ID = a.ModuleID, UnitID = a.ModuleUnitCode, UnitName = a.ModuleName }).ToList());
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(HttpContext.Request.Query["mid"], MessageNode.SYS_GET_ALL_INSTITUTION_MODULE_ERROR, exception.ToString());
            }
            return returnResult;
        }

        [HttpGet]
        public string GetRegisteredUnitByMemberID(string MemberID)
        {
            string returnResult = string.Empty;
            try
            {
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                returnResult = JsonConvert.SerializeObject(facultyBusinessComponent.GetRegisteredUnitByMemberID(MemberID));
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(MemberID, MessageNode.SYS_FACULTY_GET_REGISTERED_UNIT_BY_MEMBER_ID_ERROR,exception.ToString());
            }
            return returnResult;
        }

        public IActionResult ProgramSchedule(string mid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
            }

            return View();
        }

        public IActionResult Graduation(string mid)
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

        private string GetAllMemberList(List<GraduationMemberModel> graduations)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                graduations?.ForEach(delegate(GraduationMemberModel graduation) {
                    builder.Append("<tr>");
                    builder.Append("<td>" + graduation.FullName + "</td>");
                    builder.Append("<td>" + graduation.IDNo + "</td>");
                    builder.Append("<td>" + graduation.CellNo + "</td>");
                    builder.Append("<td>" + graduation.QualificationName + "</td>");
                    builder.Append("<td>" + graduation.UnitCode + "</td>");
                    builder.Append("<td>" + graduation.CompletedDateTime.ToString("yyyy-MM-dd") + "</td>");
                    builder.Append("</tr>");
                });
                return builder.ToString();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        [HttpGet]
        public string GetAllMemberDueForGraduationQualificationID(int qid)
        {
            string graduation_list = string.Empty;
            try
            {
                FacultyBusinessComponent businessComponent = new FacultyBusinessComponent();
                graduation_list = GetAllMemberList(businessComponent.GetAllMemberDueForGraduationByQualificationID(qid));
            }
            catch(Exception)
            {

            }
            return graduation_list;
        }

        public IActionResult RemoveStudentFromGroup(string mid, int gid, string stdid)
        {
            int member_id = 0,
                student_id = 0;
            try
            {
                member_id = _loginBusinessComponent.GetMemberIDByMemberGuid(mid);
                student_id = _loginBusinessComponent.GetMemberIDByMemberGuid(stdid);
                _facultyBusinessComponent.RemoveStudentFromUnitGroup(gid, student_id);
                _notificationBusinessComponent.InsertWebPushNotification("info", "Tipp Unit Group", "Group student is successfully removed!", member_id);
            }
            catch(Exception exception)
            {
                _notificationBusinessComponent.InsertWebPushNotification("error", "Tipp Unit Group", "Group student is unsuccessfully removed!", member_id);
            }
            return RedirectToAction("GroupAttendants", "SchoolFaculty", new { mid = mid, gid = gid });
        }
    }
}
