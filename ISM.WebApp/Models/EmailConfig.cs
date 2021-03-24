using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Models
{
    public class EmailConfig
    {
        public string email { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
    }
}
