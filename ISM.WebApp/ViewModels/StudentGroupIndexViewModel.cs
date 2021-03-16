using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class StudentGroupIndexViewModel
    {
        public List<StudentGroup> studentGroups { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public int? year { get; set; }
        public string program { get; set; }
        public DateTime? duration_start { get; set; }
        public DateTime? duration_end { get; set; }
        public string home_univercity { get; set; }
        public string campus { get; set; }
        public string coordinator { get; set; }
        public string note { get; set; }
        public List<ProgramStudy> programList { get; set; }
        public List<Campus> campusList { get; set; }
    }
}
