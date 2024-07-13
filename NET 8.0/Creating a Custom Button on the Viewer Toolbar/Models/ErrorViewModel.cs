using System;

namespace Creating_a_Custom_Button_on_the_Viewer_Toolbar.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
