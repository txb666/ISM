using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
    public class StudentHandbookController : Controller
    {
        public StudentHandbookDAO studentHandbookDAO;
        private readonly IWebHostEnvironment hostingEnvironment;
        public StudentHandbookController(StudentHandbookDAO studentHandbookDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.studentHandbookDAO = studentHandbookDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            List<StudentHandbook> studentHandbooks = studentHandbookDAO.getAllStudentHandbook();
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/Pre-Departure/StudentHandbook.cshtml", studentHandbooks);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                return View("Views/Degree/PreDeparture/StudentHandbook.cshtml", studentHandbooks);
            }
            return View();
        }

        public IActionResult Detail(int id)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            StudentHandbook studentHandbook = studentHandbookDAO.GetStudentHandbookById(id);
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/Pre-Departure/StudentHandbookDetail.cshtml", studentHandbook);
            }
            else if (sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                return View("Views/Degree/PreDeparture/StudentHandbookDetail.cshtml", studentHandbook);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Staff")]
        public IActionResult Edit(int student_handbook_id, string title, IFormFile file)
        {
            string file_name = "student_handbook_" + student_handbook_id + ".pdf";
            if (file != null)
            {
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string subfolderPath = Path.Combine(article, "Student Handbook");
                string filePath = Path.Combine(subfolderPath, file_name);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
            }
            bool result = studentHandbookDAO.editStudentHandbook(student_handbook_id, title, file_name);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }
    }
}
