using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class LocalRecommendation
    {
        public int local_recommendation_id { get; set; }
        public int campus_id { get; set; }
        public string title { get; set; }
        public string file_name { get; set; }
    }
}
