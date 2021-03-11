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

        public List<StudentGroup> GetStudentGroups(int page, int pageSize, int year, string program, string home_univercity, string campus, string coordinator)
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
                    where += " and upper(temp4.[program_name]) like upper('%' + @program + '%')";
                    com.Parameters.Add("@program", SqlDbType.NVarChar);
                    com.Parameters["@program"].Value = program;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(temp4.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(campus))
                {
                    where += " and upper(temp4.campus_name) like upper('%' + @campus + '%')";
                    com.Parameters.Add("@campus", SqlDbType.NVarChar);
                    com.Parameters["@campus"].Value = campus;
                }
                if (!string.IsNullOrEmpty(coordinator))
                {
                    where += " and upper(temp4.fullname) like upper('%' + @coordinator + '%')";
                    com.Parameters.Add("@coordinator", SqlDbType.NVarChar);
                    com.Parameters["@coordinator"].Value = coordinator;
                }
                if (year != 0)
                {
                    where += " and temp4.[year] = @year";
                    com.Parameters.Add("@year", SqlDbType.Int);
                    com.Parameters["@year"].Value = year;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select temp4.student_group_id,temp4.[program_name],temp4.[year],temp4.duration_start,temp4.duration_end," +
                      "temp4.home_univercity,temp4.note,temp4.campus_name,temp4.fullname,ROW_NUMBER() over (order by student_group_id asc) as " +
                      "rownumber from (select distinct temp3.student_group_id,temp3.[program_name],temp3.[year],temp3.duration_start,temp3.duration_end," +
                      "temp3.home_univercity, temp3.note, temp3.campus_name, temp3.fullname from(select temp1.student_group_id,temp1.[program_name]," +
                      "temp1.[year],temp1.duration_start,temp1.duration_end,temp1.home_univercity,temp1.note, temp1.campus_name," +
                      "substring((select '; ' + temp2.fullname as [text()] from (select a.student_group_id,b.[program_name],a.[year]," +
                      "a.duration_start,a.duration_end,a.home_univercity,a.note,d.fullname,e.campus_name from Student_Group a, Programs b, " +
                      "Coordinators c, Users d, Campus e where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and " +
                      "c.staff_id = d.[user_id] and a.campus_id = e.campus_id) as temp2 where temp1.student_group_id = temp2.student_group_id " +
                      "for xml path ('')),2,1000) as fullname from(select a.student_group_id, b.[program_name], a.[year], a.duration_start, " +
                      "a.duration_end, a.home_univercity, a.note, d.fullname, e.campus_name from Student_Group a, Programs b, Coordinators c, " +
                      "Users d, Campus e where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.staff_id = d.[user_id] " +
                      "and a.campus_id = e.campus_id) as temp1) as temp3) as temp4 where 1=1 " + where + ") as temp5 where temp5.rownumber >=@from and temp5.rownumber <=@to";
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
                        studentGroup.home_university = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
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

        public int getTotalStudentGroup(int year, string program, string home_univercity, string campus, string coordinator)
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
                    where += " and upper(temp5.[program_name]) like upper('%' + @program + '%')";
                    com.Parameters.Add("@program", SqlDbType.NVarChar);
                    com.Parameters["@program"].Value = program;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(temp5.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(campus))
                {
                    where += " and upper(temp5.campus_name) like upper('%' + @campus + '%')";
                    com.Parameters.Add("@campus", SqlDbType.NVarChar);
                    com.Parameters["@campus"].Value = campus;
                }
                if (!string.IsNullOrEmpty(coordinator))
                {
                    where += " and upper(temp5.fullname) like upper('%' + @coordinator + '%')";
                    com.Parameters.Add("@coordinator", SqlDbType.NVarChar);
                    com.Parameters["@coordinator"].Value = coordinator;
                }
                if (year != 0)
                {
                    where += " and temp5.[year] = @year";
                    com.Parameters.Add("@year", SqlDbType.Int);
                    com.Parameters["@year"].Value = year;
                }
                sql = "select count(*) from (select temp4.student_group_id,temp4.[program_name],temp4.[year]," +
                      "temp4.duration_start,temp4.duration_end,temp4.home_univercity,temp4.note,temp4.campus_name," +
                      "temp4.fullname from (select distinct temp3.student_group_id,temp3.[program_name],temp3.[year]," +
                      "temp3.duration_start,temp3.duration_end,temp3.home_univercity, temp3.note, temp3.campus_name, " +
                      "temp3.fullname from(select temp1.student_group_id,temp1.[program_name],temp1.[year],temp1.duration_start," +
                      "temp1.duration_end,temp1.home_univercity,temp1.note, temp1.campus_name,substring((select '; ' + temp2.fullname as [text()] " +
                      "from (select a.student_group_id,b.[program_name],a.[year],a.duration_start,a.duration_end,a.home_univercity,a.note,d.fullname," +
                      "e.campus_name from Student_Group a, Programs b, Coordinators c, Users d, Campus e where a.program_id = b.program_id " +
                      "and a.student_group_id = c.studentGroup_id and c.staff_id = d.[user_id] and a.campus_id = e.campus_id) as temp2 " +
                      "where temp1.student_group_id = temp2.student_group_id for xml path ('')),2,1000) as fullname " +
                      "from(select a.student_group_id, b.[program_name], a.[year], a.duration_start, a.duration_end, " +
                      "a.home_univercity, a.note, d.fullname, e.campus_name from Student_Group a, Programs b, Coordinators c, " +
                      "Users d, Campus e where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.staff_id = d.[user_id] " +
                      "and a.campus_id = e.campus_id) as temp1) as temp3) as temp4 ) as temp5 where 1=1" + where;
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
