using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ISM.WebApp.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
    public class MeetingController : Controller
    {
        public MeetingDAO _meetingDAO;
        public MeetingController(MeetingDAO meetingDAO)
        {
            _meetingDAO = meetingDAO;
        }
        public IActionResult Index(int page = 1, DateTime? date = null, TimeSpan? start_time = null, TimeSpan? end_time = null, string staff_name = "", string staff_email = "")
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                MeetingAvailableTimeIndexViewModel viewModel = new MeetingAvailableTimeIndexViewModel();
                int staff_id = sessionUser.user_id;
                viewModel.page = page;
                viewModel.pageSize = pagingConst.PAGE_SIZE;
                viewModel.totalPage = PagingUtils.calculateTotalPage(_meetingDAO.GetTotalAvailableTime(staff_id, date, start_time, end_time), viewModel.pageSize);
                viewModel.availableTimes = _meetingDAO.GetAvailableTimes(viewModel.page, viewModel.pageSize, staff_id, date, start_time, end_time);
                viewModel.date = date;
                viewModel.start_time = start_time;
                viewModel.end_time = end_time;
                viewModel.meetingRegisters = _meetingDAO.GetMeetingRegister(staff_id);
                viewModel.meetingSchedules = _meetingDAO.GetMeetingSchedule(staff_id);
                return View("Views/Admin/ContactUs/AvailableTime.cshtml", viewModel);
            }
            else if (sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                StaffIndexViewModel viewModel = new StaffIndexViewModel();
                int student_id = sessionUser.user_id;
                viewModel.page = page;
                viewModel.pageSize = pagingConst.PAGE_SIZE;
                viewModel.totalPage = PagingUtils.calculateTotalPage(_meetingDAO.GetTotalStaff(student_id, staff_name, staff_email), viewModel.pageSize);
                viewModel.staffs = _meetingDAO.GetStaff(viewModel.page,viewModel.pageSize,student_id,staff_name,staff_email);
                viewModel.fullname = staff_name;
                viewModel.email = staff_email;
                return View("Views/Degree/ContactUs/AvailableTime.cshtml", viewModel);
            }
            return View();
        }

        [Authorize(Roles = "Degree,Mobility")]
        [HttpGet]
        public IActionResult StaffAvailableTime(int id, int page = 1, DateTime? date = null, TimeSpan? start_time = null, TimeSpan? end_time = null)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            MeetingAvailableTimeIndexViewModel viewModel = new MeetingAvailableTimeIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = pagingConst.PAGE_SIZE;
            viewModel.totalPage = PagingUtils.calculateTotalPage(_meetingDAO.GetTotalAvailableTime(id, date, start_time, end_time), viewModel.pageSize);
            viewModel.availableTimes = _meetingDAO.GetAvailableTimes(viewModel.page, viewModel.pageSize, id, date, start_time, end_time);
            viewModel.date = date;
            viewModel.start_time = start_time;
            viewModel.end_time = end_time;
            viewModel.meetingSchedules = _meetingDAO.GetStudentMeetingSchedule(sessionUser.user_id);
            viewModel.staff = _meetingDAO.GetStaffById(id);
            return View("Views/Degree/ContactUs/BookMeeting.cshtml", viewModel);
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool CreateMAT(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            bool result = _meetingDAO.CreateMAT(staff_id, date, start_time, end_time);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool EditMAT(int mat_id, int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            bool result = _meetingDAO.EditMAT(mat_id, staff_id, date, start_time, end_time);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool DeleteMAT(int mat_id, int staff_id)
        {
            bool result = _meetingDAO.DeleteMAT(mat_id, staff_id);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool CheckMAT(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            bool result = _meetingDAO.isSameTime(staff_id, date, start_time, end_time);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool AcceptMR(int ms_id)
        {
            bool result = _meetingDAO.AcceptMeetingRegister(ms_id);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool SetupNotification(int days_before)
        {
            bool result = _meetingDAO.SetupNotification(days_before);
            return result;
        }

        [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
        public bool isExist(int staff_id, int student_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            bool result = _meetingDAO.isExist(staff_id,student_id,date,start_time,end_time);
            return result;
        }

        [Authorize(Roles = "Degree,Mobility")]
        public bool BookAMeeting(int staff_id, int student_id, DateTime date, TimeSpan start_time, TimeSpan end_time, string note)
        {
            bool result = _meetingDAO.BookAMeeting(staff_id, student_id, date, start_time, end_time, note);
            return result;
        }
    }
}