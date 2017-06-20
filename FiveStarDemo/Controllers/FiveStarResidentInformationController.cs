using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveStarDemo.Models;
using System.IO;
using System.Data.SqlClient;

namespace FiveStarDemo.Controllers
{
    public class FiveStarResidentInformationController : Controller
    {

        private readonly DataLayer dl;

        public FiveStarResidentInformationController()
        {
            dl = new DataLayer();
        }

        public FiveStarDBEntities db = new FiveStarDBEntities();

        // GET: FiveStarResidentInformation - Generic list of residentss in DB
        public ActionResult Index()
        {
            var fiveStarResidentInformation = db.FiveStarResidentInformation.Include(f => f.FiveStarApplicantInformation);
            return View(fiveStarResidentInformation.ToList());
        }

        // GET: View returns all FiveStarResidentInformation profiles
        public ActionResult ResidentPortal_All()
        {
            return View();
        }

        // JSON for getting data for ResidentIndex view
        public JsonResult GetAllResidentProfiles()
        {
            FiveStarResidentInformation FSRI = new FiveStarResidentInformation();
            FSRI.AllResidentProfiles = dl.GetAllFiveStarResidentProfiles();

            return Json(FSRI, JsonRequestBehavior.AllowGet);
        }


        // GET: FiveStarResidentInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarResidentInformation fiveStarResidentInformation = db.FiveStarResidentInformation.Find(id);
            if (fiveStarResidentInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarResidentInformation);
        }

        // GET: FiveStarResidentInformation/Create
        public ActionResult Create()
        {

            var resStatuslist = new List<SelectListItem>();
            resStatuslist.Add(new SelectListItem() { Text = "Not Accepted", Value = "Not Accepted" });
            resStatuslist.Add(new SelectListItem() { Text = "Accepted - Current Resident", Value = "Accepted - Current Resident" });
            resStatuslist.Add(new SelectListItem() { Text = "Accepted - Conditionally", Value = "Accepted - Conditionally" });
            resStatuslist.Add(new SelectListItem() { Text = "Accepted - Emergency(72 hour max)", Value = "Accepted - Emergency(72 hour max)" });
            resStatuslist.Add(new SelectListItem() { Text = "Accepted - Returning Resident", Value = "Accepted - Returning Resident" });
            resStatuslist.Add(new SelectListItem() { Text = "Accepted - Graduated from Program", Value = "Accepted - Graduated from Program" });
            resStatuslist.Add(new SelectListItem() { Text = "Accepted - Removed from Program", Value = "Accepted - Removed from Program" });

            ViewBag.ResStatus = resStatuslist;


            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName");
            return View();
        }

        // JSON for getting data from dropdown list,
        // which will populate resident page input fields
        public JsonResult GetResData(int ID)
        {
            var FSAI = from a in db.FiveStarApplicantInformation
                       where a.AppInfo_ID == ID
                       select new { a.AppInfo_Fname, a.AppInfo_Lname, a.AppInfo_SSN, a.AppInfo_DOB, a.AppInfo_Age, a.AppInfo_Phone, a.AppInfo_Gender };
            return Json(FSAI, JsonRequestBehavior.AllowGet);
        }


        // POST: FiveStarResidentInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResInfo_ID,ResInfo_Status,ResInfo_AcceptDate,ResInfo_DeclineDate,ResInfo_RoomID,ResInfo_DD214,ResInfo_TBTest,ResInfo_PhotoID,ResInfo_HUDVASH_Vouch,ResInfo_BackCheck,ResInfo_CommRecomendation,ResInfo_SSN,ResInfo_Fname,ResInfo_Lname,ResInfo_DOB,ResInfo_Age,ResInfo_Phone,ResInfo_Gender,AppInfo_ID")] FiveStarResidentInformation fiveStarResidentInformation)
        {
            if (ModelState.IsValid)
            {
                db.FiveStarResidentInformation.Add(fiveStarResidentInformation);
                db.SaveChanges();
                return RedirectToAction("ResidentPortal_All");
            }

            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName", fiveStarResidentInformation.AppInfo_ID);
            return View(fiveStarResidentInformation);
        }

        // GET: FiveStarResidentInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarResidentInformation fiveStarResidentInformation = db.FiveStarResidentInformation.Find(id);
            if (fiveStarResidentInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInformation, "AppInfo_ID", "AppInfo_FullName", fiveStarResidentInformation.AppInfo_ID);
            return View(fiveStarResidentInformation);
        }

        // POST: FiveStarResidentInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResInfo_ID,ResInfo_Status,ResInfo_AcceptDate,ResInfo_DeclineDate,ResInfo_RoomID,ResInfo_DD214,ResInfo_TBTest,ResInfo_PhotoID,ResInfo_HUDVASH_Vouch,ResInfo_BackCheck,ResInfo_CommRecomendation,ResInfo_SSN,ResInfo_Fname,ResInfo_Lname,ResInfo_DOB,ResInfo_Age,ResInfo_Phone,ResInfo_Gender,AppInfo_ID")] FiveStarResidentInformation fiveStarResidentInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiveStarResidentInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ResidentPortal_All");
            }
            ViewBag.AppInfo_ID = new SelectList(db.FiveStarApplicantInterviewInformation, "AppInfo_ID", "AppInfo_FullName", fiveStarResidentInformation.AppInfo_ID);
            return View(fiveStarResidentInformation);
        }

        // GET: FiveStarResidentInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiveStarResidentInformation fiveStarResidentInformation = db.FiveStarResidentInformation.Find(id);
            if (fiveStarResidentInformation == null)
            {
                return HttpNotFound();
            }
            return View(fiveStarResidentInformation);
        }

        // POST: FiveStarResidentInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FiveStarResidentInformation fiveStarResidentInformation = db.FiveStarResidentInformation.Find(id);
            db.FiveStarResidentInformation.Remove(fiveStarResidentInformation);
            db.SaveChanges();
            return RedirectToAction("ResidentPortal_All");
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
