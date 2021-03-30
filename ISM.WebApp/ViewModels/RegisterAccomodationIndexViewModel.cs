using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class RegisterAccomodationIndexViewModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public List<RegisterAccomodation> registerAccomodations { get; set; }
        public string degreeOrMobility { get; set; }
        public string account { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string home_univercity { get; set; }
        public string exchange_campus { get; set; }
        public string accomodation_option { get; set; }
        public string room_type { get; set; }
        public string other_request { get; set; }
    }
}
