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
    public class PassportController : Controller
    {
        public PassportDAO passportDAO;
        public PassportController(PassportDAO passportDAO)
        {
            this.passportDAO = passportDAO;
        }
        public IActionResult Index(string degreeOrMobility="", string account="", string fullname="", string passport_number="", DateTime? start_date=null, DateTime? expired_date=null, string issuing_authority="", int page=1)
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
            PassportIndexViewModel viewModel = new PassportIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(passportDAO.GetTotalPassports(isAdmin,degreeOrMobility,haveDegree,current_staff_id,fullname,account,passport_number,start_date,expired_date,issuing_authority), viewModel.pageSize);
            viewModel.passports = passportDAO.GetPassports(isAdmin,degreeOrMobility,haveDegree,current_staff_id,viewModel.page,viewModel.pageSize,fullname,account,passport_number,start_date,expired_date,issuing_authority);
            viewModel.degreeOrMobility = degreeOrMobility;
            viewModel.account = account;
            viewModel.fullname = fullname;
            viewModel.passport_number = passport_number;
            viewModel.start_date = start_date;
            viewModel.expired_date = expired_date;
            viewModel.issuing_authority = issuing_authority;
            return View("Views/Admin/Passport/Passport.cshtml", viewModel);
        }

        public bool Edit(int passport_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority)
        {
            bool result = passportDAO.editPassport(passport_id, passport_number, start_date, expired_date, issuing_authority);
            return result;
        }
    }
}
