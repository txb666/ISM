using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class ORTMaterialsDetailViewModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public StudentGroup current_student_group { get; set; }
        public List<ORTMaterials> materials { get; set; }
        public string content { get; set; }
        public string note { get; set; }
    }
}
