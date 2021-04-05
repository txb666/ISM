using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ISM.WebApp.Utils;

namespace ISM.WebApp.Controllers
{
    public class MeetingController : Controller
    {
        public MeetingDAO _meetingDAO;
        public MeetingController(MeetingDAO meetingDAO)
        {
            _meetingDAO = meetingDAO;
        }
        public IActionResult Index(int page = 1, DateTime? date = null, TimeSpan? start_time = null, TimeSpan? end_time = null)
        {
            MeetingAvailableTimeIndexViewModel viewModel = new MeetingAvailableTimeIndexViewModel();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            int staff_id = sessionUser.user_id;
            viewModel.page = page;
            viewModel.pageSize = pagingConst.PAGE_SIZE;
            viewModel.totalPage = PagingUtils.calculateTotalPage(_meetingDAO.GetTotalAvailableTime(staff_id, date, start_time, end_time), viewModel.pageSize);
            viewModel.availableTimes = _meetingDAO.GetAvailableTimes(viewModel.page, viewModel.pageSize, staff_id, date, start_time, end_time);
            viewModel.date = date;
            viewModel.start_time = start_time;
            viewModel.end_time = end_time;
            return View("Views/Admin/ContactUs/Available_Time.cshtml", viewModel);
        }

        public bool CreateMAT(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            bool result = _meetingDAO.CreateMAT(staff_id, date, start_time, end_time);
            return result;
        }

        public bool EditMAT(int mat_id, int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            bool result = _meetingDAO.EditMAT(mat_id, staff_id, date, start_time, end_time);
            return result;
        }

        public bool DeleteMAT(int mat_id, int staff_id)
        {
            bool result = _meetingDAO.DeleteMAT(mat_id, staff_id);
            return result;
        }
    }
}
