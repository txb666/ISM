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
    [Authorize]
    public class StudentGroupController : Controller
    {
        public StudentGroupDAO studentGroupDAO;
        public StudentGroupController(StudentGroupDAO studentGroupDAO)
        {
            this.studentGroupDAO = studentGroupDAO;
        }
        public IActionResult Index(int year = 0, string program = "", DateTime? duration_start = null, DateTime? duration_end = null, string home_univercity = "", string campus = "", string coordinator = "", string note = "", int page = 1)
        {
            StudentGroupIndexViewModel studentGroupViewModel = new StudentGroupIndexViewModel();
            studentGroupViewModel.page = page;
            studentGroupViewModel.pageSize = 5;
            studentGroupViewModel.totalPage = PagingUtils.calculateTotalPage(studentGroupDAO.getTotalStudentGroup(year, program, home_univercity, campus, coordinator), studentGroupViewModel.pageSize);
            studentGroupViewModel.studentGroups = studentGroupDAO.GetStudentGroups(studentGroupViewModel.page, studentGroupViewModel.pageSize, year, program, home_univercity, campus, coordinator);
            studentGroupViewModel.year = year;
            studentGroupViewModel.program = program;
            studentGroupViewModel.duration_start = duration_start;
            studentGroupViewModel.duration_end = duration_end;
            studentGroupViewModel.home_univercity = home_univercity;
            studentGroupViewModel.campus = campus;
            studentGroupViewModel.coordinator = coordinator;
            studentGroupViewModel.note = note;
            return View("Views/Admin/StudentGroup/StudentGroup.cshtml", studentGroupViewModel);
        }
    }
}
