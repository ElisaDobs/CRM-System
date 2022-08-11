using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Common;
using MRTD.Core.Encryption;

namespace MRTD.Core.Models
{
    public class ResetPasswordModel : BaseModel
    {
        private string _password;
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        public string NewPassword {
            get
            {
                if (!string.IsNullOrEmpty(_password))
                {
                    return TippAcademyEncryptionEngine.Encrypt(_password, Session.AppSession["ApplicationId"].ToString());
                }
                return string.Empty;
            }
            set
            {
                _password = value;
            }
        }

        [Display(Name ="Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
