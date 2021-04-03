using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class InsuranceController : Controller
    {
        public InsuranceDAO insuranceDAO;
        private readonly IWebHostEnvironment hostingEnvironment;

        public InsuranceController(InsuranceDAO insuranceDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.insuranceDAO = insuranceDAO;
            this.hostingEnvironment = hostingEnvironment;
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

        public bool NotificationDegree(int days_before, DateTime deadline)
        {
            bool result = insuranceDAO.SetupNotificationDegree(days_before, deadline);
            return result;
        }

        public bool NotificationMobility(int days_before)
        {
            bool result = insuranceDAO.SetupNotificationMobility(days_before);
            return result;
        }

        [HttpGet]
        public IActionResult HowTo()
        {
            return View("Views/Admin/Insurance/HowTo.cshtml");
        }

        [HttpPost]
        public IActionResult HowToPost(IFormFile uploadFile)
        {
            try
            {
                if (uploadFile != null)
                {
                    string filename = "HowTo.pdf";
                    string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                    string filePath = Path.Combine(article, filename);
                    FileStream stream = new FileStream(filePath, FileMode.Create);
                    uploadFile.CopyTo(stream);
                    stream.Close();
                }
            }
            catch(Exception e)
            {
                ErrorViewModel ev = new ErrorViewModel();
                ev.message = e.Message;
                ev.stackTrace = e.StackTrace;
                return View("Views/Shared/Error.cshtml",ev);
            }
            return RedirectToAction("Howto");
        }

        public IActionResult Instruction()
        {
           return View("Views/Admin/Insurance/InsuranceInstruction.cshtml");
        }

        [HttpGet]
        public IActionResult InsuranceClaim()
        {
            return View("Views/Admin/Insurance/GuidanceInsuranceClaim.cshtml");
        }

        [HttpPost]
        public IActionResult InsuranceClaimPost(IFormFile uploadFile)
        {
            if (uploadFile != null)
            {
                string contentType = uploadFile.ContentType;
                string filename = "Guidance for Insurance Claim.pdf";
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string filePath = Path.Combine(article, filename);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                uploadFile.CopyTo(stream);
                stream.Close();
            }
            return RedirectToAction("InsuranceClaim");
        }

        [HttpGet]
        public IActionResult BuyAndExtendNewInsusance()
        {
            return View("Views/Admin/Insurance/GuidanceBuyAndExtendNewInsurance.cshtml");
        }

        [HttpPost]
        public IActionResult BuyAndExtendNewInsusancePost(IFormFile uploadFile)
        {
            if (uploadFile != null)
            {
                string contentType = uploadFile.ContentType;
                string filename = "Guidance for buying new health insurance and extending health insurance.pdf";
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string filePath = Path.Combine(article, filename);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                uploadFile.CopyTo(stream);
                stream.Close();
            }
            return RedirectToAction("BuyAndExtendNewInsusance");
        }
    }
}
