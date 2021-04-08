using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class ContactInformationIndexViewModel
    {
        public List<ContactInformation> contactList { get; set; }
        public ContactInformation contact { get; set; }
        public string staff_account { get; set; }
        public string staff_name { get; set; }
        public string staff_email { get; set; }
        public bool? status { get; set; }
        public string telephone { get; set; }
        public string position { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
    }
}
