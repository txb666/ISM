using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class CurrentAccomodation
    {
        public int current_accomodation_id { get; set; }
        public User student { get; set; }
        public StudentGroup student_group { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public double? fee { get; set; }
        public string picture { get; set; }
        public string note { get; set; }
    }
}
