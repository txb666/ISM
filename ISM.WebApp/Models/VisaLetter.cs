using System;

namespace ISM.WebApp.Models
{
    public class VisaLetter
    {
        public int pre_approval_visa_letter_id { get; set; }
        public int student_id { get; set; }
        public int? pre_approval_visa_letter_id { get; set; }
        public int student_id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public bool? gender { get; set; }
        public DateTime? dob { get; set; }
        public string nationality { get; set; }
        public string home_univercity { get; set; }
        public string passport_number { get; set; }
        public DateTime? expired_date { get; set; }
        public string visa_type { get; set; }
        public string visa_period { get; set; }
        public string apply_receive { get; set; }
       
    }
}
