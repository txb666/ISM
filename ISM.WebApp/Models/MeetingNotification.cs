using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class MeetingNotification
    {
        public int mt_schedule_id { get; set; }
        public int staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_email { get; set; }
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string student_email { get; set; }
        public DateTime date { get; set; }
    }
}
