using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class ContactInformation
    {
        public int contact_id { get; set; }
        public int staff_id { get; set; }
        public string staff_name { get; set; }
        public string staff_account { get; set; }
        public string staff_email { get; set; }
        public bool staff_status { get; set; }
        public string staff_telephone { get; set; }
        public string picture { get; set; }
        public string staff_position { get; set; }
    }
}
