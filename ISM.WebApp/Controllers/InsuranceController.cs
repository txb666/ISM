using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.DAO;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    public class InsuranceController : Controller
    {
        public InsuranceDAO insuranceDAO;

        public InsuranceController(InsuranceDAO insuranceDAO)
        {
            this.insuranceDAO = insuranceDAO;
        }
        public IActionResult Index(string degreeOrMobility, string fullname="", string account="", DateTime? startDateFrom=null, DateTime? startDateTo=null, DateTime? expiryDateFrom=null, DateTime? expiryDateTo=null, int page=1)
        {
            InsuranceIndexViewModel viewModel = new InsuranceIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = 5;
            viewModel.totalPage = PagingUtils.calculateTotalPage(insuranceDAO.getTotalInsurance(false, "Degree", true, 2, account, fullname, startDateFrom, startDateTo, expiryDateFrom, expiryDateTo), viewModel.pageSize);
            viewModel.insurances = insuranceDAO.getInsurance(false, "Degree", true, 2, viewModel.page, viewModel.pageSize, account, fullname, startDateFrom, startDateTo, expiryDateFrom, expiryDateTo);
            viewModel.fullname = fullname;
            viewModel.account = account;
            viewModel.startDateFrom = startDateFrom;
            viewModel.startDateTo = startDateTo;
            viewModel.expiryDateFrom = expiryDateFrom;
            viewModel.expiryDateTo = expiryDateTo;
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
