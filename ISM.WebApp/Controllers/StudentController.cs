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
    }
}
