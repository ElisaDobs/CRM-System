using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MRTD.Core.Models;
using MRTD.Core.Common;

namespace BusinessSchoolMLS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string mid)
        {
            if (!string.IsNullOrEmpty(mid))
            {
                Session.AppSession.Set("MemberID", mid);
                ViewBag.AcademicYear = DateTime.Now.Year;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
