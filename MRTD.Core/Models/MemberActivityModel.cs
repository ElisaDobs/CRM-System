using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MRTD.Core.Models
{
    [XmlRoot("MemberActivityModel")]
    public class MemberActivityModel
    {
        [XmlElement("ActivityDate")]
        public string ActivityDate { get; set; }
        [XmlElement("ActivityDuration")]
        public string ActivityDuration { get; set; }
        [XmlElement("IsOnline")]
        public bool IsOnline { get; set; }
        [XmlElement("ModuleName")]
        public string ModuleName { get; set; }
        [XmlElement("ActivityName")]
        public string ActivityName { get; set; }
        [XmlElement("FullName")]
        public string FullName { get; set; }
        [XmlElement("EmailAddress")]
        public string EmailAddress { get; set; }
        [XmlElement("EmailBody")]
        public string EmailBody { get; set; }
        [XmlElement("EmailSubject")]
        public string EmailSubject { get; set; }
        [XmlElement("EmailAttachment")]
        public string EmailAttachment { get; set; }
    }
}
