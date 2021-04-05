using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class AvailableTime
    {
        public int mat_id { get; set; }
        public int staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_email { get; set; }
        public DateTime date { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
    }
}
