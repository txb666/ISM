using ClosedXML.Excel;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
    public class VisaLetterController : Controller
    {
        public VisaLetterDAO VisaLetterDAO;
        public VisaLetterController(VisaLetterDAO visaLetterDAO)
        {
            this.VisaLetterDAO = visaLetterDAO;
        }
        public IActionResult Index(string degreeOrMobility="", string fullname = "", bool gender = true, string apply_receive = "", string visa_period = "", string type_visa = "", string nationality = "",string visa_type ="", string passport_number = "", string student_name = "", DateTime? dob= null, DateTime? expired_date=null, int page=1)
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
                VisaLetterIndexViewModel visaLetterIndexView = new VisaLetterIndexViewModel();
                visaLetterIndexView.page = page;
                visaLetterIndexView.pageSize = pagingConst.PAGE_SIZE;
                visaLetterIndexView.degreeOrMobility = degreeOrMobility;
                visaLetterIndexView.totalPage = PagingUtils.calculateTotalPage(VisaLetterDAO.GetTotalVisaLetter(isAdmin, haveDegree, degreeOrMobility, current_staff_id, fullname, apply_receive, visa_period, visa_type, nationality, passport_number, dob, expired_date), visaLetterIndexView.pageSize);
                visaLetterIndexView.VisaLetters = VisaLetterDAO.GetVisaLetter(isAdmin, haveDegree, degreeOrMobility, current_staff_id, visaLetterIndexView.page, visaLetterIndexView.pageSize, fullname, apply_receive, visa_period, visa_type, nationality, passport_number, dob, expired_date);
                visaLetterIndexView.fullname = fullname;
                visaLetterIndexView.gender = gender;
                visaLetterIndexView.dob = dob;
                visaLetterIndexView.nationality = nationality;
                visaLetterIndexView.passport_number = passport_number;
                visaLetterIndexView.expired_date = expired_date;
                visaLetterIndexView.visa_type = visa_type;
                visaLetterIndexView.visa_period = visa_period;
                visaLetterIndexView.apply_receive = apply_receive;
                return View("Views/Admin/Visa/PreApprovalVisa.cshtml", visaLetterIndexView);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                VisaLetterIndexViewModel view = new VisaLetterIndexViewModel();
                view.studentVisaLetter = VisaLetterDAO.GetVisaLetter(sessionUser.user_id);
                return View("Views/Degree/Visa/PreApprovalVisaLetter.cshtml", view);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool edit(int id, string visa_type,string visa_period,string apply_receive)
        {
            bool result = VisaLetterDAO.editVisaLetter(id, visa_type, visa_period, apply_receive);
            return result;
        }

        [Authorize(Roles = "Degree,Mobility")]
        public bool CreateOrEdit(int student_id, int visa_letter_id, string visa_type, string visa_period, string apply_receive)
        {
            bool result = VisaLetterDAO.CreateOrEditVisaLetter(student_id, visa_letter_id, visa_type, visa_period, apply_receive);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public IActionResult ExportToExcel()
        {
            List<VisaLetter> visaLetterExcel = new List<VisaLetter>();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Staff"))
            {
                visaLetterExcel = VisaLetterDAO.GetVisaLettersStaffToExcel(sessionUser.user_id);
            }
            else if (sessionUser.role_name.Equals("Admin"))
            {
                visaLetterExcel = VisaLetterDAO.GetVisaLettersAdminToExcel();
            }
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Visa Letter");
                var currentRow = 1;
                ws.Cell(currentRow, 1).Value = "Student Name";
                ws.Cell(currentRow, 2).Value = "Student Email";
                ws.Cell(currentRow, 3).Value = "Passport Number";
                ws.Cell(currentRow, 4).Value = "Passport Expire Date";
                ws.Cell(currentRow, 5).Value = "Visa Type";
                ws.Cell(currentRow, 6).Value = "Visa Period";
                for (int i = 1; i < 7; i++)
                {
                    ws.Cell(currentRow, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws.Cell(currentRow, i).Style.Font.Bold = true;
                    ws.Cell(currentRow, i).Style.Font.FontSize = 12;
                    ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Column(i).Width = 30;
                }

                foreach (var item in visaLetterExcel)
                {
                    currentRow++;
                    ws.Cell(currentRow, 1).Value = item.fullname;
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 2).Value = item.email;
                    ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 3).Value = String.IsNullOrEmpty(item.passport_number) ? "N/A" : item.passport_number;
                    ws.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 4).Value = item.expired_date.HasValue ? item.expired_date.Value.ToString("yyyy-MMM-dd") : "N/A";
                    ws.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 5).Value = String.IsNullOrEmpty(item.visa_type) ? "N/A" : item.visa_type;
                    ws.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 6).Value = String.IsNullOrEmpty(item.visa_period) ? "N/A" : item.visa_period;
                    ws.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                }

                using (var stream = new MemoryStream())
                {
                    string excelName = $"VisaLetter-{DateTime.Now.ToString("yyyy-MMM-dd")}.xlsx";
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
        }
    }
}
