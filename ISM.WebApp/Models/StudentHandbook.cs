using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class StudentHandbook
    {
        public int student_handbook_id { get; set; }
        public int campus_id { get; set; }
        public string title { get; set; }
        public string file_name { get; set; }
    }
}
