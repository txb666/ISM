using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class DetailedAgenda
    {
        public int detailed_agenda_id { get; set; }
        public int student_group_id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time_start { get; set; }
        public TimeSpan time_end { get; set; }
        public string time_zone { get; set; }
        public string venue { get; set; }
        public string PIC { get; set; }
        public string content { get; set; }
    }
}
