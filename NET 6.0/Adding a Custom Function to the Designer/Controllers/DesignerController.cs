using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.Data;
using Stimulsoft.Report.Dictionary;

namespace Adding_a_Custom_Function_to_the_Designer.Controllers
{
    public class DesignerController : Controller
    {
        public static string MyFunc(string value)
        {
            return value.ToUpper();
        }

        static DesignerController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);

            var ParamNames = new string[1];
            var ParamTypes = new Type[1];
            var ParamDescriptions = new string[1];

            ParamNames[0] = "value";
            ParamDescriptions[0] = "Descriptions";
            ParamTypes[0] = typeof(string);

            // How to add my function
            StiFunctions.AddFunction(
                "MyCategory",
                "MyFunc",
                "MyFunc",
                "Description",
                typeof(DesignerController),
                typeof(string),
                "Return Description",
                ParamTypes,
                ParamNames,
                ParamDescriptions);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport()
        {
            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/MyTwoSimpleLists.mrt"));
            
            return StiNetCoreDesigner.GetReportResult(this, report);
        }

        public IActionResult DesignerEvent()
        {
            return StiNetCoreDesigner.DesignerEventResult(this);
        }
    }
}