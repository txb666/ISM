using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class PassportIndexViewModel
    {
        public List<Passport> passports { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public string account { get; set; }
        public string picture { get; set; }
        public string student_name { get; set; }
        public string passport_number { get; set; }
        public DateTime? start_dateFrom { get; set; }
        public DateTime? start_dateTo { get; set; }
        public DateTime? expried_dateFrom { get; set; }
        public DateTime? expried_dateTo { get; set; }
        public string issuing_authority { get; set; }
    }
}
