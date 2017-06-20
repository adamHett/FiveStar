using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveStarDemo.Models;

namespace FiveStarDemo.Controllers
{
    public class FiveStarApplicantInterviewInformationController : Controller
    {
        private readonly DataLayer dl;

        public FiveStarApplicantInterviewInformationController()
        {
            dl = new DataLayer();
        }
        private FiveStarDBEntities db = new FiveStarDBEntities();

        // GET: FiveStarApplicantInterviewInformation
        public ActionResult Index()
        {
            var fiveStarApplicantInterviewInformation = db.FiveStarApplicantInterviewInformation.Include(f => f.FiveStarApplicantInformation);
            return View(fiveStarApplicantInterviewInformation.ToList());
        }

        // GET: View returns all FiveStarResidentInformation profiles
        public ActionResult MentalHealthPortal_All()
        {
            return View();
        }

        // JSON for getting data for ResidentIndex view
        public JsonResult GetAllMentalHealthProfiles()
        {
            FiveStarApplicantInterviewInformation FSAII = new FiveStarApplicantInterviewInformation();
            FSAII.AllMentalHealthProfiles = dl.GetAllFiveStarMentalHealthProfiles();

            return Json(FSAII, JsonRequestBehavior.AllowGet);
        }

        // GET: FiveStarApplicantInterviewInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarApplicantInterviewInformation fiveStarApplicantInterviewInformation = db.FiveStarApplicantInterviewInformation.Find(id);
            if (fiveStarApplicantInterviewInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarApplicantInterviewInformation);
        }

        // GET: FiveStarApplicantInterviewInformation/Create
        public ActionResult Create()
        {
            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName");
            return View();
        }

        // JSON for getting data from dropdown list,
        // which will populate resident page input fields
        public JsonResult GetAppIntData(int ID)
        {
            var FSA = from a in db.FiveStarApplicantInformation
                      where a.AppInfo_ID == ID
                      select new { a.AppInfo_Fname, a.AppInfo_Lname, a.AppInfo_SSN, a.AppInfo_DOB, a.AppInfo_Age };
            return Json(FSA, JsonRequestBehavior.AllowGet);
        }


        // POST: FiveStarApplicantInterviewInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppIntInfo_ID,AppIntInfo_Date,AppIntInfo_Status,AppIntInfo_SSN,AppIntInfo_Fname,AppIntInfo_Lname,AppIntInfo_DOB,AppIntInfo_Age,AppIntInfo_MHCurrentHarm,AppIntInfo_MHPastHarm,AppIntInfo_MHPastHarm_Details,AppIntInfo_MHTBI,AppIntInfo_MHPTS,AppIntInfo_MHSubAbuse,AppIntInfo_MHBipoDisorder,AppIntInfo_MHSchizo,AppIntInfo_MHDepression,AppIntInfo_MHAnxiety,AppIntInfo_MHPersDisorder,AppIntInfo_MHOther,AppIntInfo_ASAAlcohol,AppIntInfo_ASAGambling,AppIntInfo_ASAPrescriptDrug,AppIntInfo_ASAStreetDrug,AppIntInfo_ASATobacco,AppIntInfo_ASADrugChoice1,AppIntInfo_ASADrugChoice2,AppIntInfo_ASADrugChoice3,AppIntInfo_ASACurrent_TimeSober,AppIntInfo_ASAPast_TimeSober,AppIntInfo_ASADUI_DWI,AppIntInfo_ASAPossess_Conviction,AppIntInfo_ASASell_Conviction,AppIntInfo_PHPCP_Fname,AppIntInfo_PHPCP_Lname,AppIntInfo_PHPCP_Phone,AppIntInfo_PHPCP_StAddress,AppIntInfo_PHPCP_City,AppIntInfo_PHPCP_State,AppIntInfo_PHPCP_Zip,AppIntInfo_PHPCP_ContactOK,AppIntInfo_PHSeizures,AppIntInfo_PHAllergies,AppIntInfo_PHOther,AppIntInfo_MedsCondition1,AppIntInfo_MedsMedication1,AppIntInfo_MedsDosage1,AppIntInfo_MedsFrequency1,AppIntInfo_MedsCondition2,AppIntInfo_MedsMedication2,AppIntInfo_MedsDosage2,AppIntInfo_MedsFrequency2,AppIntInfo_MedsCondition3,AppIntInfo_MedsMedication3,AppIntInfo_MedsDosage3,AppIntInfo_MedsFrequency3,AppIntInfo_MedsCondition4,AppIntInfo_MedsMedication4,AppIntInfo_MedsDosage4,AppIntInfo_MedsFrequency4,AppIntInfo_MedsCondition5,AppIntInfo_MedsMedication5,AppIntInfo_MedsDosage5,AppIntInfo_MedsFrequency5,AppIntInfo_MedsOther,AppIntInfo_AgreeSignDate,AppInfo_ID")] FiveStarApplicantInterviewInformation fiveStarApplicantInterviewInformation)
        {
            if (ModelState.IsValid)
            {
                db.FiveStarApplicantInterviewInformation.Add(fiveStarApplicantInterviewInformation);
                db.SaveChanges();
                return RedirectToAction("MentalHealthPortal_All");
            }

            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName", fiveStarApplicantInterviewInformation.AppInfo_ID);
            return View(fiveStarApplicantInterviewInformation);
        }

        // GET: FiveStarApplicantInterviewInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarApplicantInterviewInformation fiveStarApplicantInterviewInformation = db.FiveStarApplicantInterviewInformation.Find(id);
            if (fiveStarApplicantInterviewInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName", fiveStarApplicantInterviewInformation.AppInfo_ID);
            return View(fiveStarApplicantInterviewInformation);
        }

        // POST: FiveStarApplicantInterviewInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppIntInfo_ID,AppIntInfo_Date,AppIntInfo_Status,AppIntInfo_SSN,AppIntInfo_Fname,AppIntInfo_Lname,AppIntInfo_DOB,AppIntInfo_Age,AppIntInfo_MHCurrentHarm,AppIntInfo_MHPastHarm,AppIntInfo_MHPastHarm_Details,AppIntInfo_MHTBI,AppIntInfo_MHPTS,AppIntInfo_MHSubAbuse,AppIntInfo_MHBipoDisorder,AppIntInfo_MHSchizo,AppIntInfo_MHDepression,AppIntInfo_MHAnxiety,AppIntInfo_MHPersDisorder,AppIntInfo_MHOther,AppIntInfo_ASAAlcohol,AppIntInfo_ASAGambling,AppIntInfo_ASAPrescriptDrug,AppIntInfo_ASAStreetDrug,AppIntInfo_ASATobacco,AppIntInfo_ASADrugChoice1,AppIntInfo_ASADrugChoice2,AppIntInfo_ASADrugChoice3,AppIntInfo_ASACurrent_TimeSober,AppIntInfo_ASAPast_TimeSober,AppIntInfo_ASADUI_DWI,AppIntInfo_ASAPossess_Conviction,AppIntInfo_ASASell_Conviction,AppIntInfo_PHPCP_Fname,AppIntInfo_PHPCP_Lname,AppIntInfo_PHPCP_Phone,AppIntInfo_PHPCP_StAddress,AppIntInfo_PHPCP_City,AppIntInfo_PHPCP_State,AppIntInfo_PHPCP_Zip,AppIntInfo_PHPCP_ContactOK,AppIntInfo_PHSeizures,AppIntInfo_PHAllergies,AppIntInfo_PHOther,AppIntInfo_MedsCondition1,AppIntInfo_MedsMedication1,AppIntInfo_MedsDosage1,AppIntInfo_MedsFrequency1,AppIntInfo_MedsCondition2,AppIntInfo_MedsMedication2,AppIntInfo_MedsDosage2,AppIntInfo_MedsFrequency2,AppIntInfo_MedsCondition3,AppIntInfo_MedsMedication3,AppIntInfo_MedsDosage3,AppIntInfo_MedsFrequency3,AppIntInfo_MedsCondition4,AppIntInfo_MedsMedication4,AppIntInfo_MedsDosage4,AppIntInfo_MedsFrequency4,AppIntInfo_MedsCondition5,AppIntInfo_MedsMedication5,AppIntInfo_MedsDosage5,AppIntInfo_MedsFrequency5,AppIntInfo_MedsOther,AppIntInfo_AgreeSignDate,AppInfo_ID")] FiveStarApplicantInterviewInformation fiveStarApplicantInterviewInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiveStarApplicantInterviewInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MentalHealthPortal_All");
            }
            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName", fiveStarApplicantInterviewInformation.AppInfo_ID);
            return View(fiveStarApplicantInterviewInformation);
        }

        // GET: FiveStarApplicantInterviewInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarApplicantInterviewInformation fiveStarApplicantInterviewInformation = db.FiveStarApplicantInterviewInformation.Find(id);
            if (fiveStarApplicantInterviewInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarApplicantInterviewInformation);
        }

        // POST: FiveStarApplicantInterviewInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FiveStarApplicantInterviewInformation fiveStarApplicantInterviewInformation = db.FiveStarApplicantInterviewInformation.Find(id);
            db.FiveStarApplicantInterviewInformation.Remove(fiveStarApplicantInterviewInformation);
            db.SaveChanges();
            return RedirectToAction("MentalHealthPortal_All");
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
