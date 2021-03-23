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
    public class InsuranceController : Controller
    {
        public InsuranceDAO insuranceDAO;

        public InsuranceController(InsuranceDAO insuranceDAO)
        {
            this.insuranceDAO = insuranceDAO;
        }
        public IActionResult Index(string degreeOrMobility="", string fullname="", string account="", DateTime? startDate=null, DateTime? expiryDate=null , int page=1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            bool haveDegree = isAdmin==true?true:sessionUser.haveDegree;
            int sessionUser_id = sessionUser.user_id;
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
            InsuranceIndexViewModel viewModel = new InsuranceIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(insuranceDAO.getTotalInsurance(isAdmin, degreeOrMobility, haveDegree, sessionUser_id, account, fullname, startDate, expiryDate), viewModel.pageSize);
            viewModel.insurances = insuranceDAO.getInsurance(isAdmin, degreeOrMobility, haveDegree, sessionUser_id, viewModel.page, viewModel.pageSize, account, fullname, startDate, expiryDate);
            viewModel.fullname = fullname;
            viewModel.account = account;
            viewModel.startDate = startDate;
            viewModel.expiryDate = expiryDate;
            viewModel.degreeOrMobility = degreeOrMobility;
            return View("Views/Admin/Insurance/StudentInsurance.cshtml",viewModel);
        }

        public bool Edit(int id, DateTime startDate, DateTime expiryDate)
        {
            bool result = insuranceDAO.editInsurance(id, startDate, expiryDate);
            return result;
        }

        public IActionResult HowTo()
        {
            return View("Views/Admin/Insurance/HowTo.cshtml");
        }
    }
}
