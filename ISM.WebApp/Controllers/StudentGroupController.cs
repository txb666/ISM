using ISM.WebApp.DAO;
using ISM.WebApp.Models;
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
    [Authorize(Roles = "Admin")]
    public class StudentGroupController : Controller
    {
        public StudentGroupDAO studentGroupDAO;
        public ProgramDAO programDAO;
        public CampusDAO campusDAO;
        public UserDAO userDAO;
        public StudentGroupController(StudentGroupDAO studentGroupDAO, ProgramDAO programDAO, CampusDAO campusDAO, UserDAO userDAO)
        {
            this.studentGroupDAO = studentGroupDAO;
            this.programDAO = programDAO;
            this.campusDAO = campusDAO;
            this.userDAO = userDAO;
        }
        public IActionResult Index(int? year = null, string program = "", DateTime? duration_start = null, DateTime? duration_end = null, string home_univercity = "", string campus = "", string coordinator = "", string note = "", int page = 1)
        {
            StudentGroupIndexViewModel studentGroupViewModel = new StudentGroupIndexViewModel();
            studentGroupViewModel.page = page;
            studentGroupViewModel.pageSize = 5;
            studentGroupViewModel.totalPage = PagingUtils.calculateTotalPage(studentGroupDAO.getTotalStudentGroup(year, program, home_univercity, campus), studentGroupViewModel.pageSize);
            studentGroupViewModel.studentGroups = studentGroupDAO.GetStudentGroups(studentGroupViewModel.page, studentGroupViewModel.pageSize, year, program, home_univercity, campus);
            studentGroupViewModel.year = year;
            studentGroupViewModel.program = program;
            studentGroupViewModel.duration_start = duration_start;
            studentGroupViewModel.duration_end = duration_end;
            studentGroupViewModel.home_univercity = home_univercity;
            studentGroupViewModel.campus = campus;
            studentGroupViewModel.coordinator = coordinator;
            studentGroupViewModel.note = note;
            studentGroupViewModel.campusList = campusDAO.getAllCampus();
            studentGroupViewModel.programList = programDAO.getAllProgram();
            return View("Views/Admin/StudentGroup/StudentGroup.cshtml", studentGroupViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            StudentGroupCreateViewModel viewmodel = new StudentGroupCreateViewModel();
            viewmodel.campusList = campusDAO.getAllCampus();
            viewmodel.programList = programDAO.getAllProgram();
            viewmodel.coordinatorList = userDAO.getAllStaff();
            return View("Views/Admin/StudentGroup/Create.cshtml",viewmodel);
        }

        [HttpPost]
        public bool Create(int program_id, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity, string note, string coordinators)
        {
            List<int> coordinatorList = FormatUtil.JsonStringToIntegerList(coordinators);
            bool result = studentGroupDAO.createStudentGroup(program_id, campus_id, duration_start, duration_end, home_univercity, note, coordinatorList);
            return result;
        }

        public bool isStudentGroupExist(int program_id, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity)
        {
            bool result = studentGroupDAO.isStudentGroupExist(program_id, campus_id, duration_start, duration_end, home_univercity);
            return result;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            StudentGroupEditViewModel viewModel = new StudentGroupEditViewModel();
            viewModel.group = studentGroupDAO.getStudentGroupById(id);
            viewModel.campusList = campusDAO.getAllCampus();
            viewModel.programList = programDAO.getAllProgram();
            List<User> allCoordinator = userDAO.getAllStaff();
            viewModel.coordinatorList = new List<User>();
            for(int i = 0; i < allCoordinator.Count; i++)
            {
                bool insert = true;
                for(int j = 0; j < viewModel.group.coordinators.Count; j++)
                {
                    if (allCoordinator[i].user_id == viewModel.group.coordinators[j].user_id)
                    {
                        insert = false;
                    }
                }
                if (insert == true)
                {
                    viewModel.coordinatorList.Add(allCoordinator[i]);
                }
            }
            return View("Views/Admin/StudentGroup/Edit.cshtml", viewModel);
        }

        [HttpPost]
        public bool Edit(int id, string note, string coordinators)
        {
            List<int> coordinatorList = FormatUtil.JsonStringToIntegerList(coordinators);
            bool result = studentGroupDAO.editStudentGroup(id, note, coordinatorList);
            return result;
        }
    }
}
