using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class InsuranceIndexViewModel
    {
        public List<Insurance> insurances { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public string fullname { get; set; }
        public string account { get; set; }
        public DateTime? startDateFrom { get; set; }
        public DateTime? startDateTo { get; set; }
        public DateTime? expiryDateFrom { get; set; }
        public DateTime? expiryDateTo { get; set; }
    }
}
