using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MRTD.Core.Models;
using BusinessSchoolMLS.Controls;
using BusinessSchoolMLS.SchoolBusinessComponent;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessSchoolMLS.Controllers
{
    public class OnlineQuestionnaireController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnlineQuestionnaire(string mid, int aid)
        {
            try
            {
                if(!string.IsNullOrEmpty(mid))
                {
                    ViewBag.MemGuid = mid;
                    ViewBag.ActivityID = aid;
                }
            }
            catch(Exception exception)
            {
                //Show error
            }

            return View();
        }

        public IActionResult ModuleActivityQuestion(ModuleActivityQuestionModel moduleActivityQuestionModel, string mid, int maid)
        {
            OnlineModuleActivityBusinessComponent onlineModuleActivityBusinessComponent = new OnlineModuleActivityBusinessComponent();
            if(!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.ModuleActivityID = maid;
                var all_module_questions = onlineModuleActivityBusinessComponent.GetModuleActivityQuestionByModuleActivityID(maid).Select(question => new SelectListItem() { Value = question.ActivityQuestionID.ToString(), Text = question.QuestionName });
                moduleActivityQuestionModel.QuestionParent = new SelectList(all_module_questions, "Value", "Text");
                var all_field_controls = onlineModuleActivityBusinessComponent.GetAllControlField().Select(control => new SelectListItem() { Value = control.FieldTypeID.ToString(), Text = control.FieldTypeName });
                moduleActivityQuestionModel.FielTypeName = new SelectList(all_field_controls, "Value", "Text");
            }
            return View(moduleActivityQuestionModel);
        }

        public IActionResult AddModuleActivityQuestion([FromForm]ModuleActivityQuestionModel moduleActivityQuestionModel)
        {
            OnlineModuleActivityBusinessComponent businessComponent = new OnlineModuleActivityBusinessComponent();
            if(businessComponent.InsertOnlineModuleActivityQuestion(moduleActivityQuestionModel))
            {
                return RedirectToAction("ModuleActivityQuestion", "OnlineQuestionnaire", new { mid = moduleActivityQuestionModel.MemberID, maid = moduleActivityQuestionModel.ModuleActivityID });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult QuestionLookUpAnswer(string mid, int qid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.QuestionID = qid;
            }

            return View();
        }


        public IActionResult SaveLookupAnswer()
        {
            int QuestionID = 0,
                ModuleActID = 0;
            LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
            OnlineModuleActivityBusinessComponent businessComponent = new OnlineModuleActivityBusinessComponent();
            string MemberID = string.Empty;
            foreach(var row in HttpContext.Request.Form.ToList())
            {
                if (row.Key.Equals("QuestionID"))
                    QuestionID = Convert.ToInt32(row.Value.ToString());
                else if (row.Key.Equals("MemberID"))
                    MemberID = row.Value.ToString();
                else if (row.Key.Equals("ModuleActivityID"))
                    ModuleActID = Convert.ToInt32(row.Value.ToString());
                else
                {
                    
                    int MemID = loginBusinessComponent.GetMemberIDByMemberGuid(MemberID);
                    string[] answers = HttpContext.Request.Form[row.Key].ToString().Split(',');
                    QuestionLookupAnswerModel questionLookupAnswerModel = new QuestionLookupAnswerModel()
                    {
                        QuestionID = QuestionID,
                        LookupAnswerName = answers[0],
                        IsCorrectAnswer = answers.Length > 2 ? true : false,
                        Score = Int32.Parse(answers[1]),
                        MemberID = MemID
                    };
                    businessComponent.InsertQuestionLookupAnswer(questionLookupAnswerModel);
                }
            }
            return RedirectToAction("ModuleActivityQuestion", "OnlineQuestionnaire", new { mid = MemberID, maid = ModuleActID});
        }


        public IActionResult AddActivityModuleQuestionAnswer([FromForm]ModuleActivityAnswerModel moduleActivityAnswerModel)
        {
            OnlineModuleActivityBusinessComponent businessComponent = new OnlineModuleActivityBusinessComponent();
            if (businessComponent.InsertOnlineModuleActivityAnswer(moduleActivityAnswerModel))
            {
                return RedirectToAction("", new { });
            }
            else
            {
                return RedirectToAction("", new { });
            }
        }

        public IActionResult SaveActivityMemberAnswer()
        {
            string mguid = string.Empty,
                   module_activity_id = string.Empty;
            if (HttpContext.Request.Method == "POST")
            {
                int mid = 0;
                OnlineModuleActivityBusinessComponent onlineModuleActivityBusinessComponent = new OnlineModuleActivityBusinessComponent();
                foreach (var row in HttpContext.Request.Form.ToList())
                {
                    if (row.Key == "mid")
                    {
                        LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                        mguid = row.Value;
                        mid = loginBusinessComponent.GetMemberIDByMemberGuid(mguid);
                    }
                    else if(row.Key == "aid")
                    {
                        module_activity_id = row.Value;
                    }
                    else
                    {
                        var single_question = onlineModuleActivityBusinessComponent.GetModuleQuestionByQuestionID(Int32.Parse(row.Key.ToString()));
                        if (single_question != null)
                        {
                            switch ((FieldType)Enum.Parse(typeof(FieldType), single_question.FieldTypeID.ToString()))
                            {
                                case FieldType.CHECKBOX:
                                case FieldType.DROPDOWN:
                                case FieldType.RADIOBUTTON:
                                    LookupAnswerModel lookupAnswerModel = new LookupAnswerModel() { QuestionID = Int32.Parse(row.Key.ToString()), LookupAnswerID = Int32.Parse(row.Value), MemberID = mid };
                                    onlineModuleActivityBusinessComponent.InsertQuestionLookupAnswer(lookupAnswerModel);
                                    break;
                                default:
                                    AlphanumericAnswerModel alphanumericAnswerModel = new AlphanumericAnswerModel() { ActivityQuestionID = Int32.Parse(row.Key.ToString()), AlphanumericAnswer = row.Value, MemberID = mid };
                                    onlineModuleActivityBusinessComponent.InsertQuestionAlphanumericAnswer(alphanumericAnswerModel);
                                    break;
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index", "DisplayScore", new { mid = mguid, aid= module_activity_id });
        }
    }
}
