using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MRTD.Core.Models;
using MRTD.Core.Common;
using BusinessSchoolMLS.SchoolBusinessComponent;

namespace BusinessSchoolMLS.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            if (Session.AppSession.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = (string)Session.AppSession["ErrorMessage"];
                Session.AppSession.Remove("ErrorMessage");
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            if (Session.AppSession.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = (string)Session.AppSession["ErrorMessage"];
                Session.AppSession.Remove("ErrorMessage");
            }
            return View();
        }

        public IActionResult SubmitForgotPassword([FromForm]ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                string returned_password = loginBusinessComponent.ForgotPassword(forgotPasswordModel);
                if (!string.IsNullOrEmpty(returned_password))
                {
                    return RedirectToAction("Login", "Login", new object { });
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_FORGOT_PASSWORD_ERROR, exception.ToString());
            }
            Session.AppSession.Set("ErrorMessage", "Please provide the correct e-mail to retrive the password!");
            return RedirectToAction("ForgotPassword", "Login", new object { });
        }

        public IActionResult MemberLogin([FromForm]LoginModel loginModel)
        {
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                LoginModel login = loginBusinessComponent.ValidLogin(loginModel);
                if (loginModel.Password == login.EncryptedPassword)
                {
                    return RedirectToAction("Index", "Home", new { mid = login.MemberID });
                }
                else
                {
                    Session.AppSession.Set("ErrorMessage", "Invalid username and password, please provide the corrent login details!");
                }
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_LOGIN_ERROR, exception.ToString());
            }
            return RedirectToAction("Login", "Login", new object { });
        }

        public IActionResult LogOut(string mid)
        {
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                int MemberID = loginBusinessComponent.GetMemberIDByMemberGuid(mid);
                loginBusinessComponent.UpdateLogout(MemberID);
                return RedirectToAction("Login", "Login");
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_LOGOUT_ERROR, exception.ToString());
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult ResetPassword([FromForm]ResetPasswordModel resetPasswordModel)
        {
            try
            {
                LoginBusinessComponent loginBusinessComponent = new LoginBusinessComponent();
                loginBusinessComponent.ResetPassword(resetPasswordModel);
            }
            catch (Exception exception)
            {
                LogMessageBusinessComponent.InsertLogMessage(Session.AppSession["ApplicationId"].ToString(), MessageNode.SYS_RESET_PASSWORD_ERROR, exception.ToString());
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
