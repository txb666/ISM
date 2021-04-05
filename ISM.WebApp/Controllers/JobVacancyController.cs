using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.Utils;
using ISM.WebApp.DAO;

namespace ISM.WebApp.Controllers
{
    public class JobVacancyController : Controller
    {
        public JobVacancyDAO _jobVacancyDAO;
        public JobVacancyController(JobVacancyDAO jobVacancyDAO)
        {
            _jobVacancyDAO = jobVacancyDAO;
        }
        public IActionResult Index(int page = 1, string job_name = null, string job_location = null, string employment_type = null, string content = null, DateTime? deadline = null)
        {
            JobVacancyIndexViewModel viewModel = new JobVacancyIndexViewModel();
            viewModel.page = page;
            viewModel.pageSize = pagingConst.PAGE_SIZE;
            viewModel.totalPage = PagingUtils.calculateTotalPage(_jobVacancyDAO.GetTotalJobVacancies(job_name, job_location, employment_type, content, deadline), viewModel.pageSize);
            viewModel.jobVacancies = _jobVacancyDAO.GetJobVacancies(viewModel.page,viewModel.pageSize,job_name,job_location,employment_type,content,deadline);
            viewModel.job_name = job_name;
            viewModel.job_location = job_location;
            viewModel.employment_type = employment_type;
            viewModel.content = content;
            viewModel.deadline = deadline;
            return View("Views/Admin/Job/JobVacancies.cshtml", viewModel);
        }

        public bool CreateJV(string job_name, string job_location, string employment_type, string content, DateTime deadline)
        {
            bool result = _jobVacancyDAO.CreateJobVacancy(job_name, job_location, employment_type, content, deadline);
            return result;
        }

        public bool EditJV(int job_id, string job_name, string job_location, string employment_type, string content, DateTime deadline)
        {
            bool result = _jobVacancyDAO.EditJobVacancy(job_id, job_name, job_location, employment_type, content, deadline);
            return result;
        }

        public bool DeleteJV(int job_id)
        {
            bool result = _jobVacancyDAO.DeleteJobVacancy(job_id);
            return result;
        }
    }
}
