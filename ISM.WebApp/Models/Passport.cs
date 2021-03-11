using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Passport
    {
        public int passport_id { get; set; }
        public string account { get; set; }
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string picture { get; set; }
        public string passport_number { get; set; }
        public DateTime start_date { get; set; }
        public DateTime expried_date { get; set; }
        public string issuing_authority { get; set; }
    }
}
