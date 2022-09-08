using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MRTD.Core.Encryption;
using MRTD.Core.Notification;
using MRTD.Core.Models;

namespace MailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //string connectionString = @"Data Source=SAGJHBNB43ZVHM2\SQLExpress; Database=UniversityDB; Trusted_Connection=True; MultipleActiveResultSets=true";
            //string connectionString = "0MhVAX00wRhKFzB2iVuD9tMvj75qLfz1suN0azED6pI9uyvuQoXXnmFL2/EcRGQ4Qz8IIpidPpNwnpMBdtJVYpXWD6wXOz4WN99Km6c/wJjA2P/p6xYRKrkeXV5Ws7i2ecbbjXh8FQC8fqoSfv/QlzPD+gWiASTOC7LGp9hqzZa3hBRMofTDBuCcL5c5j1Qi";
            string connectionString = "FnaOtswN8pMKPDOeXySD5K3P7BbIE5lbWEAp6fc8VMH2dTTpkJdDUbF6E7TwoOe5FfrIFlTpzVMJVFyF5/QfNw3H+AG+bQx6YvXUxErdfYg1R19fdKv523WCW71h/pdyG1S4OvH1kyxw3D8OYLrtloXK5sj75ayfI25OYHmD4kbEdZ4ukY6xK/Qh93MPQhY1";
            //string emailPassword = "Hw5HQneVwhjg1YhdngEJ+oBo7TZJGydZfa6mZHNWmJU=";
            string emailPassword = "P@ssw0rd123";
            //Data Source=SAGJHBNB43ZVHM2\\SQLExpress; Database=UniversityDB; Trusted_Connection=True; MultipleActiveResultSets=true
            //connectionString = TippAcademyEncryptionEngine.Decrypt(connectionString, "F5439EB1-B6B4-42A0-A28B-9B55FD447DA8");
            //connectionString = TippAcademyEncryptionEngine.Decrypt(connectionString, "A7C2A343-3715-483C-BAA5-A22346114A80");
            //connectionString = TippAcademyEncryptionEngine.Encrypt(connectionString, "A7C2A343-3715-483C-BAA5-A22346114A80");
            emailPassword = TippAcademyEncryptionEngine.Encrypt(emailPassword, "F5439EB1-B6B4-42A0-A28B-9B55FD447DA8");
        }
    }
}
