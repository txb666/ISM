using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Models;

namespace ISM.WebApp.ViewModels
{
    public class VisaLetterIndexViewModel
    {
        public List<VisaLetter> VisaLetters { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public int pre_approval_visa_letter_id { get; set; }
        public int student_id { get; set; }
        public string fullname { get; set; }
        public bool gender { get; set; }
        public string nationality { get; set; }
        public string home_university { get; set; }
        public string passport_number { get; set; }
        public string visa_type { get; set; }
        public string visa_period { get; set; }
        public string apply_receive { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? dob { get; set; }
        public DateTime? expired_dateFrom { get; set; }
        public DateTime? expired_dateTo { get; set; }
        public DateTime? modified_date { get; set; }
    }
}
