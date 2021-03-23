using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Notification
    {
        public int user_id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public int days_before { get; set; }
        public DateTime passport_expired { get; set; }
        public DateTime visa_expired { get; set; }
        public DateTime deadline { get; set; }
        public DateTime ort_date { get; set; }
    }
}
