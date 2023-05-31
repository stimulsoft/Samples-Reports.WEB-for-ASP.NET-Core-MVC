using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;

namespace Configuring_Report_caching.Controllers
{
    public class StiDefaultCacheHelper : StiCacheHelper
    {
        public override StiReport GetReport(string guid)
        {
            return base.GetReport(guid);
        }

        public override void SaveReport(StiReport report, string guid)
        {
            base.SaveReport(report, guid);
        }

        public override void RemoveReport(string guid)
        {
            base.RemoveReport(guid);
        }
    }

    public class DefaultCacheController : Controller
    {
        static DefaultCacheController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public DefaultCacheController()
        {
            StiNetCoreViewer.CacheHelper = new StiDefaultCacheHelper();
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