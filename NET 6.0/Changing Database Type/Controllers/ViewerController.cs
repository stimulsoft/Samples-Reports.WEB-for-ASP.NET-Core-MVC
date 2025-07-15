using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;

namespace Changing_Database_Type.Controllers
{
    public class ViewerController : Controller
    {
        static ViewerController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public IActionResult Index(bool changeDatabase)
        {
            ViewBag.ShowReportWithChangedDatabase = changeDatabase;

            return View();
        }

        public IActionResult GetReportWithOriginalDatabase()
        {
            // Loading the report template
            var reportPath = StiNetCoreHelper.MapPath(this, "Reports/Report.mrt");
            var report = new StiReport();
            report.Load(reportPath);

            // Showing the report unchanged.
            // It has JSON database as its source of data.
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult GetReportWithChangedDatabase()
        {
            // Loading the report template
            var reportPath = StiNetCoreHelper.MapPath(this, "Reports/Report.mrt");
            var report = new StiReport();
            report.Load(reportPath);

            // Retrieving the only database from the template, the JSON one.
            var originalJsonDatabase = (StiJsonDatabase)report.Dictionary.Databases[0];

            // Changing its type to MySql.
            var newMySqlDatabase = (StiMySqlDatabase)report.Dictionary.ChangeDatabaseType(originalJsonDatabase, typeof(StiMySqlDatabase));

            // Providing connection information.
            // New database should contain data with the same structure as the original database,
            // i.e. tables with the same names and columns with the same names and data types.
            newMySqlDatabase.ConnectionString = "Server=myServerAddress; Database=myDataBase; UserId=myUsername; Pwd=myPassword;";

            // Showing the same template with another database as its source of data.
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}
