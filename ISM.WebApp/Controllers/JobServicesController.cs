using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    public class JobServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IWantAJob()
        {
            return View("Views/Admin/Job/IWantAJob.cshtml");
        }
    }
}
