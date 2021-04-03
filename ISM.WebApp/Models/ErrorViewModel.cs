using System;

namespace ISM.WebApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string message { get; set; }
        public string stackTrace { get; set; }
    }
}
