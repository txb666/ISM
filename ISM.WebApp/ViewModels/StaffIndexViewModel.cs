using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class StaffIndexViewModel
    {
        public List<User> staffs { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string account { get; set; }
        public DateTime? startDateFrom { get; set; }
        public DateTime? startDateTo { get; set; }
        public DateTime? endDateFrom { get; set; }
        public DateTime? endDateTo { get; set; }
        public bool? gender { get; set; }
        public bool? status { get; set; }

        public StaffIndexViewModel()
        {

        }
    }
}
