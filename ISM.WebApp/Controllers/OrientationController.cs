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
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class OrientationController : Controller
    {
        public OrientationDAO _orientationDAO;
        public OrientationController(OrientationDAO orientationDAO)
        {
            _orientationDAO = orientationDAO;
        }
        public IActionResult Index(int page = 1, string account = "", string fullname = "")
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            int current_user_id = sessionUser.user_id;
            OrientationIndexViewModel viewModel = new OrientationIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(_orientationDAO.GetTotalDegreeStudent(isAdmin, current_user_id, account, fullname), viewModel.pageSize);
            viewModel.students = _orientationDAO.GetDegreeStudent(isAdmin,current_user_id,account,fullname,viewModel.page,viewModel.pageSize);
            viewModel.account = account;
            viewModel.fullname = fullname;
            return View("Views/Admin/Program/OrientationSchedule.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult SearchORT(int id, int page = 1, string content = "", DateTime? date = null, TimeSpan? time = null, string location = "", string require_document = "")
        {
            OrientationIndexViewModel viewModel = new OrientationIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(_orientationDAO.GetSearchTotalORT(id, content, date, time, location, require_document), viewModel.pageSize);
            viewModel.orientationSchedules = _orientationDAO.GetSearchOrientationSchedules(id, viewModel.page, viewModel.pageSize, content, date, time, location, require_document);
            viewModel.content = content;
            viewModel.date = date;
            viewModel.time = time;
            viewModel.location = location;
            viewModel.require_document = require_document;
            viewModel.student = _orientationDAO.GetStudentById(id);
            return View("Views/Admin/Program/OrientationScheduleForStudent.cshtml", viewModel);
        }

        public bool CreateORT(int id, string content, DateTime date, TimeSpan time, string location, string requirement)
        {
            bool result = _orientationDAO.createORTSchedule(id, content, date, time, location, requirement);
            return result;
        }

        public bool isExist(int id, string content, DateTime date, TimeSpan time, string location)
        {
            bool result = _orientationDAO.isORTAlreadyExist(id, content, date, time, location);
            return result;
        }

        public bool isSameTime(int id, DateTime date, TimeSpan time)
        {
            bool result = _orientationDAO.isSameTime(id, date, time);
            return result;
        }

        public bool EditORT(int id, string content, DateTime date, TimeSpan time, string location, string requirement)
        {
            bool result = _orientationDAO.editORTSchedule(id, content, date, time, location, requirement);
            return result;
        }

        public bool DeleteORT(int id)
        {
            bool result = _orientationDAO.deleteORTSchedule(id);
            return result;
        }

        public bool SetupNotification(int days_before)
        {
            bool result = _orientationDAO.SetupNotification(days_before);
            return result;
        }
    }
}
