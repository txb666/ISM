using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    public class StudentHandbookController : Controller
    {
        public StudentHandbookDAO studentHandbookDAO;
        public StudentHandbookController(StudentHandbookDAO studentHandbookDAO)
        {
            this.studentHandbookDAO = studentHandbookDAO;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
