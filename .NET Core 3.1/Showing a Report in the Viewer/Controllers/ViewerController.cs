using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using StiReports;
using Stimulsoft.Base;

namespace Showing_a_Report_in_the_Viewer.Controllers
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
            return View();
        }
        
        public IActionResult GetReport(int id = 1)
        {
            // Create the report object
            var report = new StiReport();

            // Load report
            switch (id)
            {
                // Load report snapshot
                case 1:
                    report.LoadDocument(StiNetCoreHelper.MapPath(this, "Reports/SimpleList.mdc"));
                    break;

                // Load report template
                case 2:
                    report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));
                    break;

                // Load compiled report class
                case 3:
                    report = new StiMasterDetail();
                    break;

                // Load compiled report class
                case 4:
                    report = new StiParametersSelectingCountryReport();
                    break;
            }

            // Load data from JSON file for report template
            if (!report.IsDocument)
            {
                var data = StiJsonToDataSetConverterV2.GetDataSetFromFile(StiNetCoreHelper.MapPath(this, "Data/Demo.json"));
                report.Dictionary.Databases.Clear();
                report.RegData(data);
            }

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}