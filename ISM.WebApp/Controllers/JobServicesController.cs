using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
    public class JobServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IWantAJob()
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/Job/IWantAJob.cshtml");
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                return View("Views/Degree/Job/IWantAJob.cshtml");
            }
            return View();
        }
    }
}
