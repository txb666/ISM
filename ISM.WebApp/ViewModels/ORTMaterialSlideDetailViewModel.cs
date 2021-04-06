using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class ORTMaterialSlideDetailViewModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public List<ORTMaterialSlide> materials { get; set; }
        public User current_student { get; set; }
        public string program { get; set; }
        public string content { get; set; }
        public string material { get; set; }            
    }
}
