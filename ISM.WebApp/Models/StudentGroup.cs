using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class StudentGroup
    {
        public int studentGroup_id { get; set; }
        public int program_id { get; set; }
        public string program_name { get; set; }
        public int campus_id { get; set; }
        public string campus_name { get; set; }
        public int year { get; set; }
        public DateTime duration_start { get; set; }
        public DateTime duration_end { get; set; }
        public string home_university { get; set; }
        public string coordinator { get; set; }
        public string note { get; set; }

    }
}
