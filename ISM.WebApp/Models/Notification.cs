using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Notification
    {
        public int user_id { get; set; }
        public int studentGroup_id { get; set; }
        public bool isUpdatePassport { get; set; }
        public bool isUpdateVisa { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public int days_before { get; set; }
        public int total_days { get; set; }
        public DateTime passport_expired { get; set; }
        public DateTime visa_expired { get; set; }
        public DateTime deadline { get; set; }
        public DateTime ort_date { get; set; }
        public int hours_before { get; set; }
        public DateTime duration_start { get; set; }
        public DateTime detail_agenda_date { get; set; }
        public TimeSpan bus_time { get; set; }
        public DateTime bus_date { get; set; }
        public bool isSend { get; set; }
    }
}
