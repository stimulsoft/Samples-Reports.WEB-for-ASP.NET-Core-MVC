using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;

namespace Configuring_Report_caching.Controllers
{
    public class StiFileCacheHelper : StiCacheHelper
    {
        public override StiReport GetReport(string guid)
        {
            var path = Path.Combine(HttpContext.Server.MapPath("CacheFiles"), guid);
            if (File.Exists(path))
            {
                var report = new StiReport();
                var packedReport = File.ReadAllText(path);
                if (guid.EndsWith(GUID_ReportTemplate)) report.LoadPackedReportFromString(packedReport);
                else report.LoadPackedDocumentFromString(packedReport);

                return report;
            }
            return null;
        }

        public override void SaveReport(StiReport report, string guid)
        {
            var packedReport = guid.EndsWith(GUID_ReportTemplate) ? report.SavePackedReportToString() : report.SavePackedDocumentToString();
            var path = Path.Combine(HttpContext.Server.MapPath("CacheFiles"), guid);
            File.WriteAllText(path, packedReport);
        }

        public override void RemoveReport(string guid)
        {
            var path = Path.Combine(HttpContext.Server.MapPath("CacheFiles"), guid);
            if (File.Exists(path))
                File.Delete(path);
        }
    }

    public class FileCacheController : Controller
    {
        static FileCacheController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public FileCacheController()
        {
            StiNetCoreViewer.CacheHelper = new StiFileCacheHelper();
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult GetReport(int id = 1)
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}