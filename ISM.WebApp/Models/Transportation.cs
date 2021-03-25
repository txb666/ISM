using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Transportation
    {
        public int transportations_id { get; set; }
        public int studentGroup_id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public string bus { get; set; }
        public string driver { get; set; }
        public string itinerary { get; set; }
        public string supporter { get; set; }
        public string note { get; set; }
    }
}
