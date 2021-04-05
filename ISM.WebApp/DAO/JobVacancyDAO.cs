using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface JobVacancyDAO
    {
        List<JobVacancy> GetJobVacancies(int page, int pageSize, string job_name, string job_location, string employment_type, string content, DateTime? deadline);
        int GetTotalJobVacancies(string job_name, string job_location, string employment_type, string content, DateTime? deadline);
        bool CreateJobVacancy(string job_name, string job_location, string employment_type, string content, DateTime deadline);
        bool EditJobVacancy(int jobVacancy_id, string job_name, string job_location, string employment_type, string content, DateTime deadline);
        bool DeleteJobVacancy(int jobVacancy_id);
    }
}
