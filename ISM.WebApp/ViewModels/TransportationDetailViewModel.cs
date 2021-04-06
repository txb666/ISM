using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class TransportationDetailViewModel
    {
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public List<Transportation> transportations { get; set; }
        public DateTime? date { get; set; }
        public string bus { get; set; }
        public string driver { get; set; }
        public string itinerary { get; set; }
        public string supporter { get; set; }
        public StudentGroup current_student_group { get; set; }
    }
}
