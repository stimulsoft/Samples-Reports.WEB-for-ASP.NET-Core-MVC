using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.Data;

namespace Saving_a_Report_Template_in_the_Designer.Controllers
{
    public class DesignerController : Controller
    {
        static DesignerController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport()
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));
            
            return StiNetCoreDesigner.GetReportResult(this, report);
        }

        public IActionResult SaveReport()
        {
            var requestParams = StiNetCoreDesigner.GetRequestParams(this);
            var report = StiNetCoreDesigner.GetReportObject(this);
            
            // Save the report template, for example to JSON string
            var json = report.SaveToJsonString();

            var name1 = report.ReportName; // Original report name
            var name2 = requestParams.Designer.FileName; // Report name from the Save dialog

            //return StiNetCoreDesigner.SaveReportResult(this);
            return Content($"{{\"infoMessage\":\"Report file saved successfully as {name2}\"}}");
        }

        public IActionResult SaveReportAs()
        {
            return StiNetCoreDesigner.SaveReportResult(this);
        }

        public IActionResult DesignerEvent()
        {
            return StiNetCoreDesigner.DesignerEventResult(this);
        }
    }
}