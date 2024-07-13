using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Base;

namespace Changing_the_Viewer_Options_from_the_Controller.Controllers
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

        public IActionResult Index()
        {
            var viewerOptions = new StiNetCoreViewerOptions();
            viewerOptions.Actions.GetReport = "GetReport";
            viewerOptions.Actions.ViewerEvent = "ViewerEvent";
            viewerOptions.Appearance.FullScreenMode = true;
            viewerOptions.Toolbar.Zoom = 75;
            viewerOptions.Toolbar.ViewMode = Stimulsoft.Report.Web.StiWebViewMode.MultiplePages;
            viewerOptions.Theme = Stimulsoft.Report.Web.StiViewerTheme.Office2013WhiteViolet;

            // Passing options via ViewBag
            ViewBag.ViewerOptions = viewerOptions;

            return View();
        }
        
        public IActionResult GetReport()
        {
            // Create the report object
            var report = new StiReport();

            // Load report snapshot
            report.LoadDocument(StiNetCoreHelper.MapPath(this, "Reports/SimpleList.mdc"));

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}