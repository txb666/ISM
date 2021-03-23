using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Visa
    {
        public int visa_id { get; set; }
        public int student_id { get; set; }
        public string account { get; set; }
        public string fullname { get; set; }
        public string picture { get; set; }
        public DateTime start_date { get; set; }
        public DateTime expired_date { get; set; }
        public DateTime date_entry { get; set; }
        public string entry_port { get; set; }
    }
}
