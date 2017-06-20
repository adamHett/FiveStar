using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveStarDemo.Models;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace FiveStarDemo.Controllers
{
    public class ReportsController : Controller
    {
        
        // GET: Reports
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ResidentReport()
        {

            using (FiveStarDBEntities fse = new FiveStarDBEntities())
            {
                var rr = fse.FiveStarResidentInformation.ToList();
                return View(rr);
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult ApplicantReport()
        {

            using (FiveStarDBEntities fse = new FiveStarDBEntities())
            {
                var ar = fse.FiveStarApplicantInformation.ToList();
                return View(ar);
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult ResidentReportFormat(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/RPTReports"), "Resident_Information_Report.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<FiveStarResidentInformation> fsri = new List<FiveStarResidentInformation>();
            using (FiveStarDBEntities fse = new FiveStarDBEntities())
            {
                fsri = fse.FiveStarResidentInformation.ToList();
            }
            ReportDataSource rd = new ReportDataSource("FiveStarResidentDataSet1", fsri);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ApplicantReportFormat(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/RPTReports"), "Applicant_Information_Report.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<FiveStarApplicantInformation> fsai = new List<FiveStarApplicantInformation>();
            using (FiveStarDBEntities fse = new FiveStarDBEntities())
            {
                fsai = fse.FiveStarApplicantInformation.ToList();
            }
            ReportDataSource rd = new ReportDataSource("FiveStarApplicantDataSet1", fsai);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
    }


}