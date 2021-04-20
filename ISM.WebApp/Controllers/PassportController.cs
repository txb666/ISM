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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
    public class PassportController : Controller
    {
        public PassportDAO passportDAO;
        private readonly IWebHostEnvironment hostingEnvironment;
        public PassportController(PassportDAO passportDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.passportDAO = passportDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string degreeOrMobility="", string account="", string fullname="", string passport_number="", DateTime? start_date=null, DateTime? expired_date=null, string issuing_authority="", int page=1)
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
                PassportIndexViewModel viewModel = new PassportIndexViewModel();
                viewModel.page = page;
                viewModel.pageSize = 5;
                viewModel.totalPage = PagingUtils.calculateTotalPage(passportDAO.GetTotalPassports(isAdmin, degreeOrMobility, haveDegree, current_staff_id, fullname, account, passport_number, start_date, expired_date, issuing_authority), viewModel.pageSize);
                viewModel.passports = passportDAO.GetPassports(isAdmin, degreeOrMobility, haveDegree, current_staff_id, viewModel.page, viewModel.pageSize, fullname, account, passport_number, start_date, expired_date, issuing_authority);
                viewModel.degreeOrMobility = degreeOrMobility;
                viewModel.account = account;
                viewModel.fullname = fullname;
                viewModel.passport_number = passport_number;
                viewModel.start_date = start_date;
                viewModel.expired_date = expired_date;
                viewModel.issuing_authority = issuing_authority;
                return View("Views/Admin/Passport/Passport.cshtml", viewModel);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                PassportIndexViewModel view = new PassportIndexViewModel();
                view.student_passport = passportDAO.GetPassport(sessionUser.user_id);
                return View("Views/Degree/Passport/Passport.cshtml", view);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool Edit(int passport_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority)
        {
            bool result = passportDAO.editPassport(passport_id, passport_number, start_date, expired_date, issuing_authority);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool CreateOrEditNotificationConfig(int days_before)
        {
            bool result = passportDAO.CreateOrEdit(days_before);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public IActionResult ExportToExcel()
        {
            List<Passport> passportExcel = new List<Passport>();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Staff"))
            {
                passportExcel = passportDAO.GetVisaLettersStaffToExcel(sessionUser.user_id);
            }
            else if (sessionUser.role_name.Equals("Admin"))
            {
                passportExcel = passportDAO.GetVisaLettersAdminToExcel();
            }
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Passport");
                var currentRow = 1;
                ws.Cell(currentRow, 1).Value = "Student Name";
                ws.Cell(currentRow, 2).Value = "Student Email";
                ws.Cell(currentRow, 3).Value = "Passport Number";
                ws.Cell(currentRow, 4).Value = "Start Date";
                ws.Cell(currentRow, 5).Value = "Expire Date";
                ws.Cell(currentRow, 6).Value = "Issuing Authority";
                for (int i = 1; i < 7; i++)
                {
                    ws.Cell(currentRow, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws.Cell(currentRow, i).Style.Font.Bold = true;
                    ws.Cell(currentRow, i).Style.Font.FontSize = 12;
                    ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Column(i).Width = 30;
                }

                foreach (var item in passportExcel)
                {
                    currentRow++;
                    ws.Cell(currentRow, 1).Value = item.fullname;
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 2).Value = item.email;
                    ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 3).Value = String.IsNullOrEmpty(item.passport_number) ? "N/A" : item.passport_number;
                    ws.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 4).Value = item.start_date.HasValue? item.start_date.Value.ToString("yyyy-MMM-dd") : "N/A";
                    ws.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 5).Value = item.expired_date.HasValue ? item.expired_date.Value.ToString("yyyy-MMM-dd") : "N/A";
                    ws.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 6).Value = String.IsNullOrEmpty(item.issuing_authority) ? "N/A" : item.issuing_authority;
                    ws.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                }

                using (var stream = new MemoryStream())
                {
                    string excelName = $"Passport-{DateTime.Now.ToString("yyyy-MMM-dd")}.xlsx";
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
        }

        [Authorize(Roles = "Degree,Mobility")]
        public IActionResult CreateOrEdit(int student_id, int? passport_id, string passport_number, string issuing_authority, DateTime start_date, DateTime expired_date, IFormFile picture)
        {
            string pictureName = "";
            if (picture != null)
            {
                pictureName = "passport_student_" + student_id + Path.GetExtension(picture.FileName);
                string imagePath = Path.Combine(hostingEnvironment.WebRootPath, "image");
                string passportPath = Path.Combine(imagePath, "Passport");
                string filePath = Path.Combine(passportPath, pictureName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture.CopyTo(stream);
                stream.Close();
            }
            bool result = passportDAO.CreateOrEditPassport(student_id, passport_id, pictureName, passport_number, start_date, expired_date, issuing_authority);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }
    }
}
