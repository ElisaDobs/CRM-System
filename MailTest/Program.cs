using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MRTD.Core.Encryption;

namespace MailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=SAGJHBNB43ZVHM2\SQLExpress; Database=UniversityDB; Trusted_Connection=True; MultipleActiveResultSets=true";

            //string emailPassword = "Hw5HQneVwhjg1YhdngEJ+oBo7TZJGydZfa6mZHNWmJU=";
            string emailPassword = "married&*1";

            connectionString = TippAcademyEncryptionEngine.Encrypt(connectionString, "F5439EB1-B6B4-42A0-A28B-9B55FD447DA8");

            emailPassword = TippAcademyEncryptionEngine.Encrypt(emailPassword, "A7C2A343-3715-483C-BAA5-A22346114A80");
        }
    }
}
