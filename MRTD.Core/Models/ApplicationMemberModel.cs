
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MRTD.Core.Common;
using MRTD.Core.Encryption;

namespace MRTD.Core.Models
{
    public class ApplicationMemberModel : BaseModel 
    {
        private string _password;
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "E-mail Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Cell Number")]
        public string CellNo { get; set; }

        [Required]
        [Display(Name = "ID Number/Passport")]
        public string IDNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password {
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

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Telephone No (Work)")]
        public string WorkTelNo { get; set; }

        [Display(Name = "Telephone No (Home)")]
        public string HomeTelNo { get; set; }

        [Display(Name = "Physical Address")]
        public string PhysicalAddress { get; set; }

        [Display(Name = "Postal Address")]
        public string PostalAddress { get; set; }

        [Display(Name = "Same as physical address")]
        public bool IsSameAddress { get; set; }

        [Display(Name = "Home Language")]
        public int LanguageID { get; set; }
        
        [Display(Name = "Race")]
        public int RaceID { get; set; }

        [Display(Name = "Disability")]
        public string Disability { get; set; }

        [Required]
        [Display(Name = "Title")]
        public int TitleID { get; set; }

        [Display(Name = "Special Learning Requirement")]
        public string SpecialLearningRequirement { get; set; }

        [Display(Name = "Employer")]
        public string Employer { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Position/Job Title")]
        public string Occupation { get; set; }

        [Display(Name="Highest Qualification")]
        public int HighQualID { get; set; } 

        [Required]
        [Display(Name = "Nationality")]
        public int NationID { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderID { get; set; }

        [Required]
        [Display(Name = "Tipp Academy Program")]
        public int QualificationID { get; set; }

        [Required]
        [Display(Name = "Tipp Academy Qualification")]
        public int FacultyID { get; set; }

        [Display(Name = "Role")]
        public int RoleID { get; set; }

        public int MemberTypeID { get; set; }

        public string ClientID { get; set; }

        public SelectList Genders;

        public SelectList SchoolFaculties;

        public SelectList StaffRoles;

        public SelectList Languages;

        public SelectList Races;

        public SelectList Nations;

        public SelectList HighestQualifications;

        public SelectList Titles;
    }
}
