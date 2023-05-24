using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using BusinessObjects;

namespace Using_Business_Objects_in_the_Report.Controllers
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

        public IActionResult ActionITypedList()
        {
            return View("ViewITypedList");
        }

        public IActionResult ActionIEnumerable()
        {
            return View("ViewIEnumerable");
        }

        public IActionResult ActionITypedListBO()
        {
            return View("ViewITypedListBO");
        }

        public IActionResult ActionIEnumerableBO()
        {
            return View("ViewIEnumerableBO");
        }

        public IActionResult GetReportITypedList()
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/BusinessObjects_ITypedList.mrt"));
            report.RegData("EmployeeITypedList", CreateBusinessObjectsITypedList.GetEmployees());

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult GetReportIEnumerable()
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/BusinessObjects_IEnumerable.mrt"));
            report.RegData("EmployeeIEnumerable", CreateBusinessObjectsIEnumerable.GetEmployees());

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult GetReportITypedListBO()
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/BusinessObjects_ITypedList_BO.mrt"));
            report.RegBusinessObject("EmployeeITypedList", CreateBusinessObjectsITypedList.GetEmployees());
            report.Dictionary.SynchronizeBusinessObjects(2);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult GetReportIEnumerableBO()
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/BusinessObjects_IEnumerable_BO.mrt"));
            report.RegBusinessObject("EmployeeIEnumerable", CreateBusinessObjectsIEnumerable.GetEmployees());
            report.Dictionary.SynchronizeBusinessObjects(2);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}