﻿using System;
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
    [Authorize]
    public class StaffController : Controller
    {
        public UserDAO userDAO;

        public StaffController(UserDAO u)
        {
            userDAO = u;
        }



        public IActionResult Index(string fullname = "", string email = "", string account = "", bool? status = null,DateTime? startDateFrom=null,DateTime? startDateTo=null,DateTime? endDateFrom=null, DateTime? endDateTo=null, int page = 1)
        {
            StaffIndexViewModel viewmodel = new StaffIndexViewModel();
            viewmodel.page = page;
            viewmodel.pageSize = 3;
            viewmodel.totalPage = PagingUtils.calculateTotalPage(userDAO.getTotalStaff(fullname,email,account,status,startDateFrom,startDateTo,endDateFrom,endDateTo),viewmodel.pageSize);
            viewmodel.staffs = userDAO.GetStaff(viewmodel.page, viewmodel.pageSize, fullname, email, account, status,startDateFrom,startDateTo,endDateFrom,endDateTo);
            viewmodel.fullname = fullname;
            viewmodel.email = email;
            viewmodel.account = account;
            viewmodel.status = status;
            viewmodel.startDateFrom = startDateFrom;
            viewmodel.startDateTo = startDateTo;
            viewmodel.endDateFrom = endDateFrom;
            viewmodel.endDateTo = endDateTo;
            return View("Views/Admin/Staff/Staff.cshtml", viewmodel);
        }
    }
}
