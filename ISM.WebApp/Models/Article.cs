using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Article
    {
        public int article_id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string fileName { get; set; }
    }
}
