using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using MRTD.Core.Models; //Mavhayisi Ready Tsaleni Dobson Library
using MRTD.Core.Common; //Mavhayisi Ready Tsaleni Dobson Library
using BusinessSchoolMLS.SchoolBusinessComponent;
using MRTD.Core.Upload;

namespace BusinessSchoolMLS.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Application(ApplicationMemberModel applicationMemberModel)
        {
            ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
            FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
            try
            {
                var lst_all_gender = applicationBusinessComponent.GetAllGender().Select(sex => new SelectListItem { Value = sex.GenderID.ToString(), Text = sex.GenderName });
                var lst_all_faculties = facultyBusinessComponent.GetAllBusinessFaculty(Session.AppSession["ClientId"].ToString()).Select(fac => new SelectListItem { Value = fac.FacultyID.ToString(), Text = fac.FacultyName });
                var lst_all_titles = applicationBusinessComponent.GetAllTitles().Select(title => new SelectListItem { Value = title.TitleID.ToString(), Text = title.TitleName });
                var lst_all_languages = applicationBusinessComponent.GetLanguages().Select(lang => new SelectListItem { Value = lang.LanguageID.ToString(), Text = lang.LanguageName });
                var lst_all_races = applicationBusinessComponent.GetAllRaces().Select(race => new SelectListItem { Value = race.RaceID.ToString(), Text = race.Race });
                var lst_all_nationalities = applicationBusinessComponent.GetAllNationalities().Select(nation => new SelectListItem { Value = nation.NationID.ToString(), Text = nation.NationName });
                var lst_all_qualifications = applicationBusinessComponent.GetHighestQualModels().Select(qual => new SelectListItem { Value = qual.HighQualID.ToString(), Text = qual.HighQualName });
                #region "Adding empty option"
                var firstIndex = lst_all_faculties.ToList();
                firstIndex.Add(new SelectListItem() { Value = "0", Text = ""});
                #endregion
                applicationMemberModel.Genders = new SelectList(lst_all_gender, "Value", "Text");
                applicationMemberModel.SchoolFaculties = new SelectList(firstIndex.OrderBy(a => a.Value), "Value", "Text");
                applicationMemberModel.Titles = new SelectList(lst_all_titles, "Value", "Text");
                applicationMemberModel.Languages = new SelectList(lst_all_languages, "Value", "Text");
                applicationMemberModel.Races = new SelectList(lst_all_races, "Value", "Text");
                applicationMemberModel.Nations = new SelectList(lst_all_nationalities, "Value", "Text");
                applicationMemberModel.HighestQualifications = new SelectList(lst_all_qualifications, "Value", "Text");
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(Guid.NewGuid().ToString(), MessageNode.SYS_APPLICATION_ERROR, exception.ToString());
            }
            return View(applicationMemberModel);
        }

        public IActionResult SubmitApplication([FromForm]ApplicationMemberModel applicationMemberModel)
        {
            string mguid = string.Empty;
            try
            {
                if (Session.AppSession.ContainsKey("MemberID"))
                {
                    mguid = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                }
                ApplicationBusinessComponent adminBusinessComponent = new ApplicationBusinessComponent();
                var resultObject = adminBusinessComponent.SaveApplication(applicationMemberModel);
                if (!resultObject.Exist)
                {
                    mguid = resultObject.MemberID.ToString();
                    NotificationBusinessComponent notificationBusinessComponent = new NotificationBusinessComponent();
                    notificationBusinessComponent.InsertSystemNotification(
                                                                                  (int)ApplicationProgressStatus.REQUIRED_DOCUMENT,
                                                                                  Guid.Parse(resultObject?.MemberID.ToString()),
                                                                                  Session.AppSession["ApplicationId"].ToString()
                                                                              );
                }
                else
                {
                    if (!Session.AppSession.ContainsKey("ApplicationAlreadyExist"))
                    {
                        Session.AppSession.Set("ApplicationAlreadyExist", string.Format("Please ID No {0} already exist on Tipp Academy database!", applicationMemberModel.IDNo));
                    }
                    return RedirectToAction("Application", "Application");
                }
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mguid, MessageNode.SYS_REQUIRED_DOCUMENT_ERROR, exception.ToString());
            }
            return RedirectToAction("RequiredDocumentUpload", "Application", new { mid = mguid, qid = applicationMemberModel.QualificationID });
        }

        public IActionResult UploadAcceptanceLetter(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    Session.AppSession.Set("MemberID", mid);
                }
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SIGNED_LETTER_CODE_ERROR, exception.ToString());
            }
            return View();
        }

        public IActionResult UploadPayment(string mid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    Session.AppSession.Set("MemberID", mid);
                }
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SYS_UPLOAD_PAYMENT_ERROR, exception.ToString());
            }
            return View();
        }

        public async Task<IActionResult> SaveSignedAcceptenceLetter([FromForm]SignedAcceptenceLetterModel signedAcceptenceLetterModel)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                    ApplicantRequiredDocument applicantRequiredDocument = new ApplicantRequiredDocument();
                    LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                    await signedAcceptenceLetterModel.SignedLetter.CopyToAsync(memoryStream);
                    applicantRequiredDocument.UploadTypeID = signedAcceptenceLetterModel.UploadTypeID;
                    applicantRequiredDocument.MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(signedAcceptenceLetterModel.MemberID);
                    applicantRequiredDocument.UploadFileName = signedAcceptenceLetterModel.SignedLetter.FileName;
                    var memberDetails = loginBusinessComponent.GetLoginDetailByMemberGuid(signedAcceptenceLetterModel.MemberID);
                    string filePath = UploadBusinessComponent.SaveDocument(Session.AppSession["Uploads"].ToString(), memberDetails.IDNo, signedAcceptenceLetterModel.UploadTypeID.ToString(), signedAcceptenceLetterModel.SignedLetter.FileName, memoryStream.ToArray());
                    applicantRequiredDocument.UploadPath = filePath;
                    applicationBusinessComponent.InsertApplicationUpload(applicantRequiredDocument);
                }
                catch(Exception exception)
                {
                    string MemberID = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                    LogMessageBusinessComponent.InsertLogMessage(MemberID, MessageNode.SIGNED_LETTER_CODE_ERROR, exception.ToString());
                }
                return RedirectToAction("ApplicationSuccess", "Application", new { mid = signedAcceptenceLetterModel.MemberID });
            }
        }

        public async Task<IActionResult> SavePayment([FromForm]ProofPaymentModel proofPaymentModel)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                    ApplicantRequiredDocument applicantRequiredDocument = new ApplicantRequiredDocument();
                    LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                    await proofPaymentModel.ProofOfPayemnt.CopyToAsync(memoryStream);
                    applicantRequiredDocument.UploadTypeID = proofPaymentModel.UploadTypeID;
                    applicantRequiredDocument.MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(proofPaymentModel.MemberID);
                    applicantRequiredDocument.UploadFileName = proofPaymentModel.ProofOfPayemnt.FileName;
                    var memberDetails = loginBusinessComponent.GetLoginDetailByMemberGuid(proofPaymentModel.MemberID);
                    string filePath = UploadBusinessComponent.SaveDocument(Session.AppSession["Uploads"].ToString(), memberDetails.IDNo, 
                        proofPaymentModel.UploadTypeID.ToString(), proofPaymentModel.ProofOfPayemnt.FileName, memoryStream.ToArray());
                    applicantRequiredDocument.UploadPath = filePath;
                    applicationBusinessComponent.InsertApplicationUpload(applicantRequiredDocument);
                }
                catch(Exception exception)
                {
                    string MemberID = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                    LogMessageBusinessComponent.InsertLogMessage(MemberID, MessageNode.SYS_UPLOAD_PAYMENT_ERROR, exception.ToString());
                }
            }
            return RedirectToAction("ApplicationSuccess", "Application", new { mid = proofPaymentModel.MemberID });
        }

        public IActionResult RequiredDocumentUpload(string mid, int qid)
        {
            try
            {
                if (!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.QualId = qid;
                    Session.AppSession.Set("MemberID", mid);
                }
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(mid, MessageNode.SYS_REQUIRED_DOCUMENT_ERROR, exception.ToString());
            }
            return View();
        }

        public async Task<IActionResult> SaveRequiredUpload([FromForm]RequiredDocumentModel requiredDocumentModel)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    ApplicationBusinessComponent applicationBusinessComponent = new ApplicationBusinessComponent();
                    ApplicantRequiredDocument applicantRequiredDocument = new ApplicantRequiredDocument();
                    LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                    await requiredDocumentModel.IDDocument.CopyToAsync(memoryStream);
                    applicantRequiredDocument.MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(requiredDocumentModel.MemberID);
                    applicantRequiredDocument.UploadTypeID = 1;
                    applicantRequiredDocument.UploadFileName = requiredDocumentModel.IDDocument.FileName;
                    var memberDetails = loginBusinessComponent.GetLoginDetailByMemberGuid(requiredDocumentModel.MemberID);
                    applicantRequiredDocument.UploadPath = UploadBusinessComponent.SaveDocument(Session.AppSession["Uploads"].ToString(), memberDetails.IDNo, applicantRequiredDocument.UploadTypeID.ToString(), requiredDocumentModel.IDDocument.FileName, memoryStream.ToArray()); ;
                    applicationBusinessComponent.InsertApplicationUpload(applicantRequiredDocument);
                    await requiredDocumentModel.MatricCertificate.CopyToAsync(memoryStream);
                    applicantRequiredDocument.UploadTypeID = 2;
                    applicantRequiredDocument.UploadFileName = requiredDocumentModel.MatricCertificate.FileName;
                    applicantRequiredDocument.UploadPath = UploadBusinessComponent.SaveDocument(Session.AppSession["Uploads"].ToString(), memberDetails.IDNo, applicantRequiredDocument.UploadTypeID.ToString(), requiredDocumentModel.IDDocument.FileName, memoryStream.ToArray()); ;
                    applicationBusinessComponent.InsertApplicationUpload(applicantRequiredDocument);
                }
                catch(Exception exception)
                {
                    string MemberID = Session.AppSession["MemberID"].ToString();
                    Session.AppSession.Remove("MemberID");
                    LogMessageBusinessComponent.InsertLogMessage(MemberID, MessageNode.SYS_REQUIRED_DOCUMENT_ERROR, exception.ToString());
                }
            }
            return RedirectToAction("ApplicationSuccess", "Application", new { mid= requiredDocumentModel.MemberID });
        }

        public IActionResult ApplicationSuccess(string mid)
        {
            if(!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
            }

            return View();
        }

        [HttpGet]
        public string GetQualificationByFaculty(int FacultyID)
        {
            string lst_all_faculty_qual = string.Empty;
            try
            {
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                lst_all_faculty_qual = JsonConvert.SerializeObject(facultyBusinessComponent.GetAllFacultyQualificationByFacultyID(FacultyID));
                
            }
            catch(Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_APPLICATION_ERROR, exception.ToString());
            }
            return lst_all_faculty_qual;
        }
    }
}
