using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class StudentHandbookController : Controller
    {
        public StudentHandbookDAO studentHandbookDAO;
        public StudentHandbookController(StudentHandbookDAO studentHandbookDAO)
        {
            this.studentHandbookDAO = studentHandbookDAO;
        }
        public IActionResult Index()
        {
            List<StudentHandbook> studentHandbooks = studentHandbookDAO.getAllStudentHandbook();
            return View("Views/Admin/Pre-Departure/StudentHandbook.cshtml", studentHandbooks);
        }

        public IActionResult Detail(int id)
        {
            StudentHandbook studentHandbook = studentHandbookDAO.GetStudentHandbookById(id);
            return View("Views/Admin/Pre-Departure/StudentHandbookDetail.cshtml", studentHandbook);
        }
    }
}
