using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class InsuranceFlightNotification
    {
        public int student_id { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public int studentGroup_id { get; set; }
        public string program_name { get; set; }
        public string program_type { get; set; }
        public bool isUpdateFlight { get; set; }
        public bool isUpdateInsurance { get; set; }
        public DateTime duration_start { get; set; }
    }
}
