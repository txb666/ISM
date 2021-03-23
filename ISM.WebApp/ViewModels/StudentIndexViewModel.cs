using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class StudentIndexViewModel
    {
        public string degreeOrMobility { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public List<User> students { get; set; }
        public List<Campus> campusList { get; set; }
        public List<ProgramStudy> programList { get; set; }
        public List<StudentGroup> manageGroup { get; set; }
        public string fullname { get; set; }
        public string account { get; set; }
        public string email { get; set; }
        public string nationality { get; set; }
        public DateTime? dob { get; set; }
        public bool? gender { get; set; }
        public int? campus_id { get; set; }
        public int? program_id { get; set; }
        public string emergency_contact { get; set; }
        public string home_univercity { get; set; }
        public DateTime? program_duration_start { get; set; }
        public DateTime? program_duration_end { get; set; }
        public string accomodation { get; set; }
        public bool? status { get; set; }
    }
}
