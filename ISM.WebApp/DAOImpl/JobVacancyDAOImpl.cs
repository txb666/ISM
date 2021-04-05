using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAOImpl
{
    public class JobVacancyDAOImpl : JobVacancyDAO
    {
        public bool CreateJobVacancy(string job_name, string job_location, string employment_type, string content, DateTime deadline)
        {
            SqlConnection con = null;
            string sql = "INSERT INTO [dbo].[Job_Vacancies]([job_name],[job_location],[employment_type]," +
                         "[content],[deadline]) VALUES(@job_name,@job_location,@employment_type,@content,@deadline)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@job_name", SqlDbType.NVarChar);
                com.Parameters["@job_name"].Value = job_name;
                com.Parameters.Add("@job_location", SqlDbType.NVarChar);
                com.Parameters["@job_location"].Value = job_location;
                com.Parameters.Add("@employment_type", SqlDbType.NVarChar);
                com.Parameters["@employment_type"].Value = employment_type;
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.Parameters.Add("@deadline", SqlDbType.Date);
                com.Parameters["@deadline"].Value = deadline;
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public bool DeleteJobVacancy(int jobVacancy_id)
        {
            SqlConnection con = null;
            string sql = "delete from Job_Vacancies where jobVacancy_id = @jobVacancy_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@jobVacancy_id", SqlDbType.Int);
                com.Parameters["@jobVacancy_id"].Value = jobVacancy_id;
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public bool EditJobVacancy(int jobVacancy_id, string job_name, string job_location, string employment_type, string content, DateTime deadline)
        {
            SqlConnection con = null;
            string sql = "update Job_Vacancies set job_name = @job_name, [deadline] = @deadline, job_location = @job_location, " +
                         "employment_type = @employment_type, content = @content where jobVacancy_id = @jobVacancy_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@jobVacancy_id", SqlDbType.Int);
                com.Parameters["@jobVacancy_id"].Value = jobVacancy_id;
                com.Parameters.Add("@job_name", SqlDbType.NVarChar);
                com.Parameters["@job_name"].Value = job_name;
                com.Parameters.Add("@deadline", SqlDbType.Date);
                com.Parameters["@deadline"].Value = deadline;
                com.Parameters.Add("@job_location", SqlDbType.NVarChar);
                com.Parameters["@job_location"].Value = job_location;
                com.Parameters.Add("@employment_type", SqlDbType.NVarChar);
                com.Parameters["@employment_type"].Value = employment_type;
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public List<JobVacancy> GetJobVacancies(int page, int pageSize, string job_name, string job_location, string employment_type, string content, DateTime? deadline)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<JobVacancy> jobVacancies = new List<JobVacancy>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(job_name))
                {
                    where += " and upper(job_name) like upper('%' + @job_name + '%')";
                    com.Parameters.Add("@job_name", SqlDbType.NVarChar);
                    com.Parameters["@job_name"].Value = job_name;
                }
                if (!string.IsNullOrEmpty(job_location))
                {
                    where += " and upper(job_location) like upper('%' + @job_location + '%')";
                    com.Parameters.Add("@job_location", SqlDbType.NVarChar);
                    com.Parameters["@job_location"].Value = job_location;
                }
                if (!string.IsNullOrEmpty(employment_type))
                {
                    where += " and upper(employment_type) like upper('%' + @employment_type + '%')";
                    com.Parameters.Add("@employment_type", SqlDbType.NVarChar);
                    com.Parameters["@employment_type"].Value = employment_type;
                }
                if (!string.IsNullOrEmpty(content))
                {
                    where += " and upper(content) like upper('%' + @content + '%')";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                if (deadline != null)
                {
                    where += " and [deadline]=@deadline";
                    com.Parameters.Add("@deadline", SqlDbType.Date);
                    com.Parameters["@deadline"].Value = deadline;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY jobVacancy_id DESC) rownumber," +
                      "[jobVacancy_id],[job_name],[job_location],[employment_type],[content],[deadline] " +
                      "FROM [ISM].[dbo].[Job_Vacancies] where 1=1"+ where +") " +
                      "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    JobVacancy jobVacancy = new JobVacancy();
                    jobVacancy.jobVacancy_id = (int)reader.GetValue(reader.GetOrdinal("jobVacancy_id"));
                    jobVacancy.job_name = (string)reader.GetValue(reader.GetOrdinal("job_name"));
                    jobVacancy.job_location = (string)reader.GetValue(reader.GetOrdinal("job_location"));
                    jobVacancy.employment_type = (string)reader.GetValue(reader.GetOrdinal("employment_type"));
                    jobVacancy.content = (string)reader.GetValue(reader.GetOrdinal("content"));
                    jobVacancy.deadline = (DateTime)reader.GetValue(reader.GetOrdinal("deadline"));
                    jobVacancies.Add(jobVacancy);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return jobVacancies;
        }

        public int GetTotalJobVacancies(string job_name, string job_location, string employment_type, string content, DateTime? deadline)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int total = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(job_name))
                {
                    where += " and upper(job_name) like upper('%' + @job_name + '%')";
                    com.Parameters.Add("@job_name", SqlDbType.NVarChar);
                    com.Parameters["@job_name"].Value = job_name;
                }
                if (!string.IsNullOrEmpty(job_location))
                {
                    where += " and upper(job_location) like upper('%' + @job_location + '%')";
                    com.Parameters.Add("@job_location", SqlDbType.NVarChar);
                    com.Parameters["@job_location"].Value = job_location;
                }
                if (!string.IsNullOrEmpty(employment_type))
                {
                    where += " and upper(employment_type) like upper('%' + @employment_type + '%')";
                    com.Parameters.Add("@employment_type", SqlDbType.NVarChar);
                    com.Parameters["@employment_type"].Value = employment_type;
                }
                if (!string.IsNullOrEmpty(content))
                {
                    where += " and upper(content) like upper('%' + @content + '%')";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                if (deadline != null)
                {
                    where += " and [deadline]=@deadline";
                    com.Parameters.Add("@deadline", SqlDbType.Date);
                    com.Parameters["@deadline"].Value = deadline;
                }
                sql = "SELECT COUNT(*) FROM (SELECT [jobVacancy_id],[job_name],[job_location],[employment_type],[content]," +
                      "[deadline] FROM [ISM].[dbo].[Job_Vacancies] where 1=1"+ where +") as temp";
                com.CommandText = sql;
                total = Convert.ToInt32(com.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return total;
        }
    }
}
