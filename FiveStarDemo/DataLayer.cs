using FiveStarDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FiveStarDemo
{

    //This class contains all of the JSON calls
    public class DataLayer
    {

        private String _ConnectionString = GetConnectionString();
        private SqlConnection conn = new SqlConnection();

        //DB Call for returning all incomplete applicant profiles
        public List<IncompleteApplicantProfiles> GetIncompleteFiveStarApplicantProfiles()
        {
            List<IncompleteApplicantProfiles> IAP = new List<IncompleteApplicantProfiles>();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            String sSQL = "";


            sSQL = "SELECT * FROM FiveStarApplicantInformation WHERE AppInfo_Status  LIKE '%Incomplete%' ";

            conn.ConnectionString = _ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            sda.SelectCommand = cmd;

            try
            {
                sda.Fill(dt);
            }
            catch
            {
                goto CleanUp;
            }

            foreach (DataRow r in dt.Rows)
            {
                IncompleteApplicantProfiles iap = new IncompleteApplicantProfiles();

                iap.AppInfo_ID = Convert.ToInt32(r["AppInfo_ID"].ToString());
                iap.AppInfo_Status = r["AppInfo_Status"].ToString();
                iap.AppInfo_SSN = r["AppInfo_SSN"].ToString();
                iap.AppInfo_Fname = r["AppInfo_Fname"].ToString();
                iap.AppInfo_Lname = r["AppInfo_Lname"].ToString();
                IAP.Add(iap);
            }

        CleanUp:
            conn.Close();
            sda.Dispose();
            sda = null;
            dt.Dispose();
            dt = null;
            cmd = null;

            return IAP;
        }

        //DB Call for returning all applicant profiles
        public List<AllApplicantProfiles> GetAllFiveStarApplicantProfiles()
        {
            List<AllApplicantProfiles> AAP = new List<AllApplicantProfiles>();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            String sSQL = "";


            sSQL = "SELECT * FROM FiveStarApplicantInformation";

            conn.ConnectionString = _ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);

            sda.SelectCommand = cmd;

            try
            {
                sda.Fill(dt);
            }
            catch
            {
                goto CleanUp;
            }

            foreach (DataRow r in dt.Rows)
            {
                AllApplicantProfiles aap = new AllApplicantProfiles();

                aap.AppInfo_ID = Convert.ToInt32(r["AppInfo_ID"].ToString());
                aap.AppInfo_SSN = r["AppInfo_SSN"].ToString();
                aap.AppInfo_Fname = r["AppInfo_Fname"].ToString();
                aap.AppInfo_Lname = r["AppInfo_Lname"].ToString();
                AAP.Add(aap);
            }

        CleanUp:
            conn.Close();
            sda.Dispose();
            sda = null;
            dt.Dispose();
            dt = null;
            cmd = null;

            return AAP;
        }

        //DB Call for returning all resident profiles
        public List<AllResidentProfiles> GetAllFiveStarResidentProfiles()
        {
            List<AllResidentProfiles> ARP = new List<AllResidentProfiles>();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            String sSQL = "";


            sSQL = "SELECT * FROM FiveStarResidentInformation ";

            conn.ConnectionString = _ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            sda.SelectCommand = cmd;

            try
            {
                sda.Fill(dt);
            }
            catch
            {
                goto CleanUp;
            }

            foreach (DataRow r in dt.Rows)
            {
                AllResidentProfiles arp = new AllResidentProfiles();

                arp.ResInfo_ID = Convert.ToInt32(r["ResInfo_ID"].ToString());
                arp.ResInfo_SSN = r["ResInfo_SSN"].ToString();
                arp.ResInfo_Fname = r["ResInfo_Fname"].ToString();
                arp.ResInfo_Lname = r["ResInfo_Lname"].ToString();
                ARP.Add(arp);
            }

        CleanUp:
            conn.Close();
            sda.Dispose();
            sda = null;
            dt.Dispose();
            dt = null;
            cmd = null;

            return ARP;
        }

        //DB Call for returning all resident profiles
        public List<AllMentalHealthProfiles> GetAllFiveStarMentalHealthProfiles()
        {
            List<AllMentalHealthProfiles> AMHP = new List<AllMentalHealthProfiles>();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            String sSQL = "";


            sSQL = "SELECT * FROM FiveStarApplicantInterviewInformation ";

            conn.ConnectionString = _ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            sda.SelectCommand = cmd;

            try
            {
                sda.Fill(dt);
            }
            catch
            {
                goto CleanUp;
            }

            foreach (DataRow r in dt.Rows)
            {
                AllMentalHealthProfiles amhp = new AllMentalHealthProfiles();

                amhp.AppIntInfo_ID = Convert.ToInt32(r["AppIntInfo_ID"].ToString());
                amhp.AppIntInfo_SSN = r["AppIntInfo_SSN"].ToString();
                amhp.AppIntInfo_Fname = r["AppIntInfo_Fname"].ToString();
                amhp.AppIntInfo_Lname = r["AppIntInfo_Lname"].ToString();
                AMHP.Add(amhp);
            }

        CleanUp:
            conn.Close();
            sda.Dispose();
            sda = null;
            dt.Dispose();
            dt = null;
            cmd = null;

            return AMHP;
        }

        //Method for changing DB Connection String for JSON calls
        public static String GetConnectionString()
        {
            switch (Properties.Settings.Default.ActiveDBConnection)
            {
                case "Production":
                    return Properties.Settings.Default.ProductionConnectionString;
                case "Testing":
                    return Properties.Settings.Default.TestingConnectionString;
                default:
                    return Properties.Settings.Default.ProductionConnectionString;
            }

        }
    }
}