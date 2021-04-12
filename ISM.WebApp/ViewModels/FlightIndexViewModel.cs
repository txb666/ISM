using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class FlightIndexViewModel
    {
        public List<Flight> flights { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public string fullname { get; set; }
        public string account { get; set; }
        public string flight_number_a { get; set; }
        public DateTime? arrival_date_a { get; set; }
        public TimeSpan? arrival_time_a { get; set; }
        public string airport_departure_a { get; set; }
        public string airport_arrival_a { get; set; }
        public string picture_a { get; set; }
        public string flight_number_d { get; set; }
        public DateTime? arrival_date_d { get; set; }
        public TimeSpan? arrival_time_d { get; set; }
        public string airport_departure_d { get; set; }
        public string airport_arrival_d { get; set; }
        public string picture_d { get; set; }
        public string degreeOrMobility { get; set; }
        public Flight student_flight { get; set; }
    }
}
