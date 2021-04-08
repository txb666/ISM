using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ISM.WebApp.Controllers
{
    public class GeneralAgendaController : Controller
    {
        public GeneralAgendaDAO generalAgendaDAO;
        public StudentGroupDAO studentGroupDAO;
        public ProgramDAO programDAO;
        public CampusDAO campusDAO;
        private readonly IWebHostEnvironment hostingEnvironment;

        public GeneralAgendaController(GeneralAgendaDAO generalAgendaDAO, StudentGroupDAO studentGroupDAO, ProgramDAO programDAO, CampusDAO campusDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.generalAgendaDAO = generalAgendaDAO;
            this.studentGroupDAO = studentGroupDAO;
            this.programDAO = programDAO;
            this.campusDAO = campusDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int? year = null, string program = "", DateTime? duration_start = null, DateTime? duration_end = null, string home_univercity = "", string campus = "", string note = "", int page = 1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            int current_staff_id = sessionUser.user_id;
            StudentGroupIndexViewModel studentGroupViewModel = new StudentGroupIndexViewModel();
            studentGroupViewModel.page = page;
            studentGroupViewModel.pageSize = pagingConst.PAGE_SIZE;
            studentGroupViewModel.totalPage = PagingUtils.calculateTotalPage(studentGroupDAO.getTotalStudentGroupByStaff(current_staff_id, isAdmin, "Mobility", program, home_univercity, campus, year), studentGroupViewModel.pageSize);
            studentGroupViewModel.studentGroups = studentGroupDAO.GetStudentGroupByStaffWithPaging(current_staff_id, isAdmin, "Mobility", studentGroupViewModel.page, studentGroupViewModel.pageSize, program, home_univercity, campus, year);
            studentGroupViewModel.year = year;
            studentGroupViewModel.program = program;
            studentGroupViewModel.duration_start = duration_start;
            studentGroupViewModel.duration_end = duration_end;
            studentGroupViewModel.home_univercity = home_univercity;
            studentGroupViewModel.campus = campus;
            studentGroupViewModel.note = note;
            studentGroupViewModel.campusList = campusDAO.getAllCampus();
            studentGroupViewModel.programList = programDAO.getAllProgram();
            return View("Views/Admin/Pre-Departure/GeneralAgenda.cshtml", studentGroupViewModel);
        }

        public IActionResult Detail(int student_group_id = 0)
        {
            GeneralAgendaDetailViewModel view = new GeneralAgendaDetailViewModel();
            view.current_student_group = studentGroupDAO.getStudentGroupById(student_group_id);
            view.generalAgenda = generalAgendaDAO.GetGeneralAgendaByStudentGroup(student_group_id);
            return View("Views/Admin/Pre-Departure/GeneralAgendaDetail.cshtml", view);
        }

        public IActionResult Edit(int general_agenda_id, string note, IFormFile file)
        {
            string fileName = "";
            if (file != null)
            {
                fileName = "general_agenda_" + general_agenda_id + Path.GetExtension(file.FileName);
                string generalAgenda = Path.Combine(hostingEnvironment.WebRootPath, "GeneralAgenda");             
                string filePath = Path.Combine(generalAgenda, fileName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
            }
            bool result = generalAgendaDAO.EditGeneralAgenda(general_agenda_id, fileName, note);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }
    }
}
