using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class MeetingSchedule
    {
        public int ms_id_r { get; set; }
        public int staff_id_r { get; set; }
        public string staff_name_r { get; set; }
        public int student_id_r { get; set; }
        public string student_account_r { get; set; }
        public string student_name_r { get; set; }
        public string student_email_r { get; set; }
        public string note_r { get; set; }
        public DateTime date_MS_r { get; set; }
        public TimeSpan startTime_MS_r { get; set; }
        public TimeSpan endTime_MS_r { get; set; }
        public bool isAccepted_r { get; set; }

        public int ms_id_s { get; set; }
        public int staff_id_s { get; set; }
        public string staff_name_s { get; set; }
        public int student_id_s { get; set; }
        public string student_account_s { get; set; }
        public string student_name_s { get; set; }
        public string student_email_s { get; set; }
        public string note_s { get; set; }
        public DateTime date_MS_s { get; set; }
        public TimeSpan startTime_MS_s { get; set; }
        public TimeSpan endTime_MS_s { get; set; }
        public bool isAccepted_s { get; set; }
    }
}
