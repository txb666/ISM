using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        public UserDAO userDAO;

        public StaffController(UserDAO u)
        {
            userDAO = u;
        }

        public IActionResult Index(string fullname = "", string email = "", string account = "", bool? status = null,DateTime? startDate=null,DateTime? endDate=null, int page = 1)
        {
            StaffIndexViewModel viewmodel = new StaffIndexViewModel();
            viewmodel.page = page;
            viewmodel.pageSize = 5;
            viewmodel.totalPage = PagingUtils.calculateTotalPage(userDAO.getTotalStaff(fullname,email,account,status,startDate,endDate), viewmodel.pageSize);
            viewmodel.staffs = userDAO.GetStaff(viewmodel.page, viewmodel.pageSize, fullname, email, account, status,startDate,endDate);
            viewmodel.fullname = fullname;
            viewmodel.email = email;
            viewmodel.account = account;
            viewmodel.status = status;
            viewmodel.startDate = startDate;
            viewmodel.endDate = endDate;
            return View("Views/Admin/Staff/Staff.cshtml", viewmodel);
        }

        public int Create(string fullname, string email, string account, DateTime? startDate, DateTime? endDate, bool status)
        {
            int result=userDAO.createStaff(fullname, email, account, startDate, endDate, status);
            /*return RedirectToAction("Index");*/
            return result;
        }

        public bool IsStaffAlreadyExist(string account, string email)
        {
            bool result = userDAO.isStaffAlreadyExist(account, email);
            return result;
        }

        public bool isEmailExist(string email)
        {
            bool result = userDAO.isEmailExist(email);
            return result;
        }

        public bool Edit(int id, string fullname, string email, DateTime? startDate, DateTime? endDate, bool status, string originalEmail)
        {
            bool result = userDAO.editStaff(id, fullname, email, startDate, endDate, status, originalEmail);
            return result;
        }

        public bool CreateAccountNotification(string account, string email)
        {
            bool result = userDAO.CreateAccountNotification(account, email);
            return result;
        }
    }
}
