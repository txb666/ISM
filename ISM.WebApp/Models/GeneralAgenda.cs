using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class GeneralAgenda
    {
        public int general_agenda_id { get; set; }
        public int student_group_id { get; set; }
        public string content { get; set; }
        public string note { get; set; }
    }
}
