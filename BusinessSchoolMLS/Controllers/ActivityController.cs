using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using MRTD.Core.Models;
using MRTD.Core.Common;
using BusinessSchoolMLS.SchoolBusinessComponent;

namespace BusinessSchoolMLS.Controllers
{
    public class ActivityController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Activity(ModuleActivityModel moduleActivityModel)
        {
            try
            {
                int qid = Convert.ToInt32(HttpContext.Request.Query["fid"]);
                ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
                FacultyBusinessComponent facultyBusinessComponent = new FacultyBusinessComponent();
                var lst_all_activities = moduleActivityBusinessComponent.GetAllActivity()
                                         ?.Select(act => new SelectListItem() { Value = act.ActivityID.ToString(), Text = act.ActivityName });
                var lst_all_modules = facultyBusinessComponent.GetAllFacultyModuleByQualificationID(qid)
                                         ?.Select(mod => new SelectListItem { Value = mod.ModuleID.ToString(), Text = mod.ModuleName });
                moduleActivityModel.ActivitList = new SelectList(lst_all_activities, "Value", "Text");
                moduleActivityModel.ModuleList = new SelectList(lst_all_modules, "Value", "Text");
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(moduleActivityModel.MemberID, MessageNode.SYS_MODULE_ACTIVITY_LOAD_ERROR, exception.ToString());
                return RedirectToAction("Index", "DisplayError");
            }
            return View(moduleActivityModel);
        }

        public IActionResult AddModuleActivity([FromForm]ModuleActivityModel moduleActivityModel)
        {
            ModuleActivityBusinessComponent moduleActivityBusinessComponent = new ModuleActivityBusinessComponent();
            try
            {
                if (moduleActivityBusinessComponent.SaveModuleActivity(moduleActivityModel))
                {
                    return RedirectToAction("Activity", "Activity", new { });
                }
                return RedirectToAction("Login", "Login");
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(moduleActivityModel.MemberID, MessageNode.SYS_MODULE_ACTIVITY_CREATE_ERROR, exception.ToString());
                return RedirectToAction("Index", "DisplayError");
            }
        }

        public IActionResult ActivityTimeTable(string mid, int actid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                ViewBag.MemGuid = mid;
                ViewBag.ActivityID = actid;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
