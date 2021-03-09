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
    public class StudentGroupDAOImpl : StudentGroupDAO
    {
        public void assignCoordinator(int staff_id, int studentGroup_id)
        {
            SqlConnection con = null;
            string sql = "insert into Coordinators(staff_id,studentGroup_id) values(@staff_id,@studentGroup_id)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;

                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
        }

        public void createStudentGroup(int year, int program_id, int campus_id, DateTime? duration_start, DateTime? duration_end, string home_univercity, string note)
        {
            SqlConnection con = null;
            string sql = "insert into Student_Group([year],program_id,campus_id,duration_start,duration_end,home_univercity,note)" +
                        "values(@year,@program_id,@campus_id,@duration_start,@duration_end,@home_univercity,@note)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@year", SqlDbType.Int);
                com.Parameters["@year"].Value = year;
                com.Parameters.Add("@program_id", SqlDbType.Int);
                com.Parameters["@program_id"].Value = program_id;
                com.Parameters.Add("@campus_id", SqlDbType.Int);
                com.Parameters["@campus_id"].Value = campus_id;
                com.Parameters.Add("@duration_start", SqlDbType.Date);
                com.Parameters["@duration_start"].Value = duration_start;
                com.Parameters.Add("@duration_end", SqlDbType.Date);
                com.Parameters["@duration_end"].Value = duration_end;
                com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                com.Parameters["@home_univercity"].Value = home_univercity;
                com.Parameters.Add("@note", SqlDbType.Text);
                com.Parameters["@note"].Value = note;

                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
        }

        public void editStudentGroup()
        {
            
        }

        public List<StudentGroup> GetStudentGroups(int page, int pageSize, string program, string home_univercity, string campus, string coordinator)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<StudentGroup> studentGroups = new List<StudentGroup>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(program))
                {
                    where += " and upper([program_name]) like upper('%' + @program + '%')";
                    com.Parameters.Add("@program", SqlDbType.NVarChar);
                    com.Parameters["@program"].Value = program;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(campus))
                {
                    where += " and upper(campus_name) like upper('%' + @campus + '%')";
                    com.Parameters.Add("@campus", SqlDbType.NVarChar);
                    com.Parameters["@campus"].Value = campus;
                }
                if (!string.IsNullOrEmpty(coordinator))
                {
                    where += " and upper(fullname) like upper('%' + @coordinator + '%')";
                    com.Parameters.Add("@coordinator", SqlDbType.NVarChar);
                    com.Parameters["@coordinator"].Value = coordinator;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select a.student_group_id,b.[program_name],a.[year]," +
                    "a.duration_start,a.duration_end,a.home_univercity,a.note," +
                    "d.fullname,e.campus_name,ROW_NUMBER() over (order by student_group_id asc) as " +
                    "rownumber from Student_Group a,Programs b,Coordinators c,Users d, Campus e " +
                    "where a.program_id=b.program_id and a.student_group_id=c.studentGroup_id and " +
                    "c.staff_id=d.[user_id] and a.campus_id=e.campus_id" + where + ") " +
                    "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentGroup studentGroup = new StudentGroup();
                    studentGroup.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    studentGroup.year = (int)reader.GetValue(reader.GetOrdinal("year"));
                    studentGroup.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    studentGroup.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    studentGroup.duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        studentGroup.home_university = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    studentGroup.campus_name = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        studentGroup.coordinator = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    studentGroups.Add(studentGroup);
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
            return studentGroups;
        }

        public int getTotalStudentGroup(string program, string home_univercity, string campus, string coordinator)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalStudentGroup = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(program))
                {
                    where += " and upper([program_name]) like upper('%' + @program + '%')";
                    com.Parameters.Add("@program", SqlDbType.NVarChar);
                    com.Parameters["@program"].Value = program;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(campus))
                {
                    where += " and upper(campus_name) like upper('%' + @campus + '%')";
                    com.Parameters.Add("@campus", SqlDbType.NVarChar);
                    com.Parameters["@campus"].Value = campus;
                }
                if (!string.IsNullOrEmpty(coordinator))
                {
                    where += " and upper(fullname) like upper('%' + @coordinator + '%')";
                    com.Parameters.Add("@coordinator", SqlDbType.NVarChar);
                    com.Parameters["@coordinator"].Value = coordinator;
                }
                sql = "select count(*) from Student_Group a, Programs b, Coordinators c,Users d, Campus e " +
                    "where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id " +
                    "and c.staff_id = d.[user_id] and e.campus_id = a.campus_id" + where;
                com.CommandText = sql;
                totalStudentGroup = (int)com.ExecuteScalar();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalStudentGroup;
        }
    }
}
