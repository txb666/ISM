using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class JobVacancy
    {
        public int jobVacancy_id { get; set; }
        public string job_name { get; set; }
        public string job_location { get; set; }
        public string employment_type { get; set; }
        public string content { get; set; }
        public DateTime deadline { get; set; }
    }
}
