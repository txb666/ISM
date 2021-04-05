using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class JobVacancyIndexViewModel
    {
        public List<JobVacancy> jobVacancies { get; set; }
        public int jobVacancy_id { get; set; }
        public string job_name { get; set; }
        public string job_location { get; set; }
        public string employment_type { get; set; }
        public string content { get; set; }
        public DateTime? deadline { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
    }
}
