using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiveStarDemo.Models
{
    public class IncompleteApplicantProfiles
    {
        public int AppInfo_ID { get; set; }
        public string AppInfo_Status { get; set; }
        public string AppInfo_SSN { get; set; }
        public string AppInfo_Fname { get; set; }
        public string AppInfo_Lname { get; set; }
    }
}