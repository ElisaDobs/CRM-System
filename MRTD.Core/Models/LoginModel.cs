using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Common;
using MRTD.Core.Encryption;

namespace MRTD.Core.Models
{
    public class LoginModel : BaseModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string EncryptedPassword {
            get
            {
                if (!string.IsNullOrEmpty(_encryptedPassword))
                    return TippAcademyEncryptionEngine.Decrypt(_encryptedPassword, Session.AppSession["ApplicationId"].ToString());

                return string.Empty;
            }
            set
            {
                _encryptedPassword = value;
            }
        }
        public string MemberID { get; set; }

        private string _encryptedPassword;
    }
}
