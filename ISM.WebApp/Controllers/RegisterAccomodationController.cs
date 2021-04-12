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
    public class RegisterAccomodationController : Controller
    {
        public AccomodationDAO accomodationDAO;

        public RegisterAccomodationController(AccomodationDAO accomodationDAO)
        {
            this.accomodationDAO = accomodationDAO;
        }
        public IActionResult Index(string degreeOrMobility=null,string account=null, string fullname=null, string email=null, string home_univercity=null, string exchange_campus=null, string accomodation_option=null,string room_type=null, string other_request=null, int page=1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
                bool haveDegree = isAdmin == true ? true : sessionUser.haveDegree;
                int current_staff_id = sessionUser.user_id;
                if (string.IsNullOrEmpty(degreeOrMobility))
                {
                    if (haveDegree)
                    {
                        degreeOrMobility = "Degree";
                    }
                    else
                    {
                        degreeOrMobility = "Mobility";
                    }
                }
                RegisterAccomodationIndexViewModel view = new RegisterAccomodationIndexViewModel();
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.totalPage = PagingUtils.calculateTotalPage(accomodationDAO.getTotalRegisterAccomodation(isAdmin, haveDegree, degreeOrMobility, current_staff_id, account, fullname, email, home_univercity, exchange_campus, accomodation_option, room_type, other_request), view.pageSize);
                view.registerAccomodations = accomodationDAO.getRegisterAccomodation(isAdmin, haveDegree, degreeOrMobility, current_staff_id, view.page, view.pageSize, account, fullname, email, home_univercity, exchange_campus, accomodation_option, room_type, other_request);
                view.degreeOrMobility = degreeOrMobility;
                view.account = account;
                view.fullname = fullname;
                view.email = email;
                view.home_univercity = home_univercity;
                view.exchange_campus = exchange_campus;
                view.accomodation_option = accomodation_option;
                view.room_type = room_type;
                view.other_request = other_request;
                return View("Views/Admin/Program/RegisterAccomodation.cshtml", view);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                RegisterAccomodationIndexViewModel view = new RegisterAccomodationIndexViewModel();
                view.student_register_accomodation = accomodationDAO.GetRegisterAccomodation(sessionUser.user_id);
                return View("Views/Degree/Program/RegisterAccommodation.cshtml", view);
            }
            return View();
        }

        public bool CreateOrEdit(int student_id, int? register_accomodation_id, string exchange_campus, string accomodation_option, double? cost_per_month, double? room_size, string room_type, double? distance, string other_request)
        {
            DateTime register_date = DateTime.Now;
            bool result = accomodationDAO.CreateOrEditRegisterAccomodation(register_accomodation_id, student_id, exchange_campus, accomodation_option, cost_per_month, room_size, room_type, distance, other_request, register_date);
            return result;
        }
    }
}
