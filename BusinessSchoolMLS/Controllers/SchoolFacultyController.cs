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
using BusinessSchoolMLS.SchoolBusinessComponent;
using System.Text;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class SchoolFacultyController : Controller
    {
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult FacultyQualification(QualificationModel qualificationModel)
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["mid"]))
            {
                ViewBag.MemGuid = HttpContext.Request.Query["mid"].ToString();
                ViewBag.FacultyID = Convert.ToInt32(HttpContext.Request.Query["fid"].ToString());
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                var lst_all_nqlevel = facultyBusinessComponent.GetAllNQLevel().Select(level => new SelectListItem { Value = level.NQLevelID.ToString(), Text = level.NQLevelName });
                var lst_all_qual_types = facultyBusinessComponent.GetAllQualificationTypes()
                                         ?.Select(type => new SelectListItem { Value = type.QualTypeID.ToString(), Text = type.QualTypeName });
                qualificationModel.QualificationNQLevel = new SelectList(lst_all_nqlevel, "Value", "Text");
                qualificationModel.QualificationTypes = new SelectList(lst_all_qual_types, "Value", "Text");
                return View(qualificationModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public IActionResult AddQualification([FromForm]QualificationModel qualificationModel)
        {
            if (!string.IsNullOrEmpty(qualificationModel.MemberID))
            {
                string mguid = qualificationModel.MemberID;
                int fac_id = qualificationModel.FacultyID;
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                facultyBusinessComponent.SaveFacultyQualification(qualificationModel);
                return RedirectToAction("FacultyQualification", "SchoolFaculty", new { mid = mguid, fid = fac_id });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult EditProgram([FromForm]QualificationModel qualificationModel)
        {
            if (!string.IsNullOrEmpty(qualificationModel.MemberID))
            {

                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                string mguid = qualificationModel.MemberID;
                int fac_id = qualificationModel.FacultyID;
                facultyBusinessComponent.UpdateQualificationByQualificationID(qualificationModel);
                return RedirectToAction("FacultyQualification", "SchoolFaculty", new { mid = mguid, fid = fac_id });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
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
            FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
            string all_form_keys = HttpContext.Request.Form["MemberID"];
            foreach (string MemID in all_form_keys.Split(','))
            {
                UnitGroupAttendanceModel unitGroup = new UnitGroupAttendanceModel()
                {
                    GroupID = unitGroupAttendanceModel.GroupID,
                    UserID = unitGroupAttendanceModel.UserID,
                    MemberID = MemID
                };
                facultyBusinessComponent.InsertUnitGroupAttendance(unitGroup);
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

        public async Task<IActionResult> UploadMaterialStudyUnit([FromForm]LearningMaterialUploadModel learningMaterialUploadModel)
        {
            using (var memoryStream = new MemoryStream())
            {
                ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                ApplicantRequiredDocument applicantRequiredDocument = new ApplicantRequiredDocument();
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                await learningMaterialUploadModel.Upload.CopyToAsync(memoryStream);
                applicantRequiredDocument.MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(learningMaterialUploadModel.MemberID);
                applicantRequiredDocument.UploadDocument = memoryStream.ToArray();
                applicantRequiredDocument.UploadTypeID = learningMaterialUploadModel.UploadType;
                applicantRequiredDocument.UnitID = learningMaterialUploadModel.UnitID;
                applicantRequiredDocument.UploadFileName = learningMaterialUploadModel.Upload.FileName;
                applicationBusinessComponent.InsertApplicationUpload(applicantRequiredDocument);
            }
            return RedirectToAction("Library", "SchoolFaculty", new { mid = learningMaterialUploadModel.MemberID, uid = learningMaterialUploadModel.UnitID });
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
    }
}
