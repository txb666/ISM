using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ISM.WebApp.Utils;
using System.Data;
using ISM.WebApp.Helper;
using ISM.WebApp.Constant;
using System.Text;

namespace ISM.WebApp.DAOImpl
{
    public class UserDAOImpl : UserDAO
    {
        public static string GetRandomPassword(int length)
        {
            string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }
            return sb.ToString();
        }
        public int createStaff(string fullname, string email, string account, DateTime? startDate, DateTime? endDate, bool status)
        {
            SqlConnection con = null;
            string sql = "insert into Users(role_id,fullname,email,account,[password],[start_date],end_date,[status],isFirstLoggedIn)"
                        + " select role_id,@fullname,@email,@account,@password,@start_date,@end_date,@status,1"
                        + " from Roles where role_name='Staff'";
            SqlCommand com = null;
            try
            {
                if (isStaffAlreadyExist(account, email)==true)
                {
                    return 0;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                com.Parameters["@fullname"].Value = fullname;
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
                com.Parameters.Add("@password", SqlDbType.NVarChar);
                com.Parameters["@password"].Value = GetRandomPassword(8);
                com.Parameters.Add("@start_date", SqlDbType.Date);
                com.Parameters["@start_date"].Value = startDate;
                if (startDate == null)
                {
                    com.Parameters["@start_date"].Value = DBNull.Value;
                }
                com.Parameters.Add("@end_date", SqlDbType.Date);
                com.Parameters["@end_date"].Value = endDate;
                if (endDate == null)
                {
                    com.Parameters["@end_date"].Value = DBNull.Value;
                }
                com.Parameters.Add("@status", SqlDbType.Bit);
                com.Parameters["@status"].Value = status;
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

        public bool createStudent(string degreeOrMobility, string fullname, string account, string email, int student_group_id, bool status)
        {
            SqlConnection con = null;
            string sql = " insert into Users(role_id,studentGroup_id,email,account,[password],[status],isFirstLoggedIn,fullname)"
                       + " values (@role_id, @student_group_id, @email, @account, @password, @status, @isFirstLoggedIn, @fullname)";
            SqlCommand com = null;
            try
            {
                if (isStaffAlreadyExist(account, email))
                {
                    return false;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@role_id", SqlDbType.Int);
                com.Parameters["@role_id"].Value = getRoleId(degreeOrMobility);
                com.Parameters.Add("@student_group_id", SqlDbType.Int);
                com.Parameters["@student_group_id"].Value = student_group_id;
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
                com.Parameters.Add("@password", SqlDbType.NVarChar);
                com.Parameters["@password"].Value = GetRandomPassword(8);
                com.Parameters.Add("@status", SqlDbType.Bit);
                com.Parameters["@status"].Value = status;
                com.Parameters.Add("@isFirstLoggedIn", SqlDbType.Bit);
                com.Parameters["@isFirstLoggedIn"].Value = true;
                com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                com.Parameters["@fullname"].Value = fullname;
                com.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public int getRoleId(string role_name)
        {
            SqlConnection con = null;
            string sql = "select role_id from Roles where role_name = @role_name";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@role_name", SqlDbType.NVarChar);
                com.Parameters["@role_name"].Value = role_name;
                return (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return 0;
        }

        public bool editStaff(int user_id, string fullname, string email, DateTime? startDate, DateTime? endDate, bool status, string originalEmail)
        {
            SqlConnection con = null;
            string sql = "update Users set fullname=@fullname, email=@email, [start_date]=@start_date, end_date=@end_date, [status]=@status"
                        + " where user_id=@user_id";
            SqlCommand com = null;
            try
            {         
                if(isEmailExist(email) && !email.Equals(originalEmail))
                {
                    return false;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                com.Parameters["@fullname"].Value = fullname;
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.Parameters.Add("@start_date", SqlDbType.Date);
                com.Parameters["@start_date"].Value = startDate;
                if (startDate == null)
                {
                    com.Parameters["@start_date"].Value = DBNull.Value;
                }
                com.Parameters.Add("@end_date", SqlDbType.Date);
                com.Parameters["@end_date"].Value = endDate;
                if (endDate == null)
                {
                    com.Parameters["@end_date"].Value = DBNull.Value;
                }
                com.Parameters.Add("@status", SqlDbType.Bit);
                com.Parameters["@status"].Value = status;
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

        public List<User> getAllStaff()
        {
            SqlConnection con = null;
            string sql = "select a.[user_id],a.account from Users a, Roles b where a.role_id=b.role_id and b.role_name='Staff'";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> staffs = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User staff = new User();
                    staff.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    staff.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    staffs.Add(staff);
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
            return staffs;
        }

        public List<User> GetStaff(int page, int pageSize, string fullname, string email, string account, bool? status, DateTime? startDate, DateTime? endDate)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> staffs = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    where += " and upper(email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = email;
                }
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }         
                if (status != null)
                {
                    where += " and status=@status";
                    com.Parameters.Add("@status", SqlDbType.Bit);
                    com.Parameters["@status"].Value = status;
                }
                if (startDate != null)
                {
                    where += " and start_date=@startDate";
                    com.Parameters.Add("@startDate", SqlDbType.Date);
                    com.Parameters["@startDate"].Value = startDate;
                }
                if (endDate != null)
                {
                    where += " and end_date=@endDate";
                    com.Parameters.Add("@endDate", SqlDbType.Date);
                    com.Parameters["@endDate"].Value = endDate;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select a.[user_id],a.account,a.email,a.fullname,a.gender,a.[start_date],a.end_date,a.[status],ROW_NUMBER() over (order by [user_id] asc) as rownumber from Users a,Roles b where a.role_id=b.role_id and b.role_name='Staff' " + where + ") as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User newStaff = new User();
                    newStaff.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        newStaff.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    newStaff.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    newStaff.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    newStaff.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    /*newStaff.startDate = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));*/
                    if (!reader.IsDBNull(reader.GetOrdinal("start_date")))
                    {
                        newStaff.startDate = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("end_date")))
                    {
                        newStaff.endDate = (DateTime)reader.GetValue(reader.GetOrdinal("end_date"));
                    }
                    staffs.Add(newStaff);
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
            return staffs;
        }

        public List<User> getStudent(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string fullname, string account, string email, string nationality, DateTime? dob, bool? gender, int? campus_id, int? program_id, string emergency_contact, string home_univercity, string accomodation, bool? status, DateTime? program_duration_start, DateTime? program_duration_end)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> students = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(a.account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(a.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    where += " and upper(a.email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = email;
                }
                if (!string.IsNullOrEmpty(nationality))
                {
                    where += " and upper(a.nationality) like upper('%' + @nationality + '%')";
                    com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                    com.Parameters["@nationality"].Value = nationality;
                }
                if (!string.IsNullOrEmpty(emergency_contact))
                {
                    where += " and upper(a.emergency_contact) like upper('%' + @emergency_contact + '%')";
                    com.Parameters.Add("@emergency_contact", SqlDbType.NVarChar);
                    com.Parameters["@emergency_contact"].Value = emergency_contact;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(b.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(accomodation))
                {
                    where += " and upper(c.[location]) like upper('%' + @location + '%')";
                    com.Parameters.Add("@location", SqlDbType.NVarChar);
                    com.Parameters["@location"].Value = accomodation;
                }
                if (dob != null)
                {
                    where += " and a.DOB=@dob";
                    com.Parameters.Add("@dob", SqlDbType.Date);
                    com.Parameters["@dob"].Value = dob;
                }
                if (program_duration_start != null)
                {
                    where += " and b.duration_start=@program_duration_start";
                    com.Parameters.Add("@program_duration_start", SqlDbType.Date);
                    com.Parameters["@program_duration_start"].Value = program_duration_start;
                }
                if (program_duration_end != null)
                {
                    where += " and b.duration_end=@program_duration_end";
                    com.Parameters.Add("@program_duration_end", SqlDbType.Date);
                    com.Parameters["@program_duration_end"].Value = program_duration_end;
                }
                if (status != null)
                {
                    where += " and a.[status]=@status";
                    com.Parameters.Add("@status", SqlDbType.Bit);
                    com.Parameters["@status"].Value = status;
                }
                if (gender != null)
                {
                    where += " and a.gender=@gender";
                    com.Parameters.Add("@gender", SqlDbType.Bit);
                    com.Parameters["@gender"].Value = gender;
                }
                if (campus_id != null)
                {
                    where += " and b.campus_id=@campus_id";
                    com.Parameters.Add("@campus_id", SqlDbType.Int);
                    com.Parameters["@campus_id"].Value = campus_id;
                }
                if (program_id != null)
                {
                    where += " and b.program_id=@program_id";
                    com.Parameters.Add("@program_id", SqlDbType.Int);
                    com.Parameters["@program_id"].Value = program_id;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over(order by a.[user_id] asc) rownumber, a.[user_id],a.fullname,a.email,a.account,a.nationality,a.DOB,a.gender,c.[location],d.[program_name],d.[type],"
                            + " e.campus_name,a.emergency_contact,b.duration_start,b.duration_end,b.home_univercity,a.[status],b.program_id,b.campus_id,b.student_group_id"
                            + " from Users a inner join Student_Group b on a.studentGroup_id=b.student_group_id left join Current_Accommodation c on a.studentGroup_id=c.studentGroup_id"
                            + " inner join Programs d on b.program_id=d.program_id inner join Campus e on b.campus_id=e.campus_id"
                            + " where d.[type]='Mobility'" + where + ")"
                            + " as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over(order by a.[user_id] asc) rownumber, a.[user_id],a.fullname,a.email,a.account,a.nationality,a.DOB,a.gender,c.[location],d.[program_name],d.[type],"
                            + " e.campus_name,a.emergency_contact,b.duration_start,b.duration_end,b.home_univercity,a.[status],b.program_id,b.campus_id,b.student_group_id"
                            + " from Users a inner join Student_Group b on a.studentGroup_id=b.student_group_id left join Current_Accommodation c on a.studentGroup_id=c.studentGroup_id"
                            + " inner join Programs d on b.program_id=d.program_id inner join Campus e on b.campus_id=e.campus_id inner join Coordinators f on b.student_group_id=f.studentGroup_id"
                            + " where f.staff_id=@current_staff_id and d.[type]='Mobility'" + where + ")"
                            + " as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over(order by a.[user_id] asc) rownumber, a.[user_id],a.fullname,a.email,a.account,a.nationality,a.DOB,a.gender,c.[location],d.[program_name],d.[type],"
                            + " e.campus_name,a.emergency_contact,b.duration_start,b.duration_end,b.home_univercity,a.[status],b.program_id,b.campus_id,b.student_group_id"
                            + " from Users a inner join Student_Group b on a.studentGroup_id=b.student_group_id left join Current_Accommodation c on a.[user_id]=c.student_id"
                            + " inner join Programs d on b.program_id=d.program_id inner join Campus e on b.campus_id=e.campus_id"
                            + " where d.[type]='Degree'" + where + ")"
                            + " as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return students;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User student = new User();
                    student.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    student.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        student.fullname= (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    student.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    if (!reader.IsDBNull(reader.GetOrdinal("nationality")))
                    {
                        student.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                    {
                        student.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        student.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("location")))
                    {
                        student.accomodation = (string)reader.GetValue(reader.GetOrdinal("location"));
                    }
                    student.program = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    student.campus = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    student.program_duration_start= (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    student.program_duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        student.home_univercity = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("emergency_contact")))
                    {
                        student.emergency_contact = (string)reader.GetValue(reader.GetOrdinal("emergency_contact"));
                    }
                    student.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    students.Add(student);
                }
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return students;
        }

        public int getTotalStaff(string fullname, string email, string account, bool? status, DateTime? startDate, DateTime? endDate)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalStaff = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    where += " and upper(email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = email;
                }
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (status != null)
                {
                    where += " and status=@status";
                    com.Parameters.Add("@status", SqlDbType.Bit);
                    com.Parameters["@status"].Value = status;
                }
                if (startDate != null)
                {
                    where += " and start_date=@startDate";
                    com.Parameters.Add("@startDate", SqlDbType.Date);
                    com.Parameters["@startDate"].Value = startDate;
                }
                if (endDate != null)
                {
                    where += " and end_date=@endDate";
                    com.Parameters.Add("@endDate", SqlDbType.Date);
                    com.Parameters["@endDate"].Value = endDate;
                }
                sql = "select count(*) from Users a,Roles b where a.role_id=b.role_id and b.role_name='Staff'"+where;
                com.CommandText = sql;
                totalStaff = (int)com.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalStaff;
        }

        public int getTotalStudent(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string fullname, string account, string email, string nationality, DateTime? dob, bool? gender, int? campus_id, int? program_id, string emergency_contact, string home_univercity, string accomodation, bool? status, DateTime? program_duration_start, DateTime? program_duration_end)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalStudent = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(a.account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(a.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    where += " and upper(a.email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = email;
                }
                if (!string.IsNullOrEmpty(nationality))
                {
                    where += " and upper(a.nationality) like upper('%' + @nationality + '%')";
                    com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                    com.Parameters["@nationality"].Value = nationality;
                }
                if (!string.IsNullOrEmpty(emergency_contact))
                {
                    where += " and upper(a.emergency_contact) like upper('%' + @emergency_contact + '%')";
                    com.Parameters.Add("@emergency_contact", SqlDbType.NVarChar);
                    com.Parameters["@emergency_contact"].Value = emergency_contact;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(b.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(accomodation))
                {
                    where += " and upper(c.[location]) like upper('%' + @location + '%')";
                    com.Parameters.Add("@location", SqlDbType.NVarChar);
                    com.Parameters["@location"].Value = accomodation;
                }
                if (dob != null)
                {
                    where += " and a.DOB=@dob";
                    com.Parameters.Add("@dob", SqlDbType.Date);
                    com.Parameters["@dob"].Value = dob;
                }
                if (program_duration_start != null)
                {
                    where += " and b.duration_start=@program_duration_start";
                    com.Parameters.Add("@program_duration_start", SqlDbType.Date);
                    com.Parameters["@program_duration_start"].Value = program_duration_start;
                }
                if (program_duration_end != null)
                {
                    where += " and b.duration_end=@program_duration_end";
                    com.Parameters.Add("@program_duration_end", SqlDbType.Date);
                    com.Parameters["@program_duration_end"].Value = program_duration_end;
                }
                if (status != null)
                {
                    where += " and a.[status]=@status";
                    com.Parameters.Add("@status", SqlDbType.Bit);
                    com.Parameters["@status"].Value = status;
                }
                if (gender != null)
                {
                    where += " and a.gender=@gender";
                    com.Parameters.Add("@gender", SqlDbType.Bit);
                    com.Parameters["@gender"].Value = gender;
                }
                if (campus_id != null)
                {
                    where += " and b.campus_id=@campus_id";
                    com.Parameters.Add("@campus_id", SqlDbType.Int);
                    com.Parameters["@campus_id"].Value = campus_id;
                }
                if (program_id != null)
                {
                    where += " and b.program_id=@program_id";
                    com.Parameters.Add("@program_id", SqlDbType.Int);
                    com.Parameters["@program_id"].Value = program_id;
                }             
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Users a inner join Student_Group b on a.studentGroup_id=b.student_group_id left join Current_Accommodation c on a.studentGroup_id=c.studentGroup_id"
                            + " inner join Programs d on b.program_id=d.program_id inner join Campus e on b.campus_id=e.campus_id"
                            + " where d.[type]='Mobility'" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Users a inner join Student_Group b on a.studentGroup_id=b.student_group_id left join Current_Accommodation c on a.studentGroup_id=c.studentGroup_id"
                            + " inner join Programs d on b.program_id=d.program_id inner join Campus e on b.campus_id=e.campus_id inner join Coordinators f on b.student_group_id=f.studentGroup_id"
                            + " where staff_id=@current_staff_id and d.[type]='Mobility'" + where;
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select count(*)"
                            + " from Users a inner join Student_Group b on a.studentGroup_id=b.student_group_id left join Current_Accommodation c on a.[user_id]=c.student_id"
                            + " inner join Programs d on b.program_id=d.program_id inner join Campus e on b.campus_id=e.campus_id"
                            + " where d.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalStudent;
                    }
                }
                com.CommandText = sql;
                totalStudent = (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalStudent;
        }

        public bool isEmailExist(string email)
        {
            SqlConnection con = null;
            string sql = "select count(*) from Users where email=@email";
            SqlDataReader reader = null;
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;               
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

        public bool isStaffAlreadyExist(string account, string email)
        {
            SqlConnection con = null;
            string sql = "select count(*) from Users where email=@email or account=@account";
            SqlDataReader reader = null;
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
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

        public bool editStudent(int id, string fullname, string email, bool status, string originalEmail)
        {
            SqlConnection con = null;
            string sql = "update Users set fullname=@fullname, email=@email, [status]=@status where [user_id]=@id";
            SqlCommand com = null;
            try
            {
                if (isEmailExist(email) && !email.Equals(originalEmail))
                {
                    return false;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                com.Parameters["@fullname"].Value = fullname;
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.Parameters.Add("@status", SqlDbType.Bit);
                com.Parameters["@status"].Value = status;
                com.Parameters.Add("@id", SqlDbType.Int);
                com.Parameters["@id"].Value = id;
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

        public List<User> getAllDegreeStudents()
        {
            SqlConnection con = null;
            string sql = " select a.[user_id],a.account,a.fullname,a.email"
                       + " from Users a, Roles b"
                       + " where a.role_id=b.role_id and b.role_name='Degree'";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> degreeStudents = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User student = new User();
                    student.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        student.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    student.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    degreeStudents.Add(student);
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
            return degreeStudents;
        }

        public List<User> getDegreeStudent(bool isAdmin, bool haveDegree, string account, string fullname, int page, int pageSize)
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
                if (haveDegree==true || isAdmin==true)
                {
                    sql = " select * from("
                        + " select ROW_NUMBER() over(order by a.[user_id] asc) rownumber, a.[user_id] , a.account, a.fullname, a.email, a.[status], a.isFirstLoggedIn, a.DOB, a.gender, a.emergency_contact, a.nationality"
                        + " from Users a, Roles b where a.role_id=b.role_id and b.role_name='Degree'" + where
                        + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                }
                else
                {
                    return studentList;
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

        public int getTotalDegreeStudent(bool isAdmin, bool haveDegree, string account, string fullname)
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
                if (haveDegree==true || isAdmin==true)
                {
                    sql = " select count(*)"
                        + " from Users a, Roles b where a.role_id=b.role_id and b.role_name='Degree'" + where;
                }
                else
                {
                    return total;
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

        public User getUserById(int id)
        {
            SqlConnection con = null;
            string sql = "select * from Users where [user_id]=@user_id";
            SqlDataReader reader = null;
            SqlCommand com = null;
            User user = new User();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = id;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    user.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    user.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    user.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader,null);
            }
            return user;
        }

        public List<User> GetVisaLettersAdminToExcel()
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> students = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select temp.[status],temp.role_name,temp.[user_id],temp.fullname,temp.account,temp.email,temp.nationality,temp.DOB,temp.gender,temp.[program_name]," +
                      "temp.campus_name,temp.home_univercity,temp.emergency_contact,f.[location] from (select a.[status],f.role_name,a.[user_id],a.fullname,a.account," +
                      "a.email,a.nationality,a.DOB,a.gender,d.[program_name],e.campus_name,a.emergency_contact,b.home_univercity from Users a, Student_Group b, " +
                      "Programs d, Campus e, Roles f where a.role_id = f.role_id and a.studentGroup_id = b.student_group_id and b.program_id = d.program_id and " +
                      "b.campus_id = e.campus_id) as temp FULL OUTER JOIN Current_Accommodation f on temp.[user_id] = f.student_id where temp.[user_id] != ''";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User student = new User();
                    student.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    student.role_name = (string)reader.GetValue(reader.GetOrdinal("role_name"));
                    student.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    student.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    student.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("nationality")))
                    {
                        student.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                    {
                        student.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        student.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("program_name")))
                    {
                        student.program = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("campus_name")))
                    {
                        student.campus = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        student.home_univercity = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("emergency_contact")))
                    {
                        student.emergency_contact = (string)reader.GetValue(reader.GetOrdinal("emergency_contact"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("location")))
                    {
                        student.accomodation = (string)reader.GetValue(reader.GetOrdinal("location"));
                    }
                    students.Add(student);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return students;
        }

        public List<User> GetVisaLettersStaffToExcel(int staff_id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<User> students = new List<User>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select temp.[status],temp.role_name,temp.[user_id],temp.fullname,temp.account,temp.email,temp.nationality,temp.DOB,temp.gender,temp.[program_name]," +
                      "temp.campus_name,temp.home_univercity,temp.emergency_contact,f.[location] from (select a.[status],f.role_name,a.[user_id],a.fullname,a.account," +
                      "a.email,a.nationality,a.DOB,a.gender,d.[program_name],e.campus_name,a.emergency_contact,c.staff_id,b.home_univercity " +
                      "from Users a, Student_Group b, Coordinators c, Programs d, Campus e, Roles f where a.role_id = f.role_id and a.studentGroup_id = b.student_group_id " +
                      "and b.student_group_id = c.studentGroup_id and b.program_id = d.program_id and b.campus_id = e.campus_id) " +
                      "as temp FULL OUTER JOIN Current_Accommodation f on temp.[user_id] = f.student_id where temp.staff_id = @staff_id";
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    User student = new User();
                    student.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    student.role_name = (string)reader.GetValue(reader.GetOrdinal("role_name"));
                    student.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    student.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    student.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("nationality")))
                    {
                        student.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                    {
                        student.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        student.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("program_name")))
                    {
                        student.program = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("campus_name")))
                    {
                        student.campus = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        student.home_univercity = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("emergency_contact")))
                    {
                        student.emergency_contact = (string)reader.GetValue(reader.GetOrdinal("emergency_contact"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("location")))
                    {
                        student.accomodation = (string)reader.GetValue(reader.GetOrdinal("location"));
                    }
                    students.Add(student);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return students;
        }

        public bool CreateAccountNotification(string account, string email)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            User student = new User();
            EmailHelper helper = new EmailHelper();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.account,a.[password],a.email from Users a where a.account = @account and a.email = @email";
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    student.password = (string)reader.GetValue(reader.GetOrdinal("password"));
                    student.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                }
                string body = "<!DOCTYPE html>" +
                              "<html>" +
                              "<body>" +
                              "<p>Hello</p>" +
                              "<p>The account has been created on FPT International Student Management Application with</p>" +
                              "<div>" +
                              "<div> - Username: <span style='font-weight: bold; '>"+student.account+"</span></div>" +
                              "<div> - Password: <span style='font-weight: bold; '>"+student.password+"</span></div>" +
                              "</div>" +
                              "<br/>" +
                              "Click <a href='"+notificationConst.URL+"' target='_blank'>here</a> to login using your new account." +
                              "</body>" +
                              "</html>";
                helper.SendMailHtml(student.email, "Account Notification", body);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return false;
        }
    }
}
