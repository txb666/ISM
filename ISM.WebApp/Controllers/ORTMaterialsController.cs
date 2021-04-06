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
    public class ORTMaterialsController : Controller
    {
        public OrientationDAO orientationDAO;
        public StudentGroupDAO studentGroupDAO;
        public ProgramDAO programDAO;
        public CampusDAO campusDAO;

        public  ORTMaterialsController(OrientationDAO o, StudentGroupDAO s, ProgramDAO p, CampusDAO c)
        {
            this.orientationDAO = o;
            this.studentGroupDAO = s;
            this.programDAO = p;
            this.campusDAO = c;
        }
        public IActionResult Index(int? year = null, string program = "", DateTime? duration_start = null, DateTime? duration_end = null, string home_univercity = "", string campus = "", string note = "", int page = 1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            int current_staff_id = sessionUser.user_id;
            StudentGroupIndexViewModel studentGroupViewModel = new StudentGroupIndexViewModel();
            studentGroupViewModel.page = page;
            studentGroupViewModel.pageSize = pagingConst.PAGE_SIZE;
            studentGroupViewModel.totalPage = PagingUtils.calculateTotalPage(studentGroupDAO.getTotalStudentGroupByStaff(current_staff_id, isAdmin, "Mobility", program, home_univercity, campus, year), studentGroupViewModel.pageSize);
            studentGroupViewModel.studentGroups = studentGroupDAO.GetStudentGroupByStaffWithPaging(current_staff_id, isAdmin, "Mobility", studentGroupViewModel.page, studentGroupViewModel.pageSize, program, home_univercity, campus, year);
            studentGroupViewModel.year = year;
            studentGroupViewModel.program = program;
            studentGroupViewModel.duration_start = duration_start;
            studentGroupViewModel.duration_end = duration_end;
            studentGroupViewModel.home_univercity = home_univercity;
            studentGroupViewModel.campus = campus;
            studentGroupViewModel.note = note;
            studentGroupViewModel.campusList = campusDAO.getAllCampus();
            studentGroupViewModel.programList = programDAO.getAllProgram();
            return View("Views/Admin/Program/ORTMaterial.cshtml", studentGroupViewModel);
        }

        public IActionResult Detail(int student_group_id=0, string content=null, string note=null, int page=1)
        {
            ORTMaterialsDetailViewModel view = new ORTMaterialsDetailViewModel();
            view.page = page;
            view.pageSize = pagingConst.PAGE_SIZE;
            view.current_student_group = studentGroupDAO.getStudentGroupById(student_group_id);
            view.totalPage = PagingUtils.calculateTotalPage(orientationDAO.getTotalORTMaterials(student_group_id,content,note), view.pageSize);
            view.materials = orientationDAO.GetORTMaterials(student_group_id, view.page, view.pageSize, content, note);
            view.content = content;
            view.note = note;
            return View("Views/Admin/Program/ORTMaterialDetail.cshtml", view);
        }

        public bool Create(int student_group_id, string content, string note)
        {
            bool result = orientationDAO.CreateORTMaterial(student_group_id, content, note);
            return result;
        }

        public bool Edit(int ort_materials_id, string content, string note)
        {
            bool result = orientationDAO.EditORTMaterial(ort_materials_id, content, note);
            return result;
        }

        public bool Delete(int ort_materials_id)
        {
            bool result = orientationDAO.DeleteORTMaterial(ort_materials_id);
            return result;
        }
    }
}
