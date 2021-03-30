using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class OrientationSchedule
    {
        public int ortSchedule_id { get; set; }
        public int student_id { get; set; }
        public string fullname { get; set; }
        public string account { get; set; }
        public int program { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public string location { get; set; }
        public string require_document { get; set; }
    }
}
