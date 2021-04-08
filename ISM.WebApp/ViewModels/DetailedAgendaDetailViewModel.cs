using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class DetailedAgendaDetailViewModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public StudentGroup current_student_group { get; set; }
        public List<DetailedAgenda> detailedAgendas { get; set; }
        public DateTime? date { get; set; }
        public string time_zone { get; set; }
        public string venue { get; set; }
        public string PIC { get; set; }
        public string content { get; set; }
    }
}
