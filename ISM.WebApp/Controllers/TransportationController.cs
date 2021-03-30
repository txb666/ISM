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
    public class TransportationController : Controller
    {
        public TransportationDAO transportationDAO;
        public StudentGroupDAO studentGroupDAO;
        
        public TransportationController(TransportationDAO transportationDAO,StudentGroupDAO studentGroupDAO)
        {
            this.transportationDAO = transportationDAO;
            this.studentGroupDAO = studentGroupDAO;
        }
        public IActionResult Index(int studentGroup_id=0, DateTime? date=null, string bus=null, string driver=null, string itinerary=null, string supporter=null,int page=1)
        {          
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            int current_staff_id = sessionUser.user_id;
            TransportationIndexViewModel view = new TransportationIndexViewModel();
            view.manageGroup = studentGroupDAO.GetStudentGroupByStaff(current_staff_id, isAdmin, "Mobility");
            if (studentGroup_id == 0 && view.manageGroup.Count>0)
            {
                view.current_student_group_id = view.manageGroup[0].studentGroup_id;
            }
            else
            {
                view.current_student_group_id = studentGroup_id;
            }
            view.page = page;
            view.pageSize = 5;
            view.totalPage = PagingUtils.calculateTotalPage(transportationDAO.getTotalTransportation(view.current_student_group_id,date,bus,driver,itinerary,supporter),view.pageSize);
            view.transportations = transportationDAO.GetTransportations(view.current_student_group_id, view.page, view.pageSize, date, bus, driver, itinerary, supporter);
            view.date = date;
            view.bus = bus;
            view.driver = driver;
            view.itinerary = itinerary;
            view.supporter = supporter;
            return View("Views/Admin/Program/Transportation.cshtml",view);
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
    }
}
