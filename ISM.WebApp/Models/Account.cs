using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class Account
    {
        public int user_id { get; set; }
        public int student_group_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public bool status { get; set; }
        public bool isFirstLoggedIn { get; set; }
        public bool haveDegree { get; set; }
        public List<WebNotification> webNotifications { get; set; }
        public int totalNotification { get; set; }
    }
}
