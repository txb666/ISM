using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    public class DetailedAgendaController : Controller
    {
        public DetailedAgendaDAO detailedAgendaDAO;
        public StudentGroupDAO studentGroupDAO;
        public ProgramDAO programDAO;
        public CampusDAO campusDAO;

        public DetailedAgendaController(DetailedAgendaDAO detailedAgendaDAO,StudentGroupDAO studentGroupDAO,ProgramDAO programDAO,CampusDAO campusDAO)
        {
            this.detailedAgendaDAO = detailedAgendaDAO;
            this.studentGroupDAO = studentGroupDAO;
            this.programDAO = programDAO;
            this.campusDAO = campusDAO;
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
            return View("Views/Admin/Program/DetailedAgenda.cshtml", studentGroupViewModel);
        }

        public IActionResult Detail(int student_group_id=0, DateTime? date=null, string time_zone=null, string venue=null, string PIC=null, string content = null, int page=1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                DetailedAgendaDetailViewModel view = new DetailedAgendaDetailViewModel();
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.current_student_group = studentGroupDAO.getStudentGroupById(student_group_id);
                view.totalPage = PagingUtils.calculateTotalPage(detailedAgendaDAO.GetTotalDetailedAgenda(student_group_id, date, time_zone, venue, PIC, content), view.pageSize);
                view.detailedAgendas = detailedAgendaDAO.GetDetailedAgenda(student_group_id, date, time_zone, venue, PIC, content, view.page, view.pageSize);
                view.date = date;
                view.time_zone = time_zone;
                view.venue = venue;
                view.PIC = PIC;
                view.content = content;
                return View("Views/Admin/Program/DetailedAgendaDetail.cshtml", view);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                DetailedAgendaDetailViewModel view = new DetailedAgendaDetailViewModel();
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.totalPage = PagingUtils.calculateTotalPage(detailedAgendaDAO.GetTotalDetailedAgenda(student_group_id, date, time_zone, venue, PIC, content), view.pageSize);
                view.detailedAgendas = detailedAgendaDAO.GetDetailedAgenda(sessionUser.student_group_id, date, time_zone, venue, PIC, content, view.page, view.pageSize);
                view.date = date;
                view.time_zone = time_zone;
                view.venue = venue;
                view.PIC = PIC;
                view.content = content;
                return View("Views/Degree/Program/DetailedAgendaDetail.cshtml", view);
            }
            return View();
        }

        public bool Create(int student_group_id, DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone, string venue, string PIC, string content)
        {
            bool result = detailedAgendaDAO.CreateDetailedAgenda(student_group_id, date, time_start, time_end, time_zone, venue, PIC, content);
            return result;
        }

        public bool Edit(int detailed_agenda_id, DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone, string venue, string PIC, string content)
        {
            bool result = detailedAgendaDAO.EditDetailedAgenda(detailed_agenda_id, date, time_start, time_end, time_zone, venue, PIC, content);
            return result;
        }

        public bool Delete(int detailed_agenda_id)
        {
            bool result = detailedAgendaDAO.DeleteDetailedAgenda(detailed_agenda_id);
            return result;
        }

        public bool IsDetailedAgendaExist(DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone)
        {
            bool result = detailedAgendaDAO.isDetailedAgendaExist(date, time_start, time_end, time_zone);
            return result;
        }

        public bool SetupNotification(int days_before)
        {
            bool result = detailedAgendaDAO.SetupNotification(days_before);
            return result;
        }
    }
}
