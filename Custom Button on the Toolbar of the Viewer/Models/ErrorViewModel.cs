using System;

namespace Custom_Button_on_the_Toolbar_of_the_Viewer.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
