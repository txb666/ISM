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
    public class VisaController : Controller
    {
        public VisaDAO visaDAO;
        public VisaController(VisaDAO visaDAO)
        {
            this.visaDAO = visaDAO;
        }
        public IActionResult Index(string degreeOrMobility = "", string fullname="", string account="", string entry_port="", DateTime? start_date=null, DateTime? expired_date=null, DateTime? date_entry=null, int page=1)
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
            VisaIndexViewModel visaIndexView = new VisaIndexViewModel();
            visaIndexView.page = page;
            visaIndexView.pageSize = 5;
            visaIndexView.totalPage = PagingUtils.calculateTotalPage(visaDAO.GetTotalVisa(isAdmin,haveDegree,degreeOrMobility,current_staff_id,account,fullname,start_date,expired_date,date_entry,entry_port), visaIndexView.pageSize);
            visaIndexView.visalist = visaDAO.GetVisa(isAdmin, haveDegree, degreeOrMobility, current_staff_id, visaIndexView.page, visaIndexView.pageSize, account, fullname, start_date, expired_date, date_entry, entry_port);
            visaIndexView.account = account;
            visaIndexView.fullname = fullname;
            visaIndexView.entry_port = entry_port;
            visaIndexView.start_date = start_date;
            visaIndexView.expired_date = expired_date;
            visaIndexView.date_entry = date_entry;
            visaIndexView.degreeOrMobility = degreeOrMobility;
            return View("Views/Admin/Visa/Visa.cshtml", visaIndexView);
        }

        public bool Edit(int visa_id, DateTime start_date, DateTime expired_date, DateTime entry_date, string entry_port)
        {
            bool result = visaDAO.editVisa(visa_id,start_date,expired_date,entry_date,entry_port);
            return result;
        }
    }
}
