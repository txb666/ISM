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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class StudentController : Controller
    {
        public UserDAO userDAO;
        public CampusDAO campusDAO;
        public ProgramDAO programDAO;
        public StudentGroupDAO studentGroupDAO;
        public StudentController(UserDAO userDAO, CampusDAO campusDAO, ProgramDAO programDAO, StudentGroupDAO studentGroupDAO)
        {
            this.userDAO = userDAO;
            this.campusDAO = campusDAO;
            this.programDAO = programDAO;
            this.studentGroupDAO = studentGroupDAO;
        }
        public IActionResult Index(string degreeOrMobility="", string fullname="", string account="", string email="", string nationality="", DateTime? dob=null, bool? gender=null, int? campus_id=null, int? program_id=null, string emergency_contact="", string home_univercity="", string accomodation="", bool? status=null, DateTime? program_duration_start=null, DateTime? program_duration_end=null, int page=1)
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
            StudentIndexViewModel viewModel = new StudentIndexViewModel();
            viewModel.degreeOrMobility = degreeOrMobility;
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(userDAO.getTotalStudent(isAdmin,haveDegree,degreeOrMobility,current_staff_id,fullname,account,email,nationality,dob,gender,campus_id,program_id,emergency_contact,home_univercity,accomodation,status,program_duration_start,program_duration_end),viewModel.pageSize);
            viewModel.students = userDAO.getStudent(isAdmin, haveDegree, degreeOrMobility, current_staff_id, viewModel.page, viewModel.pageSize, fullname, account, email, nationality, dob, gender, campus_id, program_id, emergency_contact, home_univercity, accomodation, status, program_duration_start, program_duration_end);
            viewModel.fullname = fullname;
            viewModel.account = account;
            viewModel.email = email;
            viewModel.nationality = nationality;
            viewModel.dob = dob;
            viewModel.gender = gender;
            viewModel.campusList = campusDAO.getAllCampus();
            viewModel.programList = programDAO.getAllProgram();
            viewModel.campus_id = campus_id;
            viewModel.program_id = program_id;
            viewModel.emergency_contact = emergency_contact;
            viewModel.home_univercity = home_univercity;
            viewModel.accomodation = accomodation;
            viewModel.status = status;
            viewModel.program_duration_start = program_duration_start;
            viewModel.program_duration_end = program_duration_end;
            viewModel.manageGroup = studentGroupDAO.GetStudentGroupByStaff(current_staff_id,isAdmin,degreeOrMobility);
            return View("Views/Admin/Student/Student.cshtml",viewModel);
        }

        public bool Create(string degreeOrMobility, string fullname, string email, string account, int student_group_id, bool status)
        {
            bool result = userDAO.createStudent(degreeOrMobility, fullname, account, email, student_group_id, status);
            return result;
        }

        public bool IsStudentExist(string account, string email)
        {
            bool result = userDAO.isStaffAlreadyExist(account, email);
            return result;
        }

        public bool isEmailExist(string email)
        {
            bool result = userDAO.isEmailExist(email);
            return result;
        }

        public bool Edit(int id, string fullname, string email, bool status, string originalEmail)
        {
            bool result = userDAO.editStudent(id, fullname, email, status, originalEmail);
            return result;
        }

        public bool CreateAccountNotification(string account, string email)
        {
            bool result = userDAO.CreateAccountNotification(account, email);
            return result;
        }

        public IActionResult ExportToExcel()
        {
            List<User> studentExcel = new List<User>();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Staff"))
            {
                studentExcel = userDAO.GetVisaLettersStaffToExcel(sessionUser.user_id);
            }
            else if (sessionUser.role_name.Equals("Admin"))
            {
                studentExcel = userDAO.GetVisaLettersAdminToExcel();
            }
            using (var wb = new XLWorkbook())
            {
                var currentRowDegree = 1;
                var currentRowMobility = 1;
                var ws = wb.Worksheets.Add("Degree_Student");
                ws.Cell(currentRowDegree, 1).Value = "Student Account";
                ws.Cell(currentRowDegree, 2).Value = "Student Name";
                ws.Cell(currentRowDegree, 3).Value = "Student Email";
                ws.Cell(currentRowDegree, 4).Value = "Nationality";
                ws.Cell(currentRowDegree, 5).Value = "Date of Birth";
                ws.Cell(currentRowDegree, 6).Value = "Gender";
                ws.Cell(currentRowDegree, 7).Value = "Program";
                ws.Cell(currentRowDegree, 8).Value = "Campus";
                ws.Cell(currentRowDegree, 9).Value = "Accommodation";
                ws.Cell(currentRowDegree, 10).Value = "Emergency Contact";
                ws.Cell(currentRowDegree, 11).Value = "Status";
                for (int i = 1; i < 12; i++)
                {
                    ws.Cell(currentRowDegree, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws.Cell(currentRowDegree, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws.Cell(currentRowDegree, i).Style.Font.Bold = true;
                    ws.Cell(currentRowDegree, i).Style.Font.FontSize = 12;
                    ws.Cell(currentRowDegree, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Column(i).Width = 30;
                }
                var ws1 = wb.Worksheets.Add("Mobility_Student");
                ws1.Cell(currentRowMobility, 1).Value = "Student Account";
                ws1.Cell(currentRowMobility, 2).Value = "Student Name";
                ws1.Cell(currentRowMobility, 3).Value = "Student Email";
                ws1.Cell(currentRowMobility, 4).Value = "Nationality";
                ws1.Cell(currentRowMobility, 5).Value = "Date of Birth";
                ws1.Cell(currentRowMobility, 6).Value = "Gender";
                ws1.Cell(currentRowMobility, 7).Value = "Program";
                ws1.Cell(currentRowMobility, 8).Value = "Campus";
                ws1.Cell(currentRowMobility, 9).Value = "Home University";
                ws1.Cell(currentRowMobility, 10).Value = "Accommodation";
                ws1.Cell(currentRowMobility, 11).Value = "Emergency Contact";
                ws1.Cell(currentRowMobility, 12).Value = "Status";
                for (int i = 1; i < 13; i++)
                {
                    ws1.Cell(currentRowMobility, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws1.Cell(currentRowMobility, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws1.Cell(currentRowMobility, i).Style.Font.Bold = true;
                    ws1.Cell(currentRowMobility, i).Style.Font.FontSize = 12;
                    ws1.Cell(currentRowMobility, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws1.Column(i).Width = 30;
                }

                foreach (var item in studentExcel)
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
                        ws.Cell(currentRowDegree, 4).Value = String.IsNullOrEmpty(item.nationality) ? "N/A" : item.nationality;
                        ws.Cell(currentRowDegree, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 5).Value = item.dob.HasValue ? item.dob.Value.ToString("yyyy-MMM-dd") : "N/A";
                        ws.Cell(currentRowDegree, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 6).Value = !item.gender == true ? (item.gender == false ? "Female" : "N/A") : "Male";
                        ws.Cell(currentRowDegree, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 7).Value = String.IsNullOrEmpty(item.program) ? "N/A" : item.program;
                        ws.Cell(currentRowDegree, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 8).Value = String.IsNullOrEmpty(item.campus) ? "N/A" : item.campus;
                        ws.Cell(currentRowDegree, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 9).Value = String.IsNullOrEmpty(item.accomodation) ? "N/A" : item.accomodation;
                        ws.Cell(currentRowDegree, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 10).Value = String.IsNullOrEmpty(item.emergency_contact) ? "N/A" : item.emergency_contact;
                        ws.Cell(currentRowDegree, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRowDegree, 11).Value = item.status == true ? "Active" : "Inactive";
                        ws.Cell(currentRowDegree, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        ws1.Cell(currentRowMobility, 4).Value = String.IsNullOrEmpty(item.nationality) ? "N/A" : item.nationality;
                        ws1.Cell(currentRowMobility, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 5).Value = item.dob.HasValue ? item.dob.Value.ToString("yyyy-MMM-dd") : "N/A";
                        ws1.Cell(currentRowMobility, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 6).Value = !item.gender == true ? (item.gender == false ? "Female" : "N/A") : "Male";
                        ws1.Cell(currentRowMobility, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 7).Value = String.IsNullOrEmpty(item.program) ? "N/A" : item.program;
                        ws1.Cell(currentRowMobility, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 8).Value = String.IsNullOrEmpty(item.campus) ? "N/A" : item.campus;
                        ws1.Cell(currentRowMobility, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 9).Value = String.IsNullOrEmpty(item.home_univercity) ? "N/A" : item.home_univercity;
                        ws1.Cell(currentRowMobility, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 10).Value = String.IsNullOrEmpty(item.accomodation) ? "N/A" : item.accomodation;
                        ws1.Cell(currentRowMobility, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 11).Value = String.IsNullOrEmpty(item.emergency_contact) ? "N/A" : item.emergency_contact;
                        ws1.Cell(currentRowMobility, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws1.Cell(currentRowMobility, 12).Value = item.status == true ? "Active" : "Inactive";
                        ws1.Cell(currentRowMobility, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    string excelName = $"Student-{DateTime.Now.ToString("yyyy-MMM-dd")}.xlsx";
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
        }
    }
}
