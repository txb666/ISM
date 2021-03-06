using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class StudentGroupEditViewModel
    {
        public List<ProgramStudy> programList { get; set; }
        public List<Campus> campusList { get; set; }
        public List<User> coordinatorList { get; set; }
        public StudentGroup group { get; set; }
    }
}
