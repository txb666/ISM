using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace ISM.WebApp.Controllers
{
    [Authorize]
    public class VisaLetterController : Controller
    {
        public VisaLetterDAO VisaLetterDAO;
        public VisaLetterController(VisaLetterDAO visaLetterDAO)
        {
            this.VisaLetterDAO = visaLetterDAO;
        }
        public IActionResult Index(string degreeOrMobility="", string fullname = "", bool gender = true, string apply_receive = "", string visa_period = "", string type_visa = "", string nationality = "",string visa_type ="", string passport_number = "", string student_name = ""
           , DateTime? dob= null, DateTime? expired_date=null, int page=1)
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
            VisaLetterIndexViewModel visaLetterIndexView = new VisaLetterIndexViewModel();
            visaLetterIndexView.page = page;
            visaLetterIndexView.pageSize = pagingConst.PAGE_SIZE;
            visaLetterIndexView.degreeOrMobility = degreeOrMobility;
            visaLetterIndexView.totalPage = PagingUtils.calculateTotalPage(VisaLetterDAO.GetTotalVisaLetter(isAdmin,haveDegree,degreeOrMobility,current_staff_id,fullname,apply_receive,visa_period,visa_type,nationality,passport_number,dob,expired_date), visaLetterIndexView.pageSize);
            visaLetterIndexView.VisaLetters = VisaLetterDAO.GetVisaLetter(isAdmin,haveDegree,degreeOrMobility,current_staff_id,visaLetterIndexView.page,visaLetterIndexView.pageSize,fullname,apply_receive,visa_period,visa_type,nationality,passport_number,dob,expired_date);
            visaLetterIndexView.fullname = fullname;
            visaLetterIndexView.gender = gender;
            visaLetterIndexView.dob = dob;
            visaLetterIndexView.nationality = nationality;
            visaLetterIndexView.passport_number = passport_number;
            visaLetterIndexView.expired_date = expired_date;    
            visaLetterIndexView.visa_type = visa_type;
            visaLetterIndexView.visa_period = visa_period;
            visaLetterIndexView.apply_receive = apply_receive;
            return View("Views/Admin/Visa/PreApprovalVisa.cshtml", visaLetterIndexView);
        }
        
        public bool edit(int id, string visa_type,string visa_period,string apply_receive)
        {
            bool result = VisaLetterDAO.editVisaLetter(id, visa_type, visa_period, apply_receive);
            return result;
        }

        public IActionResult ExportToExcel(string fullname = "", bool gender = true, string apply_receive = "", string visa_period = "", string type_visa = "", string home_university = "", string nationality = "", string visa_type = "", string passport_number = "", string student_name = ""
           , DateTime? dob = null, DateTime? expired_dateFrom = null, DateTime? expired_dateTo = null)
        {

            /*VisaLetterIndexViewModel visaLetterIndexView = new VisaLetterIndexViewModel();
            dynamic json = Newtonsoft.Json.JsonConvert.SerializeObject(VisaLetterDAO.GetVisaLetter(visaLetterIndexView.page, visaLetterIndexView.pageSize, fullname, apply_receive, visa_period, type_visa, nationality, passport_number, dob, expired_dateFrom, expired_dateTo));
            DataTable dt = JsonConvert.DeserializeObject(json,typeof( DataTable));

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells.LoadFromDataTable(dt, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"VisaLetter-{DateTime.Now.ToString("yyyyMMdd")}.xlsx";
            return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",excelName);*/
            return View();
        }
    }
}
