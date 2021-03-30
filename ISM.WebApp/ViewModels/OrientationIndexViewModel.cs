using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class OrientationIndexViewModel
    {
        public List<User> students { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
        public string account { get; set; }
        public string fullname { get; set; }
        public List<OrientationSchedule> orientationSchedules { get; set; }
        public string content { get; set; }
        public DateTime? date { get; set; }
        public TimeSpan? time { get; set; }
        public string location { get; set; }
        public string require_document { get; set; }
    }
}
