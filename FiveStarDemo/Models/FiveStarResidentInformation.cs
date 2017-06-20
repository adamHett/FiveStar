//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FiveStarDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FiveStarResidentInformation
    {
        public List<AllResidentProfiles> AllResidentProfiles { get; set; }
        public int ResInfo_ID { get; set; }
        public string ResInfo_Status { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public System.DateTime ResInfo_AcceptDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ResInfo_DeclineDate { get; set; }
        public int ResInfo_RoomID { get; set; }
        public bool ResInfo_DD214 { get; set; }
        public bool ResInfo_TBTest { get; set; }
        public bool ResInfo_PhotoID { get; set; }
        public bool ResInfo_HUDVASH_Vouch { get; set; }
        public bool ResInfo_BackCheck { get; set; }
        public string ResInfo_CommRecomendation { get; set; }
        [Required(ErrorMessage = "You must provide a SSN")]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string ResInfo_SSN { get; set; }
        [Required(ErrorMessage = "You must provide a First Name")]
        public string ResInfo_Fname { get; set; }
        [Required(ErrorMessage = "You must provide a Last Name")]
        public string ResInfo_Lname { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ResInfo_DOB { get; set; }
        public Nullable<int> ResInfo_Age { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public string ResInfo_Phone { get; set; }
        public string ResInfo_Gender { get; set; }
        public int AppInfo_ID { get; set; }
        public Nullable<System.DateTime> ResInfo_ExitDate { get; set; }
        public string ResInfo_ExitSummary { get; set; }
        public Nullable<int> AppIntInfo_ID { get; set; }

        public virtual FiveStarApplicantInformation FiveStarApplicantInformation { get; set; }
        public virtual FiveStarApplicantInterviewInformation FiveStarApplicantInterviewInformation { get; set; }
    }
}