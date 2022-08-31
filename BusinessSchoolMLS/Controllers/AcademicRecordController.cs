using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSchoolMLS.SchoolBusinessComponent;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using MRTD.Core.Models;
using MRTD.Core.Extensions;
using MRTD.Core.Common;
using BusinessSchoolMLS.SchoolBusinessComponent;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class AcademicRecordController : Controller
    {
        private readonly FacultyBusinessComponent _facultyBusinessComponent;
        // GET: /<controller>/
        public AcademicRecordController()
        {
            _facultyBusinessComponent = new FacultyBusinessComponent();
        }
        public IActionResult ProgramStudents(string mid, int pid)
        {
            try
            {
                if (pid != 0)
                {
                    Session.AppSession.Add("ProgramID", pid);
                }
            }
            catch (Exception exception)
            {

            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ModuleActivityReport(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    if (Session.AppSession.ContainsKey(mid))
                    {
                        ModuleActivityModel data = (ModuleActivityModel)Session.AppSession[mid];
                        ViewBag.ModuleID = data.ModuleID.ToString();
                        ViewBag.ModuleActivityID = data.ModuleActivityID.ToString();
                        Session.AppSession.Remove(mid); //Clear Session
                    }
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SYS_MODULE_ACTIVITY_REPORT_ERROR, exception.ToString());
            }
            return View();
        }
        public IActionResult AcademicRecord(string mid, string sid = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.StudentGuid = sid;
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SYS_STUDENT_ACADEMIC_RECORD_ERROR, exception.ToString());
            }
            return View();
        }

        public IActionResult LoadReport()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Form["MemberID"]))
                {
                    Session.AppSession.Set(HttpContext.Request.Form["MemberID"].ToString(), new ModuleActivityModel
                    {
                        ModuleID = int.Parse(HttpContext.Request.Form["UnitID"].ToString()),
                        ModuleActivityID = int.Parse(HttpContext.Request.Form["ddlActivity"].ToString())
                    });
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(HttpContext.Request.Form["MemberID"], MessageNode.SYS_MODULE_ACTIVITY_REPORT_ERROR, exception.ToString());
            }
            return RedirectToAction("ModuleActivityReport", "AcademicRecord", new { mid = HttpContext.Request.Form["MemberID"].ToString() });
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

        public IActionResult QualificationAcademicRecord(string mid, string qid, string sid = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.QualID = Int32.Parse(qid);
                    ViewBag.StudentGuid = sid;
                    ViewBag.ActivityID = 3;
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SYS_STUDENT_ACADEMIC_RECORD_ERROR, exception.ToString());
            }
            return View();
        }

        [HttpPost]
        [Obsolete]
        public FileResult AcademicDownLoad([FromForm] DownLoadModel downLoadModel)
        {
            string member_Guid = downLoadModel.AdminMemberID;
            byte[] bytes = null;
            try
            {
                if (!string.IsNullOrEmpty(downLoadModel.MemberID))
                {
                    LoginBusinessComponent businessComponent = new LoginBusinessComponent();
                    AdministratorBusinessComponent administratorBusinessComponent = new AdministratorBusinessComponent();
                    OnlineModuleActivityBusinessComponent onlineModuleActivityBusinessComponent = new OnlineModuleActivityBusinessComponent();
                    int MemberID = businessComponent.GetMemberIDByMemberGuid(downLoadModel.MemberID);
                    TemplateModel templateModel = administratorBusinessComponent.GetTemplateByTempleteID((int)ApplicationProgressStatus.ACADEMIC_RECORDS);
                    string html_file_template = templateModel.TemplateText;
                    var all_member_academics = onlineModuleActivityBusinessComponent.GetAcademicRecordByMemberID(downLoadModel.QualificationID, MemberID, downLoadModel.ActivityID);
                    var student_obj = onlineModuleActivityBusinessComponent.GetStudentDetailByMemberID(MemberID, downLoadModel.QualificationID);
                    html_file_template = html_file_template.StudentHeader(student_obj);
                    html_file_template = html_file_template.StudentAcademicRecords(all_member_academics);
                    StringReader sr = new StringReader(html_file_template);
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
                    return File(bytes, "application/pdf", string.Format("{0}.pdf", student_obj.StudentNumber));
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(member_Guid, MessageNode.SYS_ACADEMIC_RECORD_DOWN_LOAD_ERROR, exception.ToString());
            }
            return File(bytes, "", "");
        }

        [HttpPost]
        [Obsolete]
        public FileResult ReportDownload([FromForm] DownLoadModel model)
        {
            string member_Guid = model.AdminMemberID;
            byte[] bytes = null;
            try
            {
                OnlineModuleActivityBusinessComponent onlineModuleActivityBusinessComponent = new OnlineModuleActivityBusinessComponent();
                AdministratorBusinessComponent administratorBusinessComponent = new AdministratorBusinessComponent();
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                TemplateModel templateModel = administratorBusinessComponent.GetTemplateByTempleteID((int)ApplicationProgressStatus.MODULE_ACTIVITY_REPORT);
                string html_file_template = templateModel.TemplateText;
                var all_member_academics = moduleActivityBusinessComponent.GetAllMemberByModuleActivityID(model.ModuleID, model.ActivityID);
                //html_file_template = html_file_template.StudentHeader(student_obj);
                html_file_template = html_file_template.StudentAcademicRecords(all_member_academics);
                StringReader sr = new StringReader(html_file_template);
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
                return File(bytes, "application/pdf", string.Format("{0}.pdf", "ActivityReport"));
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(member_Guid, MessageNode.SYS_ACADEMIC_RECORD_DOWN_LOAD_ERROR, exception.ToString());
            }
            return File(bytes, "", "");
        }

        public IActionResult SearchByStudent(string MemberID, string StudentNo)
        {
            string student_id = string.Empty;
            try
            {
                OnlineModuleActivityBusinessComponent onlineModuleActivityBusinessComponent = new OnlineModuleActivityBusinessComponent();
                student_id = onlineModuleActivityBusinessComponent.GetStudentGuidByStudentNumber(StudentNo)?.ToString();
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(MemberID, MessageNode.SYS_SEARCH_STUDENT_ERROR, exception.ToString());
            }
            return RedirectToAction("AcademicRecord", "AcademicRecord", new { mid = MemberID, sid = student_id });
        }
    }
}
