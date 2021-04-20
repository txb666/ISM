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
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
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
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
                bool haveDegree = isAdmin == true ? true : sessionUser.haveDegree;
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
                return View("Views/Admin/Insurance/StudentInsurance.cshtml", viewModel);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                InsuranceIndexViewModel view = new InsuranceIndexViewModel();
                view.studentInsurance = insuranceDAO.GetInsurance(sessionUser.user_id);
                return View("Views/Degree/Insurance/MyInsurance.cshtml", view);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool Edit(int id, DateTime startDate, DateTime expiryDate)
        {
            bool result = insuranceDAO.editInsurance(id, startDate, expiryDate);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool NotificationDegree(int days_before, DateTime deadline)
        {
            bool result = insuranceDAO.SetupNotificationDegree(days_before, deadline);
            return result;
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool NotificationMobility(int days_before)
        {
            bool result = insuranceDAO.SetupNotificationMobility(days_before);
            return result;
        }

        [Authorize(Roles = "Degree,Mobility")]
        public IActionResult CreateOrEdit(int student_id, int? insurance_id, DateTime start_date, DateTime expiry_date, IFormFile picture)
        {
            string pictureName = "";
            if (picture != null)
            {
                pictureName = "insurance_student_" + student_id + Path.GetExtension(picture.FileName);
                string imagePath = Path.Combine(hostingEnvironment.WebRootPath, "image");
                string insurancePath = Path.Combine(imagePath, "Insurance");
                string filePath = Path.Combine(insurancePath, pictureName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture.CopyTo(stream);
                stream.Close();
            }
            bool result = insuranceDAO.CreateOrEditInsurance(insurance_id, student_id, pictureName, start_date, expiry_date);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }
    }
}
