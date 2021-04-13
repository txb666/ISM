using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
    public class TransportationController : Controller
    {
        public TransportationDAO transportationDAO;
        public StudentGroupDAO studentGroupDAO;
        public ProgramDAO programDAO;
        public CampusDAO campusDAO;

        public TransportationController(TransportationDAO transportationDAO, StudentGroupDAO studentGroupDAO, ProgramDAO programDAO, CampusDAO campusDAO)
        {
            this.transportationDAO = transportationDAO;
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
            studentGroupViewModel.totalPage = PagingUtils.calculateTotalPage(studentGroupDAO.getTotalStudentGroupByStaff(current_staff_id,isAdmin,"Mobility",program,home_univercity,campus,year), studentGroupViewModel.pageSize);
            studentGroupViewModel.studentGroups = studentGroupDAO.GetStudentGroupByStaffWithPaging(current_staff_id,isAdmin,"Mobility",studentGroupViewModel.page,studentGroupViewModel.pageSize,program,home_univercity,campus,year);
            studentGroupViewModel.year = year;
            studentGroupViewModel.program = program;
            studentGroupViewModel.duration_start = duration_start;
            studentGroupViewModel.duration_end = duration_end;
            studentGroupViewModel.home_univercity = home_univercity;
            studentGroupViewModel.campus = campus;
            studentGroupViewModel.note = note;
            studentGroupViewModel.campusList = campusDAO.getAllCampus();
            studentGroupViewModel.programList = programDAO.getAllProgram();
            return View("Views/Admin/Program/Transportation.cshtml", studentGroupViewModel);
        }

        public IActionResult Detail(int studentGroup_id = 0, DateTime? date = null, string bus = null, string driver = null, string itinerary = null, string supporter = null, int page = 1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                TransportationDetailViewModel view = new TransportationDetailViewModel();
                view.current_student_group = studentGroupDAO.getStudentGroupById(studentGroup_id);
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.totalPage = PagingUtils.calculateTotalPage(transportationDAO.getTotalTransportation(view.current_student_group.studentGroup_id, date, bus, driver, itinerary, supporter), view.pageSize);
                view.transportations = transportationDAO.GetTransportations(view.current_student_group.studentGroup_id, view.page, view.pageSize, date, bus, driver, itinerary, supporter);
                view.date = date;
                view.bus = bus;
                view.driver = driver;
                view.itinerary = itinerary;
                view.supporter = supporter;
                return View("Views/Admin/Program/TransportationDetail.cshtml", view);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                TransportationDetailViewModel view = new TransportationDetailViewModel();
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.totalPage = PagingUtils.calculateTotalPage(transportationDAO.getTotalTransportation(sessionUser.student_group_id, date, bus, driver, itinerary, supporter), view.pageSize);
                view.transportations = transportationDAO.GetTransportations(sessionUser.student_group_id, view.page, view.pageSize, date, bus, driver, itinerary, supporter);
                view.date = date;
                view.bus = bus;
                view.driver = driver;
                view.itinerary = itinerary;
                view.supporter = supporter;
                return View("Views/Degree/Program/TransportationDetail.cshtml", view);
            }
            return View();
        }

        public bool Create(int student_group_id, DateTime date, TimeSpan time, string bus, string driver, string itinerary, string supporter, string note)
        {
            bool result = transportationDAO.createTransportation(student_group_id, date, time, bus, driver, itinerary, supporter, note);
            return result;
        }

        public bool Edit(int transportation_id, DateTime date, TimeSpan time, string bus, string driver, string itinerary, string supporter, string note)
        {
            bool result = transportationDAO.editTransportation(transportation_id, date, time, bus, driver, itinerary, supporter, note);
            return result;
        }

        public bool Delete(int transportations_id)
        {
            bool result = transportationDAO.DeleteTransportation(transportations_id);
            return result;
        }

        public bool SetupNotification(int hours_before)
        {
            bool result = transportationDAO.setupNotification(hours_before);
            return result;
        }
    }
}
