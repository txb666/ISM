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
    public class OrientationDAOImpl : OrientationDAO
    {
        public bool createORTSchedule(int student_id, string content, DateTime date, TimeSpan time, string location, string require_document)
        {
            SqlConnection con = null;
            string sql = "insert into ORT_Schedule(student_id,content,[date],[time],[location],requirement) " +
                         "values(@student_id, @content, @date, @time, @location, @requirement)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time", SqlDbType.Time);
                com.Parameters["@time"].Value = time;
                com.Parameters.Add("@location", SqlDbType.NVarChar);
                com.Parameters["@location"].Value = location;
                com.Parameters.Add("@requirement", SqlDbType.NVarChar);
                com.Parameters["@requirement"].Value = require_document;
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

        public bool deleteORTSchedule(int ort_schedule_id)
        {
            SqlConnection con = null;
            string sql = "delete from ORT_Schedule where ort_schedule_id = @ort_schedule_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@ort_schedule_id", SqlDbType.Int);
                com.Parameters["@ort_schedule_id"].Value = ort_schedule_id;
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

        public bool editORTSchedule(int ort_schedule_id, string content, DateTime date, TimeSpan time, string location, string require_document)
        {
            SqlConnection con = null;
            string sql = "update ORT_Schedule set content = @content, [date] = @date, [time] = @time, " +
                         "[location] = @location, requirement = @requirement where ort_schedule_id = @ort_schedule_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@ort_schedule_id", SqlDbType.Int);
                com.Parameters["@ort_schedule_id"].Value = ort_schedule_id;
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time", SqlDbType.Time);
                com.Parameters["@time"].Value = time;
                com.Parameters.Add("@location", SqlDbType.NVarChar);
                com.Parameters["@location"].Value = location;
                com.Parameters.Add("@requirement", SqlDbType.NVarChar);
                com.Parameters["@requirement"].Value = require_document;
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

        public List<User> GetDegreeStudent(bool isAdmin, int staff_id, string account, string fullname, int page, int pageSize)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> studentList = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                if (isAdmin)
                {
                    sql = "select * from " +
                          "(select ROW_NUMBER() over (order by a.[user_id] asc) rownumber, a.[user_id], a.account, a.fullname " +
                          "from Users a, Roles b where a.role_id = b.role_id and role_name = 'Degree'"+ where +") " +
                          "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                }
                else
                {
                    sql = "select * from " +
                          "(select ROW_NUMBER() over (order by a.[user_id] asc) rownumber,a.[user_id],a.fullname,a.account,d.staff_id " +
                          "from Users a, Roles b, Student_Group c, Coordinators d where a.role_id = b.role_id and b.role_name = 'Degree' " +
                          "and a.studentGroup_id = c.student_group_id and c.student_group_id = d.studentGroup_id and d.staff_id = @staff_id "+ where +") " +
                          "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    com.Parameters.Add("@staff_id", SqlDbType.Int);
                    com.Parameters["@staff_id"].Value = staff_id;
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User student = new User();
                    student.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        student.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    studentList.Add(student);
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
            return studentList;
        }

        public List<OrientationSchedule> GetSearchOrientationSchedules(int student_id, int page, int pageSize, string content, DateTime? date, TimeSpan? time, string location, string require_document)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<OrientationSchedule> orientationList = new List<OrientationSchedule>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(content))
                {
                    where += " and upper(content) like upper('%' + @content + '%')";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                if (!string.IsNullOrEmpty(location))
                {
                    where += " and upper(location) like upper('%' + @location + '%')";
                    com.Parameters.Add("@location", SqlDbType.NVarChar);
                    com.Parameters["@location"].Value = location;
                }
                if (!string.IsNullOrEmpty(require_document))
                {
                    where += " and upper(requirement) like upper('%' + @require_document + '%')";
                    com.Parameters.Add("@require_document", SqlDbType.NVarChar);
                    com.Parameters["@require_document"].Value = location;
                }
                if (date != null)
                {
                    where += " and [date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (time != null)
                {
                    where += " and [time]=@time";
                    com.Parameters.Add("@time", SqlDbType.Time);
                    com.Parameters["@time"].Value = time;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select ROW_NUMBER() over (order by a.[date] desc) rownumber,b.fullname,b.account," +
                      "a.ort_schedule_id,a.student_id,a.content,a.[date],a.[time],a.[location],a.requirement from ORT_Schedule a, " +
                      "Users b where a.student_id = @student_id and a.student_id = b.[user_id] " + where + ") " +
                      "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    OrientationSchedule ortSchedule = new OrientationSchedule();
                    ortSchedule.ortSchedule_id = (int)reader.GetValue(reader.GetOrdinal("ort_schedule_id"));
                    ortSchedule.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    ortSchedule.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    ortSchedule.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    ortSchedule.content = (string)reader.GetValue(reader.GetOrdinal("content"));
                    ortSchedule.date = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    ortSchedule.time = (TimeSpan)reader.GetValue(reader.GetOrdinal("time"));
                    ortSchedule.location = (string)reader.GetValue(reader.GetOrdinal("location"));
                    ortSchedule.require_document = (string)reader.GetValue(reader.GetOrdinal("requirement"));
                    orientationList.Add(ortSchedule);
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
            return orientationList;
        }

        public int GetSearchTotalORT(int student_id, string content, DateTime? date, TimeSpan? time, string location, string require_document)
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
                if (!string.IsNullOrEmpty(content))
                {
                    where += " and upper(content) like upper('%' + @content + '%')";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                if (!string.IsNullOrEmpty(location))
                {
                    where += " and upper([location]) like upper('%' + @location + '%')";
                    com.Parameters.Add("@location", SqlDbType.NVarChar);
                    com.Parameters["@location"].Value = location;
                }
                if (!string.IsNullOrEmpty(require_document))
                {
                    where += " and upper(requirement) like upper('%' + @require_document + '%')";
                    com.Parameters.Add("@require_document", SqlDbType.NVarChar);
                    com.Parameters["@require_document"].Value = location;
                }
                if (date != null)
                {
                    where += " and [date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (time != null)
                {
                    where += " and [time]=@time";
                    com.Parameters.Add("@time", SqlDbType.Time);
                    com.Parameters["@time"].Value = time;
                }
                sql = "select count(*) from ORT_Schedule a where a.student_id = @student_id"+ where +"";
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
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

        public int GetTotalDegreeStudent(bool isAdmin, int staff_id, string account, string fullname)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalDegreeStudent = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (isAdmin)
                {
                    sql = "select count(*) from (select a.[user_id], a.account, a.fullname " +
                          "from Users a, Roles b where a.role_id = b.role_id and role_name = 'Degree'" + where + ") as temp";
                }
                else
                {
                    sql = "select count(*) from (select a.[user_id],a.fullname,a.account,d.staff_id from Users a, Roles b, " +
                          "Student_Group c, Coordinators d where a.role_id = b.role_id and b.role_name = 'Degree' " +
                          "and a.studentGroup_id = c.student_group_id and c.student_group_id = d.studentGroup_id " +
                          "and d.staff_id = @staff_id " + where + ") as temp";
                    com.Parameters.Add("@staff_id", SqlDbType.Int);
                    com.Parameters["@staff_id"].Value = staff_id;
                }
                com.CommandText = sql;
                totalDegreeStudent = (int)com.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalDegreeStudent;
        }

        public bool isORTAlreadyExist(int student_id, string content, DateTime date, TimeSpan time, string location)
        {
            SqlConnection con = null;
            string sql = "select count(*) from ORT_Schedule a where a.student_id = @student_id and upper(a.content) like upper('%' + @content + '%') and a.[date] = @date and a.[time] = @time and upper(a.[location]) like upper('%' + @location + '%')";
            SqlDataReader reader = null;
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time", SqlDbType.Time);
                com.Parameters["@time"].Value = time;
                com.Parameters.Add("@location", SqlDbType.NVarChar);
                com.Parameters["@location"].Value = location;
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                count = (int)com.ExecuteScalar();
                if (count == 0)
                {
                    isExist = false;
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
            return isExist;
        }

        public bool isSameTime(int student_id, DateTime date, TimeSpan time)
        {
            SqlConnection con = null;
            string sql = "select count(*) from ORT_Schedule a where a.[date] = @date and a.[time] = @time and a.student_id = @student_id";
            SqlDataReader reader = null;
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time", SqlDbType.Time);
                com.Parameters["@time"].Value = time;
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                count = (int)com.ExecuteScalar();
                if (count == 0)
                {
                    isExist = false;
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
            return isExist;
        }
    }
}
