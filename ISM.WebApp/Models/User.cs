using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class User
    {
        public int user_id { get; set; }
        public int role_id { get; set; }
        public int campus_id { get; set; }
        public string campus { get; set; }
        public int studentGroup_id { get; set; }
        public string email { get; set; }
        public string account { get; set; }
        public bool status { get; set; }
        public bool isFirstLoggedIn { get; set; }
        public string password { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string fullname { get; set; }
        public string nationality { get; set; }
        public DateTime? dob { get; set; }
        public bool? gender { get; set; }
        public string emergency_contact { get; set; }
        public string picture { get; set; }
        public string accomodation { get; set; }
        public int program_id { get; set; }
        public string program { get; set; }
        public DateTime program_duration_start { get; set; }
        public DateTime program_duration_end { get; set; }
        public string home_univercity { get; set; }

        public User()
        {

        }
    }
}
