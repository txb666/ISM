using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class NotificationConfig
    {
        public int config_id { get; set; }
        public string type { get; set; }
        public int days_before { get; set; }
        public int hours_before { get; set; }
        public string kind { get; set; }
        public DateTime deadline { get; set; }
    }
}
