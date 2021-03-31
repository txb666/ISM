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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class FlightController : Controller
    {
        public FlightDAO flightDAO;
        private readonly IWebHostEnvironment hostingEnvironment;

        public FlightController(FlightDAO flightDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.flightDAO = flightDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string degreeOrMobility = "", string account = "", string fullname = "", string flight_number_a = "", DateTime? arrival_date_a = null, TimeSpan? arrival_time_a = null, string airport_departure_a = "", string airport_arrival_a = "", string flight_number_d = "", DateTime? arrival_date_d = null, TimeSpan? arrival_time_d = null, string airport_departure_d = "", string airport_arrival_d = "", int page = 1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
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
            FlightIndexViewModel viewModel = new FlightIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(flightDAO.getTotalFlight(isAdmin, degreeOrMobility, haveDegree, current_staff_id, account, fullname, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d), viewModel.pageSize);
            viewModel.flights = flightDAO.getFlight(isAdmin, degreeOrMobility, haveDegree, current_staff_id, viewModel.page, viewModel.pageSize, account, fullname, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d);
            viewModel.fullname = fullname;
            viewModel.account = account;
            viewModel.flight_number_a = flight_number_a;
            viewModel.flight_number_d = flight_number_d;
            viewModel.arrival_date_a = arrival_date_a;
            viewModel.arrival_date_d = arrival_date_d;
            viewModel.arrival_time_a = arrival_time_a;
            viewModel.arrival_time_d = arrival_time_d;
            viewModel.airport_arrival_a = airport_arrival_a;
            viewModel.airport_arrival_d = airport_arrival_d;
            viewModel.airport_departure_a = airport_departure_a;
            viewModel.airport_departure_d = airport_departure_d;
            viewModel.degreeOrMobility = degreeOrMobility;
            return View("Views/Admin/Pre-Departure/StudentFlight.cshtml", viewModel);
        }

        public bool Edit(string degreeOrMobility = "", int? flight_id = null, string flight_number_a = "", DateTime? arrival_date_a = null, TimeSpan? arrival_time_a = null, string airport_departure_a = "", string airport_arrival_a = "", string picture_a = "", string flight_number_d = "", DateTime? arrival_date_d = null, TimeSpan? arrival_time_d = null, string airport_departure_d = "", string airport_arrival_d = "", string picture_d = "")
        {
            bool result = false;
            if (degreeOrMobility.Equals("Mobility"))
            {
                result = flightDAO.editFlightMobility(flight_id, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, picture_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d, picture_d);
            }
            else if (degreeOrMobility.Equals("Degree"))
            {
                result = flightDAO.editFlightDegree(flight_id, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, picture_a);
            }
            return result;
        }

        public bool NotificationDegree(int days_before, DateTime deadline)
        {
            bool result = flightDAO.SetupNotificationDegree(days_before, deadline);
            return result;
        }

        public bool NotificationMobility(int days_before)
        {
            bool result = flightDAO.SetupNotificationMobility(days_before);
            return result;
        }
    }
}
