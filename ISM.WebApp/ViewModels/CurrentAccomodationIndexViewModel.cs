using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class CurrentAccomodationIndexViewModel
    {
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public List<CurrentAccomodation> currentAccomodations { get; set; }
        public List<User> degreeStudents { get; set; }
        public List<StudentGroup> manageGroup { get; set; }
        public int? student_id { get; set; }
        public int? student_group_id { get; set; }
        public string account { get; set; }
        public string fullname { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public string degreeOrMobility { get; set; }
    }
}
