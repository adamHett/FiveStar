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

    public partial class FiveStarApplicantInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FiveStarApplicantInformation()
        {
            this.FiveStarApplicantInterviewInformation = new HashSet<FiveStarApplicantInterviewInformation>();
            this.FiveStarResidentInformation = new HashSet<FiveStarResidentInformation>();
        }

        public List<AllApplicantProfiles> AllApplicantProfiles { get; set; }
        public List<IncompleteApplicantProfiles> IncompleteApplicantProfiles { get; set; }
        public string AppInfo_FullName
        {
            get { return AppInfo_Fname + " " + AppInfo_Lname; }
        }
        public int AppInfo_ID { get; set; }
        public string AppInfo_Status { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public System.DateTime AppInfo_Date { get; set; }
        public string AppInfo_Type { get; set; }
        [Required(ErrorMessage = "Your must provide a SSN")]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string AppInfo_SSN { get; set; }
        [Required(ErrorMessage = "Your must provide a First Name")]
        public string AppInfo_Fname { get; set; }
        [Required(ErrorMessage = "Your must provide a Last Name")]
        public string AppInfo_Lname { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_DOB { get; set; }
        public Nullable<int> AppInfo_Age { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid E-mail Address")]
        public string AppInfo_Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public string AppInfo_Phone { get; set; }
        public string AppInfo_Gender { get; set; }
        public string AppInfo_Need1 { get; set; }
        public string AppInfo_Need2 { get; set; }
        public string AppInfo_LTGoal { get; set; }
        public string AppInfo_ReferAgency { get; set; }
        public string AppInfo_ReferAgent { get; set; }
        public string AppInfo_NonRefer { get; set; }
        public string AppInfo_HousingStatus { get; set; }
        public string AppInfo_Facility { get; set; }
        public string AppInfo_StayLength { get; set; }
        public string AppInfo_EContFname { get; set; }
        public string AppInfo_EContLname { get; set; }
        public string AppInfo_EContRelation { get; set; }
        public string AppInfo_EContStAddress { get; set; }
        public string AppInfo_EContCity { get; set; }
        public string AppInfo_EContState { get; set; }
        public string AppInfo_EContZip { get; set; }
        public string AppInfo_EContPH { get; set; }
        public string AppInfo_MaritalStatus { get; set; }
        public Nullable<int> AppInfo_Children { get; set; }
        public Nullable<int> AppInfo_ChildrenMale { get; set; }
        public Nullable<int> AppInfo_ChildrenFem { get; set; }
        public string AppInfo_TrainLevel { get; set; }
        public string AppInfo_TrainSkills { get; set; }
        public Nullable<bool> AppInfo_MilServDD214 { get; set; }
        public string AppInfo_MilServBranch { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_MilServBeginDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_MilServEndDate { get; set; }
        public string AppInfo_MilServDischargeRnk { get; set; }
        public string AppInfo_MilServMOS { get; set; }
        public string AppInfo_MilServCombat { get; set; }
        public string AppInfo_MilServSepCode { get; set; }
        public string AppInfo_MilServPurpHrt { get; set; }
        public string AppInfo_MilServTOD { get; set; }
        public string AppInfo_MilServDischargeStatus { get; set; }
        public string AppInfo_MoIncServiceDisability { get; set; }
        public string AppInfo_MoIncSDPercent { get; set; }
        public string AppInfo_MoIncSDMoAmt { get; set; }
        public string AppInfo_MoIncCurrEmployed { get; set; }
        public string AppInfo_MoIncCEMoAmt { get; set; }
        public string AppInfo_MoIncSSI { get; set; }
        public string AppInfo_MoIncSSDI { get; set; }
        public string AppInfo_MoIncFoodStamps { get; set; }
        public string AppInfo_MoIncOther { get; set; }
        public string AppInfo_MoIncMoChildSupport { get; set; }
        public string AppInfo_MoIncTotalMoAmt { get; set; }
        public string AppInfo_EmpHistJobTitle { get; set; }
        public string AppInfo_EmpHistEmployer { get; set; }
        public string AppInfo_EmpHistCity { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_EmpHistJobStart { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_EmpHistJobEnd { get; set; }
        public string AppInfo_EmpHistMoWage { get; set; }
        public string AppInfo_EmpHistJobTitle2 { get; set; }
        public string AppInfo_EmpHistEmployer2 { get; set; }
        public string AppInfo_EmpHistCity2 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_EmpHistJobStart2 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_EmpHistJobEnd2 { get; set; }
        public string AppInfo_EmpHistMoWage2 { get; set; }
        public string AppInfo_EmpHistJobTitle3 { get; set; }
        public string AppInfo_EmpHistEmployer3 { get; set; }
        public string AppInfo_EmpHistCity3 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_EmpHistJobStart3 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AppInfo_EmpHistJobEnd3 { get; set; }
        public string AppInfo_EmpHistMoWage3 { get; set; }
        public string AppInfo_LegInfoMisdemeanor { get; set; }
        public string AppInfo_LegInfoFelony { get; set; }
        public string AppInfo_LegInfoState { get; set; }
        public string AppInfo_LegInfoCounty { get; set; }
        public string AppInfo_LegInfoCharge_Details { get; set; }
        public string AppInfo_LegInfoCharge_Timeframe { get; set; }
        public string AppInfo_LegInfoProbation_Status { get; set; }
        public string AppInfo_LegInfoProbation_Officer { get; set; }
        public string AppInfo_LegInfoVetTreatCourt_Status { get; set; }
        public string AppInfo_LegInfoVetTreatCourt_Phase { get; set; }
        public string AppInfo_LegInfoVetTreatCourt_GradStauts { get; set; }
        public string AppInfo_LegInfoLegal_List { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public string AppInfo_AppAgree_SignDate1 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public string AppInfo_AppAgree_SignDate2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FiveStarApplicantInterviewInformation> FiveStarApplicantInterviewInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FiveStarResidentInformation> FiveStarResidentInformation { get; set; }
    }
}
