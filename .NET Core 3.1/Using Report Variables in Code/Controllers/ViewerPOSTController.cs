using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;

namespace Using_Report_Variables_in_Code.Controllers
{
    public class ViewerPOSTController : Controller
    {
        static ViewerPOSTController()
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
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/Variables.mrt"));

            report.Compile();

            var formValues = StiNetCoreViewer.GetFormValues(this);
            report["Name"] = formValues["name"] ?? string.Empty;
            report["Surname"] = formValues["surname"] ?? string.Empty;
            report["Email"] = formValues["email"] ?? string.Empty;
            report["Address"] = formValues["address"] ?? string.Empty;
            report["Sex"] = formValues["sex"] != null && Convert.ToBoolean(formValues["sex"]);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}
