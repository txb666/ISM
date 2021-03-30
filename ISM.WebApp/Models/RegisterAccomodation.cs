using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class RegisterAccomodation
    {
        public int register_accomodation_id { get; set; }
        public int student_id { get; set; }
        public string account { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string home_univercity { get; set; }
        public string exchange_campus { get; set; }
        public string accomodation_option { get; set; }
        public double? cost_per_month { get; set; }
        public double? room_size { get; set; }
        public string room_type { get; set; }
        public double? distance { get; set; }
        public string other_request { get; set; }
        public DateTime register_date { get; set; }
    }
}
