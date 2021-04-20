using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
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
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                FlightIndexViewModel view = new FlightIndexViewModel();
                view.student_flight = flightDAO.GetFlight(sessionUser.user_id);
                return View("Views/Degree/PreDeparture/StudentFlight.cshtml", view);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool Edit(string degreeOrMobility = "", int? flight_id = null, string flight_number_a = "", DateTime? arrival_date_a = null, TimeSpan? arrival_time_a = null, string airport_departure_a = "", string airport_arrival_a = "", string flight_number_d = "", DateTime? arrival_date_d = null, TimeSpan? arrival_time_d = null, string airport_departure_d = "", string airport_arrival_d = "")
        {
            bool result = false;
            if (degreeOrMobility.Equals("Mobility"))
            {
                result = flightDAO.editFlightMobility(flight_id, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d);
            }
            else if (degreeOrMobility.Equals("Degree"))
            {
                result = flightDAO.editFlightDegree(flight_id, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a);
            }
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool NotificationDegree(int days_before, DateTime deadline)
        {
            bool result = flightDAO.SetupNotificationDegree(days_before, deadline);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool NotificationMobility(int days_before)
        {
            bool result = flightDAO.SetupNotificationMobility(days_before);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public IActionResult ExportToExcel()
        {
            List<Flight> flightsExcel = new List<Flight>();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Staff"))
            {
                flightsExcel = flightDAO.GetVisaLettersStaffToExcel(sessionUser.user_id);
            }
            else if (sessionUser.role_name.Equals("Admin"))
            {
                flightsExcel = flightDAO.GetVisaLettersAdminToExcel();
            }
            using (var wb = new XLWorkbook())
            {
                var currentRowDegree = 1;
                var currentRowMobility = 1;
                var ws = wb.Worksheets.Add("Degree_Student");
                ws.Cell(currentRowDegree, 1).Value = "Student Account";
                ws.Cell(currentRowDegree, 2).Value = "Student Name";
                ws.Cell(currentRowDegree, 3).Value = "Student Email";
                ws.Cell(currentRowDegree, 4).Value = "Flight Number (arrival)";
                ws.Cell(currentRowDegree, 5).Value = "Airport Arrival (arrival)";
                ws.Cell(currentRowDegree, 6).Value = "Airport Departure (arrival)";
                ws.Cell(currentRowDegree, 7).Value = "Arrival Date (arrival)";
                ws.Cell(currentRowDegree, 8).Value = "Arrival Time (arrival)";
                for (int i = 1; i < 9; i++)
                {
                    ws.Cell(currentRowDegree, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws.Cell(currentRowDegree, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws.Cell(currentRowDegree, i).Style.Font.Bold = true;
                    ws.Cell(currentRowDegree, i).Style.Font.FontSize = 12;
                    ws.Cell(currentRowDegree, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Column(i).Width = 40;
                }
                var ws1 = wb.Worksheets.Add("Mobility_Student");
                ws1.Cell(currentRowMobility, 1).Value = "Student Account";
                ws1.Cell(currentRowMobility, 2).Value = "Student Name";
                ws1.Cell(currentRowMobility, 3).Value = "Student Email";
                ws1.Cell(currentRowMobility, 4).Value = "Flight Number (arrival)";
                ws1.Cell(currentRowMobility, 5).Value = "Airport Arrival (arrival)";
                ws1.Cell(currentRowMobility, 6).Value = "Airport Departure (arrival)";
                ws1.Cell(currentRowMobility, 7).Value = "Arrival Date (arrival)";
                ws1.Cell(currentRowMobility, 8).Value = "Arrival Time (arrival)";
                ws1.Cell(currentRowMobility, 9).Value = "Flight Number (departure)";
                ws1.Cell(currentRowMobility, 10).Value = "Airport Arrival (departure)";
                ws1.Cell(currentRowMobility, 11).Value = "Airport Departure (departure)";
                ws1.Cell(currentRowMobility, 12).Value = "Arrival Date (departure)";
                ws1.Cell(currentRowMobility, 13).Value = "Arrival Time (departure)";
                for (int i = 1; i < 14; i++)
                {
                    ws1.Cell(currentRowMobility, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws1.Cell(currentRowMobility, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws1.Cell(currentRowMobility, i).Style.Font.Bold = true;
                    ws1.Cell(currentRowMobility, i).Style.Font.FontSize = 12;
                    ws1.Cell(currentRowMobility, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws1.Column(i).Width = 40;
                }

                foreach (var item in flightsExcel)
                {
                    if (item.role_name.Equals("Degree"))
                    {
                        currentRowDegree++;
                        ws.Cell(currentRowDegree, 1).Value = item.account;
                        ws.Cell(currentRowDegree, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 2).Value = item.fullname;
                        ws.Cell(currentRowDegree, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 3).Value = item.email;
                        ws.Cell(currentRowDegree, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 4).Value = String.IsNullOrEmpty(item.flight_number_a) ? "N/A" : item.flight_number_a;
                        ws.Cell(currentRowDegree, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 5).Value = String.IsNullOrEmpty(item.airport_arrival_a) ? "N/A" : item.airport_arrival_a;
                        ws.Cell(currentRowDegree, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 6).Value = String.IsNullOrEmpty(item.airport_departure_a) ? "N/A" : item.airport_departure_a;
                        ws.Cell(currentRowDegree, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 7).Value = item.arrival_date_a.HasValue ? item.arrival_date_a.Value.ToString("yyyy-MMM-dd") : "N/A";
                        ws.Cell(currentRowDegree, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 8).Value = item.arrival_time_a.HasValue ? item.arrival_time_a.Value.ToString(@"hh\:mm\:ss") : "N/A";
                        ws.Cell(currentRowDegree, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    }
                    else if (item.role_name.Equals("Mobility"))
                    {
                        currentRowMobility++;
                        ws1.Cell(currentRowMobility, 1).Value = item.account;
                        ws1.Cell(currentRowMobility, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 2).Value = item.fullname;
                        ws1.Cell(currentRowMobility, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 3).Value = item.email;
                        ws1.Cell(currentRowMobility, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 4).Value = String.IsNullOrEmpty(item.flight_number_a) ? "N/A" : item.flight_number_a;
                        ws1.Cell(currentRowMobility, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 5).Value = String.IsNullOrEmpty(item.airport_arrival_a) ? "N/A" : item.airport_arrival_a;
                        ws1.Cell(currentRowMobility, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 6).Value = String.IsNullOrEmpty(item.airport_departure_a) ? "N/A" : item.airport_departure_a;
                        ws1.Cell(currentRowMobility, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 7).Value = item.arrival_date_a.HasValue ? item.arrival_date_a.Value.ToString("yyyy-MMM-dd") : "N/A";
                        ws1.Cell(currentRowMobility, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 8).Value = item.arrival_time_a.HasValue ? item.arrival_time_a.Value.ToString(@"hh\:mm\:ss") : "N/A";
                        ws1.Cell(currentRowMobility, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 9).Value = String.IsNullOrEmpty(item.flight_number_d) ? "N/A" : item.flight_number_d;
                        ws1.Cell(currentRowMobility, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 10).Value = String.IsNullOrEmpty(item.airport_arrival_d) ? "N/A" : item.airport_arrival_d;
                        ws1.Cell(currentRowMobility, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 11).Value = String.IsNullOrEmpty(item.airport_departure_d) ? "N/A" : item.airport_departure_d;
                        ws1.Cell(currentRowMobility, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 12).Value = item.arrival_date_d.HasValue ? item.arrival_date_d.Value.ToString("yyyy-MMM-dd") : "N/A";
                        ws1.Cell(currentRowMobility, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 13).Value = item.arrival_time_d.HasValue ? item.arrival_time_d.Value.ToString(@"hh\:mm\:ss") : "N/A";
                        ws1.Cell(currentRowMobility, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    string excelName = $"FlightTicket-{DateTime.Now.ToString("yyyy-MMM-dd")}.xlsx";
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
        }

        [Authorize(Roles = "Degree,Mobility")]
        public IActionResult CreateOrEdit(int? flight_id, int student_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d, IFormFile picture_a, IFormFile picture_d)
        {
            string pictureName_a = "";
            string pictureName_d = "";
            if (picture_a != null)
            {
                pictureName_a = "flight_a_student_" + student_id + Path.GetExtension(picture_a.FileName);
                string imagePath = Path.Combine(hostingEnvironment.WebRootPath, "image");
                string flightPath = Path.Combine(imagePath, "Flight");
                string filePath = Path.Combine(flightPath, pictureName_a);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture_a.CopyTo(stream);
                stream.Close();
            }
            if (picture_d != null)
            {
                pictureName_d = "flight_d_student_" + student_id + Path.GetExtension(picture_d.FileName);
                string imagePath = Path.Combine(hostingEnvironment.WebRootPath, "image");
                string flightPath = Path.Combine(imagePath, "Flight");
                string filePath = Path.Combine(flightPath, pictureName_d);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture_d.CopyTo(stream);
                stream.Close();
            }
            bool result = flightDAO.CreateOrEditFlight(student_id, flight_id, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, pictureName_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d, pictureName_d);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }

        [Authorize(Roles = "Degree,Mobility")]
        public bool SkipNotification(int user_id)
        {
            bool result = flightDAO.SkipNotification(user_id);
            return result;
        }
    }
}
