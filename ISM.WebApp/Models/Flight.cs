using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Flight
    {
      public  int? flight_id { get; set; }
        public int student_id { get; set; }
        public string fullname { get; set; }
        public string account { get; set; }
        public string flight_number_a { get; set; }
        public DateTime? arrival_date_a { get; set; }
        public TimeSpan? arrival_time_a { get; set; }
        public string airport_departure_a { get; set; }
        public string airport_arrival_a { get; set; }
        public string picture_a { get; set; }
        public string flight_number_d  { get; set; }
        public DateTime? arrival_date_d { get; set; }
        public TimeSpan? arrival_time_d { get; set; }
        public string airport_departure_d { get; set; }
        public string airport_arrival_d { get; set; }
        public string picture_d { get; set; }
    }
}
