using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Report_Export_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportController : ControllerBase
    {
        public ExportController()
        {

        }

        [HttpGet]
        public IActionResult Get(string reportName, string format = "pdf")
        {
            // Check a report file in the 'Reports' folder
            var reportFilePath = Path.Combine("Reports", reportName + ".mrt");
            if (!System.IO.File.Exists(reportFilePath))
                reportFilePath = Path.Combine("Reports", reportName + ".mdc");
            if (!System.IO.File.Exists(reportFilePath))
                return Content("The report file does not exist!");

            // Load and render the report template
            var report = new StiReport();
            if (Path.GetExtension(reportFilePath) == "mrt")
            {
                report.Load(reportFilePath);
                report.Render(false);
            }
            // Load the rendered report document
            else
                report.LoadDocument(reportFilePath);

            MemoryStream stream;
            byte[] buffer;

            switch (format)
            {
                // Export to PDF
                case "pdf":
                    var pdfSettings = new StiPdfExportSettings();
                    // settings, if required

                    stream = new MemoryStream();
                    report.ExportDocument(StiExportFormat.Pdf, stream, pdfSettings);
                    buffer = stream.ToArray();
                    stream.Close();
                    return File(buffer, "application/pdf");

                // Export to Excel 2007+
                case "excel":
                    var excelSettings = new StiExcel2007ExportSettings();
                    // settings, if required

                    stream = new MemoryStream();
                    report.ExportDocument(StiExportFormat.Excel2007, stream, excelSettings);
                    buffer = stream.ToArray();
                    stream.Close();
                    return File(buffer, "application/vnd.ms-excel");

                // Export to HTML
                case "html":
                    var htmlSettings = new StiHtmlExportSettings();
                    // settings, if required

                    stream = new MemoryStream();
                    report.ExportDocument(StiExportFormat.Html, stream, htmlSettings);
                    buffer = stream.ToArray();
                    stream.Close();
                    return File(buffer, "text/html");

                default:
                    return Content($"The export format [{format}] is not supported!");
            }
        }
    }
}
