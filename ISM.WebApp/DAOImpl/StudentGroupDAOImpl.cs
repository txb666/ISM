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
        private class CoordinatorMapping
        {
            public int studentGroup_id { get; set; }
            public string account { get; set; }
            public int user_id { get; set; }
        }
        public int assignCoordinator(int staff_id, int studentGroup_id)
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
                return com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return 0;
        }

        public bool createStudentGroup(int program_id, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity, string note, List<int> coordinators)
        {
            SqlConnection con = null;
            string sql = "insert into Student_Group([year],program_id,campus_id,duration_start,duration_end,home_univercity,note)"
                        + " values(@year,@program_id,@campus_id,@duration_start,@duration_end,@home_univercity,@note);"
                        + " select SCOPE_IDENTITY();";
            SqlCommand com = null;
            try
            {
                if (isStudentGroupExist(program_id, campus_id, duration_start, duration_end, home_univercity))
                {
                    return false;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@year", SqlDbType.Int);
                com.Parameters["@year"].Value = duration_start.Year;
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
                if (home_univercity == null)
                {
                    com.Parameters["@home_univercity"].Value = DBNull.Value;
                }
                com.Parameters.Add("@note", SqlDbType.NVarChar);
                com.Parameters["@note"].Value = note;
                if (note == null)
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
                decimal inserted_idraw = (decimal)com.ExecuteScalar();
                int inserted_id = Convert.ToInt32(inserted_idraw);
                for (int i = 0; i < coordinators.Count; i++)
                {
                    assignCoordinator(coordinators[i], inserted_id);
                }
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
    

        /* public List<StudentGroup> GetStudentGroups(int page, int pageSize, int year, string program, string home_univercity, string campus, string coordinator)
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
         }*/

        public List<StudentGroup> GetStudentGroups(int page, int pageSize, int? year, string program, string home_univercity, string campus)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<StudentGroup> studentGroups = new List<StudentGroup>();
            List<CoordinatorMapping> mappings = new List<CoordinatorMapping>();
            try
            {
                mappings = GetCoordinatorMappings();
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(program))
                {
                    where += " and upper(b.[program_name]) like upper('%' + @program + '%')";
                    com.Parameters.Add("@program", SqlDbType.NVarChar);
                    com.Parameters["@program"].Value = program;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(a.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(campus))
                {
                    where += " and upper(c.campus_name) like upper('%' + @campus + '%')";
                    com.Parameters.Add("@campus", SqlDbType.NVarChar);
                    com.Parameters["@campus"].Value = campus;
                }              
                if (year != null)
                {
                    where += " and a.[year] = @year";
                    com.Parameters.Add("@year", SqlDbType.Int);
                    com.Parameters["@year"].Value = year;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select ROW_NUMBER() over(order by student_group_id asc) rownumber, a.student_group_id,b.[program_name],c.campus_name,a.[year],a.[duration_start],a.[duration_end],a.home_univercity,a.note"
                    + " from Student_Group a, Programs b, Campus c"
                    + " where a.program_id=b.program_id and a.campus_id=c.campus_id"+where+") temptable"
                    + " where rownumber>=@from and rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentGroup group = new StudentGroup();
                    group.studentGroup_id= (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    group.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    group.campus_name = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    group.year = (int)reader.GetValue(reader.GetOrdinal("year"));
                    group.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    group.duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        group.home_university = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("note")))
                    {
                        group.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                    }
                    group.coordinators = new List<User>();
                    for(int i = 0; i < mappings.Count; i++)
                    {
                        if (mappings[i].studentGroup_id == group.studentGroup_id)
                        {
                            User u = new User();
                            u.user_id = mappings[i].user_id;
                            u.account = mappings[i].account;
                            group.coordinators.Add(u);
                        }
                    }
                    studentGroups.Add(group);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return studentGroups;
        }

        private List<CoordinatorMapping> GetCoordinatorMappings()
        {
            SqlConnection con = null;
            string sql = "select a.studentGroup_id,b.account,b.[user_id] from Coordinators a, Users b where a.staff_id=b.[user_id]";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<CoordinatorMapping> mappings = new List<CoordinatorMapping>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CoordinatorMapping mapping = new CoordinatorMapping();
                    mapping.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                    mapping.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    mapping.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    mappings.Add(mapping);                   
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return mappings;
        }

            public int getTotalStudentGroup(int? year, string program, string home_univercity, string campus)
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
                if (year != null)
                {
                    where += " and [year] = @year";
                    com.Parameters.Add("@year", SqlDbType.Int);
                    com.Parameters["@year"].Value = year;
                }
                sql = "select count(*) from Student_Group where 1=1" + where;
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
      
        public bool isStudentGroupExist(int program_id, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity)
        {
            SqlConnection con = null;
            string sql = "select count(*) from Student_Group"
                        + " where program_id=@program_id and campus_id=@campus_id and duration_start=@duration_start and duration_end=@duration_end";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<StudentGroup> studentGroups = new List<StudentGroup>();
            bool isExist = true;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.Parameters.Add("@program_id", SqlDbType.Int);
                com.Parameters["@program_id"].Value = program_id;
                com.Parameters.Add("@campus_id", SqlDbType.Int);
                com.Parameters["@campus_id"].Value = campus_id;
                com.Parameters.Add("@duration_start", SqlDbType.Date);
                com.Parameters["@duration_start"].Value = duration_start;
                com.Parameters.Add("@duration_end", SqlDbType.Date);
                com.Parameters["@duration_end"].Value = duration_end;
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    sql += " and home_univercity=@home_univercity";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                com.CommandText = sql;
                int count = (int)com.ExecuteScalar();
                if (count == 0)
                {
                    isExist = false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return isExist;
        }

        public bool resetCoordinator(int studentGroup_id)
        {
            SqlConnection con = null;
            string sql = "delete from Coordinators where studentGroup_id=@studentGroup_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                com.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public StudentGroup getStudentGroupById(int studentGroup_id)
        {
            SqlConnection con = null;
            string sql = " select a.[year],a.student_group_id,a.program_id,b.program_name,a.campus_id,c.campus_name,a.duration_start,a.duration_end,a.home_univercity,a.note"
                       + " from Student_Group a, Programs b, Campus c where a.program_id=b.program_id and a.campus_id=c.campus_id and a.student_group_id=@studentGroup_id";
            SqlCommand com = null;
            SqlDataReader reader = null;
            StudentGroup group = null;
            List<CoordinatorMapping> mappings = new List<CoordinatorMapping>();
            try
            {
                mappings = GetCoordinatorMappings();
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                reader = com.ExecuteReader();
                group = new StudentGroup();
                while (reader.Read())
                {                   
                    group.studentGroup_id = studentGroup_id;
                    group.program_id = (int)reader.GetValue(reader.GetOrdinal("program_id"));
                    group.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    group.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    group.campus_name=(string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    group.year = (int)reader.GetValue(reader.GetOrdinal("year"));
                    group.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    group.duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity"))){
                        group.home_university = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("note")))
                    {
                        group.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                    }
                }
                group.coordinators = new List<User>();
                for(int i = 0; i < mappings.Count; i++)
                {
                    if (group.studentGroup_id == mappings[i].studentGroup_id)
                    {
                        User u = new User();
                        u.user_id = mappings[i].user_id;
                        u.account = mappings[i].account;
                        group.coordinators.Add(u);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return group;
        }

        public bool editStudentGroup(int studentGroup_id, string note, List<int> coordinators, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity, DateTime original_duration_start, DateTime original_duration_end, int original_campus_id, string original_home_univercity, int program_id)
        {
            SqlConnection con = null;
            string sql = " update Student_Group set [year]=@year, note=@note, duration_start=@duration_start, duration_end=@duration_end, home_univercity=@home_univercity, campus_id=@campus_id"
                       + " where student_group_id=@studentGroup_id";
            SqlCommand com = null;
            try
            {
                if(isStudentGroupExist(program_id,campus_id,duration_start,duration_end,home_univercity)==true && (duration_start!=original_duration_start || duration_end != original_duration_end || campus_id != original_campus_id || home_univercity != original_home_univercity))
                {
                    return false;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                com.Parameters.Add("@year", SqlDbType.Int);
                com.Parameters["@year"].Value = duration_start.Year;
                com.Parameters.Add("@campus_id", SqlDbType.Int);
                com.Parameters["@campus_id"].Value = campus_id;
                com.Parameters.Add("@duration_start", SqlDbType.Date);
                com.Parameters["@duration_start"].Value = duration_start;
                com.Parameters.Add("@duration_end", SqlDbType.Date);
                com.Parameters["@duration_end"].Value = duration_end;
                com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                com.Parameters["@home_univercity"].Value = home_univercity;
                if (home_univercity == null)
                {
                    com.Parameters["@home_univercity"].Value = DBNull.Value;
                }
                com.Parameters.Add("@note", SqlDbType.NVarChar);
                com.Parameters["@note"].Value = note;
                if (note == null)
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
                com.ExecuteNonQuery();
                resetCoordinator(studentGroup_id);
                for(int i = 0; i < coordinators.Count; i++)
                {
                    assignCoordinator(coordinators[i], studentGroup_id);
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public List<StudentGroup> GetStudentGroupByStaff(int staff_id, bool isAdmin, string degreeOrMobility)
        {
            SqlConnection con = null;
            string sql = "";
            if (isAdmin)
            {
                sql = " select a.student_group_id,b.campus_id,b.campus_name,c.program_id,c.[program_name],a.[year],a.duration_start,a.duration_end,a.home_univercity,a.note,c.[type] "
                   + " from Student_Group a, Campus b, Programs c"
                   + " where a.campus_id=b.campus_id and a.program_id=c.program_id and c.[type]=@degreeOrMobility";
            }
            else
            {
                sql = " select b.student_group_id,c.campus_id,c.campus_name,d.program_id,d.[program_name],b.[year],b.duration_start,b.duration_end,b.home_univercity,b.note,d.[type]"
                           + " from Coordinators a, Student_Group b, Campus c, Programs d"
                           + " where a.studentGroup_id=b.student_group_id and b.campus_id=c.campus_id and b.program_id=d.program_id and a.staff_id=@staff_id and d.[type]=@degreeOrMobility";
            }
            SqlCommand com = null;
            SqlDataReader reader = null;
            List<StudentGroup> groups = new List<StudentGroup>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.Parameters.Add("@degreeOrMobility", SqlDbType.NVarChar);
                com.Parameters["@degreeOrMobility"].Value = degreeOrMobility;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentGroup group = new StudentGroup();
                    group.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    group.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    group.campus_name = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    group.program_id = (int)reader.GetValue(reader.GetOrdinal("program_id"));
                    group.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    group.year = (int)reader.GetValue(reader.GetOrdinal("year"));
                    group.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    group.duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                    if (!reader.IsDBNull("home_univercity"))
                    {
                        group.home_university = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("note")))
                    {
                        group.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                    }
                    group.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    groups.Add(group);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return groups;
        }

        public List<StudentGroup> GetStudentGroupByStaffWithPaging(int staff_id, bool isAdmin, string degreeOrMobility, int page, int pageSize, string program, string home_univercity, string campus, int? year)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";       
            SqlCommand com = null;
            SqlDataReader reader = null;
            List<StudentGroup> groups = new List<StudentGroup>();
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
                if (year != null)
                {
                    where += " and [year] = @year";
                    com.Parameters.Add("@year", SqlDbType.Int);
                    com.Parameters["@year"].Value = year;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.Parameters.Add("@degreeOrMobility", SqlDbType.NVarChar);
                com.Parameters["@degreeOrMobility"].Value = degreeOrMobility;
                if (isAdmin)
                {
                    sql = " select * from("
                        + " select ROW_NUMBER() over (order by a.student_group_id asc) rownumber, a.student_group_id,b.campus_id,b.campus_name,c.program_id,c.[program_name],a.[year],a.duration_start,a.duration_end,a.home_univercity,a.note,c.[type]"
                        + " from Student_Group a, Campus b, Programs c"
                        + " where a.campus_id=b.campus_id and a.program_id=c.program_id and c.[type]=@degreeOrMobility" + where
                        + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                }
                else
                {
                    sql = " select * from("
                        + " select ROW_NUMBER() over (order by b.student_group_id asc) rownumber, b.student_group_id,c.campus_id,c.campus_name,d.program_id,d.[program_name],b.[year],b.duration_start,b.duration_end,b.home_univercity,b.note,d.[type]"
                        + " from Coordinators a, Student_Group b, Campus c, Programs d"
                        + " where a.studentGroup_id=b.student_group_id and b.campus_id=c.campus_id and b.program_id=d.program_id and a.staff_id=@staff_id and d.[type]=@degreeOrMobility" + where
                        + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentGroup group = new StudentGroup();
                    group.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    group.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    group.campus_name = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    group.program_id = (int)reader.GetValue(reader.GetOrdinal("program_id"));
                    group.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    group.year = (int)reader.GetValue(reader.GetOrdinal("year"));
                    group.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    group.duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                    if (!reader.IsDBNull("home_univercity"))
                    {
                        group.home_university = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("note")))
                    {
                        group.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                    }
                    group.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    groups.Add(group);
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
            return groups;
        }

        public int getTotalStudentGroupByStaff(int staff_id, bool isAdmin, string degreeOrMobility, string program, string home_univercity, string campus, int? year)
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
                if (year != null)
                {
                    where += " and [year] = @year";
                    com.Parameters.Add("@year", SqlDbType.Int);
                    com.Parameters["@year"].Value = year;
                }             
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.Parameters.Add("@degreeOrMobility", SqlDbType.NVarChar);
                com.Parameters["@degreeOrMobility"].Value = degreeOrMobility;
                if (isAdmin)
                {
                    sql = " select count(*)"
                        + " from Student_Group a, Campus b, Programs c"
                        + " where a.campus_id=b.campus_id and a.program_id=c.program_id and c.[type]=@degreeOrMobility" + where;
                }
                else
                {
                    sql = " select count(*)"
                        + " from Coordinators a, Student_Group b, Campus c, Programs d"
                        + " where a.studentGroup_id=b.student_group_id and b.campus_id=c.campus_id and b.program_id=d.program_id and a.staff_id=@staff_id and d.[type]=@degreeOrMobility" + where;
                }
                com.CommandText = sql;
                total = (int)com.ExecuteScalar();
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
