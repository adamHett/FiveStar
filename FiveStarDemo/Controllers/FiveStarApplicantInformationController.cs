using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.SqlClient;
using FiveStarDemo.Models;

namespace FiveStarDemo.Controllers
{
    public class FiveStarApplicantInformationController : Controller
    {
        private readonly DataLayer dl;

        public FiveStarApplicantInformationController()
        {
            dl = new DataLayer();
        }

        public FiveStarDBEntities db = new FiveStarDBEntities();

        // GET: FiveStarApplicantInformation - Generic list of applicants in DB
        public ActionResult Index()
        {
            return View(db.FiveStarApplicantInformation.ToList());
        }

        // GET: View returns all FiveStarApplicantInformation profiles which are incomplete
        public ActionResult FrontDeskPortal_Incomplete()
        {
            return View();
        }

        // JSON for getting data for FrontDeskPortal view
        public JsonResult GetIncompleteApplicantProfiles()
        {
            FiveStarApplicantInformation FSAIM = new FiveStarApplicantInformation();
            FSAIM.IncompleteApplicantProfiles = dl.GetIncompleteFiveStarApplicantProfiles();

            return Json(FSAIM, JsonRequestBehavior.AllowGet);
        }


        // GET: View returns all FiveStarApplicantInformation profiles
        public ActionResult FrontDeskPortal_All()
        {
            return View();
        }

        // JSON for getting data for MentalHealthPortal view
        public JsonResult GetAllApplicantProfiles()
        {
            FiveStarApplicantInformation FSAIM = new FiveStarApplicantInformation();
            FSAIM.AllApplicantProfiles = dl.GetAllFiveStarApplicantProfiles();

            return Json(FSAIM, JsonRequestBehavior.AllowGet);
        }

        // GET: View for returning data on a specific applicant's profile
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarApplicantInformation fiveStarApplicantInformation = db.FiveStarApplicantInformation.Find(id);
            if (fiveStarApplicantInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarApplicantInformation);
        }

        // GET: FiveStarApplicantInformation/Create
        public ActionResult Create()
        {
            var appStatuslist = new List<SelectListItem>();
            appStatuslist.Add(new SelectListItem() { Text = "Incomplete Application", Value = "Incomplete Application" });
            appStatuslist.Add(new SelectListItem() { Text = "Incomplete Application - Pending Missing Documents", Value = "Incomplete Application - Pending Missing Documents" });
            appStatuslist.Add(new SelectListItem() { Text = "Complete Application", Value = "Complete Application" });
            appStatuslist.Add(new SelectListItem() { Text = "Complete Application - Pending Interview", Value = "Complete Application - Pending Interview" });
            appStatuslist.Add(new SelectListItem() { Text = "Complete Application - Pending Final Decision", Value = "Complete Application - Pending Final Decision" });

            ViewBag.AppStatus = appStatuslist;

            var appTypelist = new List<SelectListItem>();
            appTypelist.Add(new SelectListItem() { Text = "Walk-in", Value = "Walk-in" });
            appTypelist.Add(new SelectListItem() { Text = "Referral", Value = "Referral" });
            appTypelist.Add(new SelectListItem() { Text = "Re-entry", Value = "Re-entry" });

            ViewBag.AppType = appTypelist;

            var noResidencelist = new List<SelectListItem>();
            noResidencelist.Add(new SelectListItem() { Text = "< 3 months", Value = "less than 3 months" });
            noResidencelist.Add(new SelectListItem() { Text = "3-6 mo.", Value = "3 to 6 months" });
            noResidencelist.Add(new SelectListItem() { Text = "6 mo. - 1 year", Value = "6 months to 1 year" });
            noResidencelist.Add(new SelectListItem() { Text = "1-3 years", Value = "1 to 3 years" });
            noResidencelist.Add(new SelectListItem() { Text = "> 3 years", Value = "greater than 3 years" });

            ViewBag.NoResidence = noResidencelist;

            var maritalStatus = new List<SelectListItem>();
            maritalStatus.Add(new SelectListItem() { Text = "Single", Value = "Single" });
            maritalStatus.Add(new SelectListItem() { Text = "Married", Value = "Married" });
            maritalStatus.Add(new SelectListItem() { Text = "Living Together", Value = "Living Together" });
            maritalStatus.Add(new SelectListItem() { Text = "Separated", Value = "Separated" });
            maritalStatus.Add(new SelectListItem() { Text = "Divorced", Value = "Divorced" });
            maritalStatus.Add(new SelectListItem() { Text = "Widowed", Value = "Widowed" });

            ViewBag.MaritalStatus = maritalStatus;

            var highestEdlevel = new List<SelectListItem>();
            highestEdlevel.Add(new SelectListItem() { Text = "HS Diploma or GED", Value = "HS Diploma or GED" });
            highestEdlevel.Add(new SelectListItem() { Text = "Associate's", Value = "Associate's" });
            highestEdlevel.Add(new SelectListItem() { Text = "Bachelor's", Value = "Bachelor's" });
            highestEdlevel.Add(new SelectListItem() { Text = "Master's", Value = "Master's" });

            ViewBag.EdLevel = highestEdlevel;

            return View();
        }

        // POST: FiveStarApplicantInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppInfo_ID,AppInfo_Status,AppInfo_Date,AppInfo_Type,AppInfo_SSN,AppInfo_Fname,AppInfo_Lname,AppInfo_DOB,AppInfo_Age,AppInfo_Email,AppInfo_Phone,AppInfo_Gender,AppInfo_Need1,AppInfo_Need2,AppInfo_LTGoal,AppInfo_ReferAgency,AppInfo_ReferAgent,AppInfo_NonRefer,AppInfo_HousingStatus,AppInfo_Facility,AppInfo_StayLength,AppInfo_EContFname,AppInfo_EContLname,AppInfo_EContRelation,AppInfo_EContStAddress,AppInfo_EContCity,AppInfo_EContState,AppInfo_EContZip,AppInfo_EContPH,AppInfo_MaritalStatus,AppInfo_Children,AppInfo_ChildrenMale,AppInfo_ChildrenFem,AppInfo_TrainLevel,AppInfo_TrainSkills,AppInfo_MilServDD214,AppInfo_MilServBranch,AppInfo_MilServBeginDate,AppInfo_MilServEndDate,AppInfo_MilServDischargeRnk,AppInfo_MilServMOS,AppInfo_MilServCombat,AppInfo_MilServSepCode,AppInfo_MilServPurpHrt,AppInfo_MilServTOD,AppInfo_MilServDischargeStatus,AppInfo_MoIncServiceDisability,AppInfo_MoIncSDPercent,AppInfo_MoIncSDMoAmt,AppInfo_MoIncCurrEmployed,AppInfo_MoIncCEMoAmt,AppInfo_MoIncSSI,AppInfo_MoIncSSDI,AppInfo_MoIncFoodStamps,AppInfo_MoIncOther,AppInfo_MoIncMoChildSupport,AppInfo_MoIncTotalMoAmt,AppInfo_EmpHistJobTitle,AppInfo_EmpHistEmployer,AppInfo_EmpHistCity,AppInfo_EmpHistJobStart,AppInfo_EmpHistJobEnd,AppInfo_EmpHistMoWage,AppInfo_EmpHistJobTitle2,AppInfo_EmpHistEmployer2,AppInfo_EmpHistCity2,AppInfo_EmpHistJobStart2,AppInfo_EmpHistJobEnd2,AppInfo_EmpHistMoWage2,AppInfo_EmpHistJobTitle3,AppInfo_EmpHistEmployer3,AppInfo_EmpHistCity3,AppInfo_EmpHistJobStart3,AppInfo_EmpHistJobEnd3,AppInfo_EmpHistMoWage3,AppInfo_LegInfoMisdemeanor,AppInfo_LegInfoFelony,AppInfo_LegInfoState,AppInfo_LegInfoCounty,AppInfo_LegInfoCharge_Details,AppInfo_LegInfoCharge_Timeframe,AppInfo_LegInfoProbation_Status,AppInfo_LegInfoProbation_Officer,AppInfo_LegInfoVetTreatCourt_Status,AppInfo_LegInfoVetTreatCourt_Phase,AppInfo_LegInfoVetTreatCourt_GradStauts,AppInfo_LegInfoLegal_List,AppInfo_AppAgree_SignDate1,AppInfo_AppAgree_SignDate2,AppIntInfo_Interview_ID,AppIntInfo_Date,AppIntInfo_Status,AppIntInfo_SSN,AppIntInfo_Fname,AppIntInfo_Lname,AppIntInfo_DOB,AppIntInfo_Age,AppIntInfo_MHCurrentHarm,AppIntInfo_MHPastHarm,AppIntInfo_MHPastHarm_Details,AppIntInfo_MHTBI,AppIntInfo_MHPTS,AppIntInfo_MHSubAbuse,AppIntInfo_MHBipoDisorder,AppIntInfo_MHSchizo,AppIntInfo_MHDepression,AppIntInfo_MHAnxiety,AppIntInfo_MHPersDisorder,AppIntInfo_MHOther,AppIntInfo_ASAAlcohol,AppIntInfo_ASAGambling,AppIntInfo_ASAPrescriptDrug,AppIntInfo_ASAStreetDrug,AppIntInfo_ASATobacco,AppIntInfo_ASADrugChoice1,AppIntInfo_ASADrugChoice2,AppIntInfo_ASADrugChoice3,AppIntInfo_ASACurrent_TimeSober,AppIntInfo_ASAPast_TimeSober,AppIntInfo_ASADUI_DWI,AppIntInfo_ASAPossess_Conviction,AppIntInfo_ASASell_Conviction,AppIntInfo_PHPCP_Fname,AppIntInfo_PHPCP_Lname,AppIntInfo_PHPCP_Phone,AppIntInfo_PHPCP_StAddress,AppIntInfo_PHPCP_City,AppIntInfo_PHPCP_State,AppIntInfo_PHPCP_Zip,AppIntInfo_PHPCP_ContactOK,AppIntInfo_PHSeizures,AppIntInfo_PHAllergies,AppIntInfo_PHOther,AppIntInfo_MedsCondition1,AppIntInfo_MedsMedication1,AppIntInfo_MedsDosage1,AppIntInfo_MedsFrequency1,AppIntInfo_MedsCondition2,AppIntInfo_MedsMedication2,AppIntInfo_MedsDosage2,AppIntInfo_MedsFrequency2,AppIntInfo_MedsCondition3,AppIntInfo_MedsMedication3,AppIntInfo_MedsDosage3,AppIntInfo_MedsFrequency3,AppIntInfo_MedsCondition4,AppIntInfo_MedsMedication4,AppIntInfo_MedsDosage4,AppIntInfo_MedsFrequency4,AppIntInfo_MedsCondition5,AppIntInfo_MedsMedication5,AppIntInfo_MedsDosage5,AppIntInfo_MedsFrequency5,AppIntInfo_AgreeSignDate")] FiveStarApplicantInformation fiveStarApplicantInformation)
        {
            if (ModelState.IsValid)
            {
                db.FiveStarApplicantInformation.Add(fiveStarApplicantInformation);
                db.SaveChanges();
                return RedirectToAction("FrontDeskPortal_All");
            }

            return View(fiveStarApplicantInformation);
        }

        // GET: FiveStarApplicantInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            var appStatuslist = new List<SelectListItem>();
            appStatuslist.Add(new SelectListItem() { Text = "Incomplete Application", Value = "Incomplete Application" });
            appStatuslist.Add(new SelectListItem() { Text = "Incomplete Application - Pending Missing Documents", Value = "Incomplete Application - Pending Missing Documents" });
            appStatuslist.Add(new SelectListItem() { Text = "Complete Application", Value = "Complete Application" });
            appStatuslist.Add(new SelectListItem() { Text = "Complete Application - Pending Interview", Value = "Complete Application - Pending Interview" });
            appStatuslist.Add(new SelectListItem() { Text = "Complete Application - Pending Final Decision", Value = "Complete Application - Pending Final Decision" });

            ViewBag.AppStatus = appStatuslist;

            var appTypelist = new List<SelectListItem>();
            appTypelist.Add(new SelectListItem() { Text = "Walk-in", Value = "Walk-in" });
            appTypelist.Add(new SelectListItem() { Text = "Referral", Value = "Referral" });
            appTypelist.Add(new SelectListItem() { Text = "Re-entry", Value = "Re-entry" });

            ViewBag.AppType = appTypelist;

            var noResidencelist = new List<SelectListItem>();
            noResidencelist.Add(new SelectListItem() { Text = "< 3 months", Value = "less than 3 months" });
            noResidencelist.Add(new SelectListItem() { Text = "3-6 mo.", Value = "3 to 6 months" });
            noResidencelist.Add(new SelectListItem() { Text = "6 mo. - 1 year", Value = "6 months to 1 year" });
            noResidencelist.Add(new SelectListItem() { Text = "1-3 years", Value = "1 to 3 years" });
            noResidencelist.Add(new SelectListItem() { Text = "> 3 years", Value = "greater than 3 years" });

            ViewBag.NoResidence = noResidencelist;

            var maritalStatus = new List<SelectListItem>();
            maritalStatus.Add(new SelectListItem() { Text = "Single", Value = "Single" });
            maritalStatus.Add(new SelectListItem() { Text = "Married", Value = "Married" });
            maritalStatus.Add(new SelectListItem() { Text = "Living Together", Value = "Living Together" });
            maritalStatus.Add(new SelectListItem() { Text = "Separated", Value = "Separated" });
            maritalStatus.Add(new SelectListItem() { Text = "Divorced", Value = "Divorced" });
            maritalStatus.Add(new SelectListItem() { Text = "Widowed", Value = "Widowed" });

            ViewBag.MaritalStatus = maritalStatus;

            var highestEdlevel = new List<SelectListItem>();
            highestEdlevel.Add(new SelectListItem() { Text = "HS Diploma or GED", Value = "HS Diploma or GED" });
            highestEdlevel.Add(new SelectListItem() { Text = "Associate's", Value = "Associate's" });
            highestEdlevel.Add(new SelectListItem() { Text = "Bachelor's", Value = "Bachelor's" });
            highestEdlevel.Add(new SelectListItem() { Text = "Master's", Value = "Master's" });

            ViewBag.EdLevel = highestEdlevel;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarApplicantInformation fiveStarApplicantInformation = db.FiveStarApplicantInformation.Find(id);
            if (fiveStarApplicantInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarApplicantInformation);
        }

        // POST: FiveStarApplicantInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppInfo_ID,AppInfo_Status,AppInfo_Date,AppInfo_Type,AppInfo_SSN,AppInfo_Fname,AppInfo_Lname,AppInfo_DOB,AppInfo_Age,AppInfo_Email,AppInfo_Phone,AppInfo_Gender,AppInfo_Need1,AppInfo_Need2,AppInfo_LTGoal,AppInfo_ReferAgency,AppInfo_ReferAgent,AppInfo_NonRefer,AppInfo_HousingStatus,AppInfo_Facility,AppInfo_StayLength,AppInfo_EContFname,AppInfo_EContLname,AppInfo_EContRelation,AppInfo_EContStAddress,AppInfo_EContCity,AppInfo_EContState,AppInfo_EContZip,AppInfo_EContPH,AppInfo_MaritalStatus,AppInfo_Children,AppInfo_ChildrenMale,AppInfo_ChildrenFem,AppInfo_TrainLevel,AppInfo_TrainSkills,AppInfo_MilServDD214,AppInfo_MilServBranch,AppInfo_MilServBeginDate,AppInfo_MilServEndDate,AppInfo_MilServDischargeRnk,AppInfo_MilServMOS,AppInfo_MilServCombat,AppInfo_MilServSepCode,AppInfo_MilServPurpHrt,AppInfo_MilServTOD,AppInfo_MilServDischargeStatus,AppInfo_MoIncServiceDisability,AppInfo_MoIncSDPercent,AppInfo_MoIncSDMoAmt,AppInfo_MoIncCurrEmployed,AppInfo_MoIncCEMoAmt,AppInfo_MoIncSSI,AppInfo_MoIncSSDI,AppInfo_MoIncFoodStamps,AppInfo_MoIncOther,AppInfo_MoIncMoChildSupport,AppInfo_MoIncTotalMoAmt,AppInfo_EmpHistJobTitle,AppInfo_EmpHistEmployer,AppInfo_EmpHistCity,AppInfo_EmpHistJobStart,AppInfo_EmpHistJobEnd,AppInfo_EmpHistMoWage,AppInfo_EmpHistJobTitle2,AppInfo_EmpHistEmployer2,AppInfo_EmpHistCity2,AppInfo_EmpHistJobStart2,AppInfo_EmpHistJobEnd2,AppInfo_EmpHistMoWage2,AppInfo_EmpHistJobTitle3,AppInfo_EmpHistEmployer3,AppInfo_EmpHistCity3,AppInfo_EmpHistJobStart3,AppInfo_EmpHistJobEnd3,AppInfo_EmpHistMoWage3,AppInfo_LegInfoMisdemeanor,AppInfo_LegInfoFelony,AppInfo_LegInfoState,AppInfo_LegInfoCounty,AppInfo_LegInfoCharge_Details,AppInfo_LegInfoCharge_Timeframe,AppInfo_LegInfoProbation_Status,AppInfo_LegInfoProbation_Officer,AppInfo_LegInfoVetTreatCourt_Status,AppInfo_LegInfoVetTreatCourt_Phase,AppInfo_LegInfoVetTreatCourt_GradStauts,AppInfo_LegInfoLegal_List,AppInfo_AppAgree_SignDate1,AppInfo_AppAgree_SignDate2,AppIntInfo_Interview_ID,AppIntInfo_Date,AppIntInfo_Status,AppIntInfo_SSN,AppIntInfo_Fname,AppIntInfo_Lname,AppIntInfo_DOB,AppIntInfo_Age,AppIntInfo_MHCurrentHarm,AppIntInfo_MHPastHarm,AppIntInfo_MHPastHarm_Details,AppIntInfo_MHTBI,AppIntInfo_MHPTS,AppIntInfo_MHSubAbuse,AppIntInfo_MHBipoDisorder,AppIntInfo_MHSchizo,AppIntInfo_MHDepression,AppIntInfo_MHAnxiety,AppIntInfo_MHPersDisorder,AppIntInfo_MHOther,AppIntInfo_ASAAlcohol,AppIntInfo_ASAGambling,AppIntInfo_ASAPrescriptDrug,AppIntInfo_ASAStreetDrug,AppIntInfo_ASATobacco,AppIntInfo_ASADrugChoice1,AppIntInfo_ASADrugChoice2,AppIntInfo_ASADrugChoice3,AppIntInfo_ASACurrent_TimeSober,AppIntInfo_ASAPast_TimeSober,AppIntInfo_ASADUI_DWI,AppIntInfo_ASAPossess_Conviction,AppIntInfo_ASASell_Conviction,AppIntInfo_PHPCP_Fname,AppIntInfo_PHPCP_Lname,AppIntInfo_PHPCP_Phone,AppIntInfo_PHPCP_StAddress,AppIntInfo_PHPCP_City,AppIntInfo_PHPCP_State,AppIntInfo_PHPCP_Zip,AppIntInfo_PHPCP_ContactOK,AppIntInfo_PHSeizures,AppIntInfo_PHAllergies,AppIntInfo_PHOther,AppIntInfo_MedsCondition1,AppIntInfo_MedsMedication1,AppIntInfo_MedsDosage1,AppIntInfo_MedsFrequency1,AppIntInfo_MedsCondition2,AppIntInfo_MedsMedication2,AppIntInfo_MedsDosage2,AppIntInfo_MedsFrequency2,AppIntInfo_MedsCondition3,AppIntInfo_MedsMedication3,AppIntInfo_MedsDosage3,AppIntInfo_MedsFrequency3,AppIntInfo_MedsCondition4,AppIntInfo_MedsMedication4,AppIntInfo_MedsDosage4,AppIntInfo_MedsFrequency4,AppIntInfo_MedsCondition5,AppIntInfo_MedsMedication5,AppIntInfo_MedsDosage5,AppIntInfo_MedsFrequency5,AppIntInfo_AgreeSignDate")] FiveStarApplicantInformation fiveStarApplicantInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiveStarApplicantInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FrontDeskPortal_All");
            }
            return View(fiveStarApplicantInformation);
        }

        // GET: FiveStarApplicantInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarApplicantInformation fiveStarApplicantInformation = db.FiveStarApplicantInformation.Find(id);
            if (fiveStarApplicantInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarApplicantInformation);
        }

        // POST: FiveStarApplicantInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FiveStarApplicantInformation fiveStarApplicantInformation = db.FiveStarApplicantInformation.Find(id);
            db.FiveStarApplicantInformation.Remove(fiveStarApplicantInformation);
            db.SaveChanges();
            return RedirectToAction("FrontDeskPortal_All");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
