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

namespace Using_a_Custom_Data_Adapter.Controllers
{
    public class DesignerController : Controller
    {
        static DesignerController()
        {
            //Clearing standard data adapters, if necessary
            StiOptions.Services.Databases.Clear();

            //Adding a Custom PostgreSQL data adapter
            StiOptions.Services.Databases.Add(new CustomPostgreSQLDatabase());
            StiOptions.Services.DataAdapters.Add(new CustomPostgreSQLAdapterService());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport()
        {
            var report = new StiReport();

            //Adding a connection to the report from code
            var database = new CustomPostgreSQLDatabase("CustomData1", "Server=127.0.0.1; Port=5432; Database=myDataBase; User Id=myUsername; Password=myPassword;");
            report.Dictionary.Databases.Add(database);

            //Adding a reference to this project using a custom adapter class
            var assemblies = report.ReferencedAssemblies.ToList();
            assemblies.Add("Using_a_Custom_Data_Adapter.dll");
            report.ReferencedAssemblies = assemblies.ToArray();

            return StiNetCoreDesigner.GetReportResult(this, report);
        }

        public IActionResult DesignerEvent()
        {
            return StiNetCoreDesigner.DesignerEventResult(this);
        }
    }
}