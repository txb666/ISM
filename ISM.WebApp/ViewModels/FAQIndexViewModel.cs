using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class FAQIndexViewModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public List<FAQ> faqs { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
