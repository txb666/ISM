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
    //[Authorize]
    public class VisaController : Controller
    {
        public VisaDAO visaDAO;
        public VisaController(VisaDAO visaDAO)
        {
            this.visaDAO = visaDAO;
        }
        public IActionResult Index(string account = "", string student_name = "", string picture = "",
            string entry_port = "", DateTime? start_dateFrom = null, DateTime? start_dateTo = null, DateTime? expired_dateFrom = null, DateTime? expired_dateTo = null,
            DateTime? entry_dateFrom = null, DateTime? entry_dateTo = null, int page = 1)
        {
            VisaIndexViewModel visaIndexView = new VisaIndexViewModel();
            visaIndexView.page = page;
            visaIndexView.pageSize = 5;
            visaIndexView.totalPage = PagingUtils.calculateTotalPage(visaDAO.GetTotalVisa(account, picture, student_name, start_dateFrom, start_dateTo, expired_dateFrom, expired_dateTo, entry_dateFrom, entry_dateTo, entry_port), visaIndexView.pageSize);
            visaIndexView.visalist = visaDAO.GetVisa(visaIndexView.page, visaIndexView.pageSize, account, picture, student_name, start_dateFrom, start_dateTo, expired_dateFrom, expired_dateTo, entry_dateFrom, entry_dateTo, entry_port);
            visaIndexView.account = account;
            visaIndexView.student_name = student_name;
            visaIndexView.picture = picture;
            visaIndexView.entry_port = entry_port;
            visaIndexView.start_dateFrom = start_dateFrom;
            visaIndexView.start_dateTo = start_dateTo;
            visaIndexView.expired_dateFrom = expired_dateFrom;
            visaIndexView.expired_dateTo = expired_dateTo;
            visaIndexView.entry_dateFrom = entry_dateFrom;
            visaIndexView.entry_dateTo = entry_dateTo;
            return View("Views/Admin/Visa/Visa.cshtml", visaIndexView);
        }

        public bool Edit(int visa_id, DateTime start_date, DateTime expired_date, DateTime entry_date, string entry_port)
        {
            bool result = visaDAO.editVisa(visa_id,start_date,expired_date,entry_date,entry_port);
            return result;
        }
    }
}
