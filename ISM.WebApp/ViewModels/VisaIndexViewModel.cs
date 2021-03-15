using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class VisaIndexViewModel
    {
        public List<Visa> visalist { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public int visa_id { get; set; }
        public string account { get; set; }
        public string picture { get; set; }
        public string student_name { get; set; }
        public string entry_port { get; set; }
        public DateTime? start_dateFrom { get; set; }
        public DateTime? start_dateTo { get; set; }
        public DateTime? expried_dateFrom { get; set; }
        public DateTime? expried_dateTo { get; set; }
        public DateTime? entry_dateFrom { get; set; }
        public DateTime? entry_dateTo { get; set; }
    }
}
