using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BusinessSchoolMLS.Controllers
{
    public class DisplaySuccessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}