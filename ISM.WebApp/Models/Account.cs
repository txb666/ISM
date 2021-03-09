using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public bool status { get; set; }
        public bool isFirstLoggedIn { get; set; }
    }
}
