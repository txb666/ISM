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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    public class CurrentAccomodationController : Controller
    {
        public StudentGroupDAO StudentGroupDAO;
        public AccomodationDAO accomodationDAO;
        public UserDAO userDAO;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CurrentAccomodationController(StudentGroupDAO studentGroupDAO, AccomodationDAO accomodationDAO, UserDAO userDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.StudentGroupDAO = studentGroupDAO;
            this.accomodationDAO = accomodationDAO;
            this.userDAO = userDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string degreeOrMobility="",string account="",string fullname="",int? student_id=null,int? student_group_id=null,string type="",string location="",string description="",string note="",int page=1)
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
            CurrentAccomodationIndexViewModel view = new CurrentAccomodationIndexViewModel();
            view.page = page;
            view.pageSize = pagingConst.PAGE_SIZE;
            view.totalPage = PagingUtils.calculateTotalPage(accomodationDAO.getTotalCurrentAccomodations(isAdmin,haveDegree,degreeOrMobility,current_staff_id,account,fullname,student_id,student_group_id,type,location,description,note),view.pageSize);
            view.currentAccomodations = accomodationDAO.GetCurrentAccomodations(isAdmin, haveDegree, degreeOrMobility, current_staff_id, view.page, view.pageSize, account, fullname, student_id, student_group_id, type, location, description, note);
            view.degreeStudents = userDAO.getAllDegreeStudents();
            view.manageGroup = StudentGroupDAO.GetStudentGroupByStaff(current_staff_id, isAdmin, "Mobility");
            view.account = account;
            view.fullname = fullname;
            view.type = type;
            view.location = location;
            view.description = description;
            view.note = note;
            view.student_id = student_id;
            view.student_group_id = student_group_id;
            view.degreeOrMobility = degreeOrMobility;
            return View("Views/Admin/CurrentAccomodation/Index.cshtml", view);
        }

        public IActionResult Create(int? student_id, int? student_group_id, string type, string location, string description, double? fee, string note, IFormFile picture)
        {
            string pictureName = "";
            if (picture != null)
            {
                string extension = Path.GetExtension(picture.FileName);
                if (student_id != null)
                {
                    pictureName = "student_" + student_id;
                    pictureName += extension;
                }
                else if(student_group_id != null)
                {
                    pictureName = "studentGroup_" + student_group_id;
                    pictureName += extension;
                }
                string image = Path.Combine(hostingEnvironment.WebRootPath, "image");
                string currentAccomodation = Path.Combine(image, "CurrentAccomodation");
                string filePath = Path.Combine(currentAccomodation, pictureName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture.CopyTo(stream);
                stream.Close();
            }
            bool result = accomodationDAO.createCurrentAccomodation(student_id, student_group_id, type, location, description, fee, pictureName, note);
            if (result == false)
            {
                return Json(new { status = "error", message = "Create Failed" });
            }
            return Json(new { status = "success", message = "Create successfully" });
        }

        public IActionResult Edit(int? student_id, int? student_group_id, int current_accomodation_id, string type, string location, string description, double? fee, string note, IFormFile picture, string pictureName)
        {
            if (picture != null)
            {
                string extension = Path.GetExtension(picture.FileName);
                if (student_id != null)
                {
                    pictureName = "student_" + student_id;
                    pictureName += extension;
                }
                else if (student_group_id != null)
                {
                    pictureName = "studentGroup_" + student_group_id;
                    pictureName += extension;
                }
                string image = Path.Combine(hostingEnvironment.WebRootPath, "image");
                string currentAccomodation = Path.Combine(image, "CurrentAccomodation");
                string filePath = Path.Combine(currentAccomodation, pictureName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture.CopyTo(stream);
                stream.Close();
            }
            bool result = accomodationDAO.editCurrentAccomodation(current_accomodation_id, type, location, description, fee, pictureName, note);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }

        public bool isCurrentAccomodationExist(int? student_id, int? student_group_id)
        {
            bool result=accomodationDAO.isCurrentAccomodationExist(student_id, student_group_id);
            return result;
        }
    }
}
