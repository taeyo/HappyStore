using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyStoreWeb.Models
{
    public class ReportUploadModel
    {
        public string ReportName { get; set; }
        public HttpPostedFileBase PbixReport { get; set; }
        public string ReportCategory { get; set; }
        public string NewReportCategory { get; set; }
    }
}