using ISM.WebApp.DAO;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    [Authorize]
    public class PassportController : Controller
    {
        public PassportDAO passportDAO;
        public PassportController(PassportDAO passportDAO)
        {
            this.passportDAO = passportDAO;
        }
        public IActionResult Index(int id = 0, string account = "", string picture = "", string passport_number = "", string student_name = "",
            DateTime? start_dateFrom = null, DateTime? start_dateTo = null, DateTime? expired_dateFrom = null, DateTime? expired_dateTo = null,
            string issuing_authority = "", int page = 1)
        {
            PassportIndexViewModel passportIndexView = new PassportIndexViewModel();
            passportIndexView.page = page;
            passportIndexView.pageSize = 5;
            passportIndexView.totalPage = PagingUtils.calculateTotalPage(passportDAO.GetTotalPassports(id, account, picture,student_name, passport_number, start_dateFrom, start_dateTo, expired_dateFrom, expired_dateTo, issuing_authority), passportIndexView.pageSize);
            passportIndexView.passports = passportDAO.GetPassports(passportIndexView.page, passportIndexView.pageSize, id, account, picture, student_name, passport_number, start_dateFrom, start_dateTo, expired_dateFrom, expired_dateTo, issuing_authority);
            passportIndexView.passport_id = id;
            passportIndexView.account = account;
            passportIndexView.picture = picture;
            passportIndexView.passport_number = passport_number;
            passportIndexView.student_name = student_name;
            passportIndexView.start_dateFrom = start_dateFrom;
            passportIndexView.start_dateTo = start_dateTo;
            passportIndexView.expried_dateFrom = expired_dateFrom;
            passportIndexView.expried_dateTo = expired_dateTo;
            passportIndexView.issuing_authority = issuing_authority;
            return View("Views/Admin/Passport/Passport.cshtml", passportIndexView);
        }
    }
}
