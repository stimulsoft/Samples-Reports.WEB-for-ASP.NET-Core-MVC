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
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report.Components;
using System.Drawing;
using Stimulsoft.Base;

namespace Adding_a_Font_to_the_Resource.Controllers
{
    public class DesignerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport()
        {
            var report = new StiReport();

            //Loading and adding a font to resources
            var fileContent = System.IO.File.ReadAllBytes(StiNetCoreHelper.MapPath(this, "Fonts/Roboto-Black.ttf"));
            var resource = new StiResource("Roboto-Black", "Roboto-Black", false, StiResourceType.FontTtf, fileContent, false);
            report.Dictionary.Resources.Add(resource);

            //Adding a font from resources to the font collection
            StiFontCollection.AddResourceFont(resource.Name, resource.Content, "ttf", resource.Alias);

            //Creating a text component
            var dataText = new StiText();
            dataText.ClientRectangle = new RectangleD(1, 1, 3, 2);
            dataText.Text = "Sample Text";
            dataText.Font = StiFontCollection.CreateFont("Roboto-Black", 12, FontStyle.Regular);
            dataText.Border.Side = StiBorderSides.All;

            report.Pages[0].Components.Add(dataText);

            return StiNetCoreDesigner.GetReportResult(this, report);
        }

        public IActionResult DesignerEvent()
        {
            return StiNetCoreDesigner.DesignerEventResult(this);
        }
    }
}