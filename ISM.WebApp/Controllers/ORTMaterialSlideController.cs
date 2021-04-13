using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using ISM.WebApp.Utils;
using Newtonsoft.Json;
using ISM.WebApp.Constant;
using Microsoft.AspNetCore.Http;

namespace ISM.WebApp.Controllers
{
    public class ORTMaterialSlideController : Controller
    {
        public OrientationDAO orientationDAO;
        public UserDAO userDAO;

        public ORTMaterialSlideController(OrientationDAO orientationDAO, UserDAO userDAO)
        {          
            this.orientationDAO = orientationDAO;
            this.userDAO = userDAO;
        }
        public IActionResult Index(int page = 1, string account = "", string fullname = "")
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            bool haveDegree = sessionUser.haveDegree;
            StudentIndexViewModel view = new StudentIndexViewModel();
            view.page = page;
            view.pageSize = pagingConst.PAGE_SIZE;
            view.totalPage = PagingUtils.calculateTotalPage(userDAO.getTotalDegreeStudent(isAdmin,haveDegree,account,fullname), view.pageSize);
            view.students = userDAO.getDegreeStudent(isAdmin, haveDegree, account, fullname, view.page, view.pageSize);
            view.fullname = fullname;
            view.account = account;
            return View("Views/Admin/Program/ORTMaterialSlide.cshtml", view);
        }

        public IActionResult Detail(int student_id=0, string program="", string content="", string material="", int page = 1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                ORTMaterialSlideDetailViewModel view = new ORTMaterialSlideDetailViewModel();
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.current_student = userDAO.getUserById(student_id);
                view.totalPage = PagingUtils.calculateTotalPage(orientationDAO.getTotalORTMaterialSlides(student_id, program, content, material), view.pageSize);
                view.materials = orientationDAO.GetORTMaterialSlides(student_id, view.page, view.pageSize, program, content, material);
                view.program = program;
                view.material = material;
                view.content = content;
                return View("Views/Admin/Program/ORTMaterialSlideDetail.cshtml", view);
            }
            else if (sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                ORTMaterialSlideDetailViewModel view = new ORTMaterialSlideDetailViewModel();
                view.page = page;
                view.pageSize = pagingConst.PAGE_SIZE;
                view.totalPage = PagingUtils.calculateTotalPage(orientationDAO.getTotalORTMaterialSlides(sessionUser.user_id, program, content, material), view.pageSize);
                view.materials = orientationDAO.GetORTMaterialSlides(sessionUser.user_id, view.page, view.pageSize, program, content, material);
                view.program = program;
                view.material = material;
                view.content = content;
                return View("Views/Degree/Program/ORTMaterialSlideDetail.cshtml", view);
            }
            return View();
        }

        public bool Create(int student_id, string program, string content, string material)
        {
            bool result = orientationDAO.CreateORTMaterialSlide(student_id, program, content, material);
            return result;
        }

        public bool Edit(int ort_material_slide_id, string program, string content, string material)
        {
            bool result = orientationDAO.EditORTMaterialSlide(ort_material_slide_id, program, content, material);
            return result;
        }
        public bool Delete(int ort_material_slide_id)
        {
            bool result = orientationDAO.DeleteORTMaterialSlide(ort_material_slide_id);
            return result;
        }

    }
}
