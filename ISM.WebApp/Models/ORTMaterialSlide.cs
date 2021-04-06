using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class ORTMaterialSlide
    {
        public int ort_material_slide_id { get; set; }
        public int student_id { get; set; }
        public string program { get; set; }
        public string content { get; set; }
        public string material { get; set; }
    }
}
