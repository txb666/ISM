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
    public class AccomodationDAOImpl : AccomodationDAO
    {
        public bool createCurrentAccomodation(int? student_id, int? student_group_id, string type, string location, string description, double? fee, string picture, string note)
        {
            SqlConnection con = null;
            string sql = " insert into Current_Accommodation(student_id,studentGroup_id,[type],[location],[description],fee,picture,note)"
                       + " values (@student_id,@studentGroup_id,@type,@location,@description,@fee,@picture,@note)";
            SqlCommand com = null;
            try
            {
                if (isCurrentAccomodationExist(student_id, student_group_id))
                {
                    return false;
                }
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                if (student_id == null)
                {
                    com.Parameters["@student_id"].Value = DBNull.Value;
                }
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = student_group_id;
                if (student_group_id == null)
                {
                    com.Parameters["@studentGroup_id"].Value = DBNull.Value;
                }
                com.Parameters.Add("@type", SqlDbType.NVarChar);
                com.Parameters["@type"].Value = type;
                com.Parameters.Add("@location", SqlDbType.NVarChar);
                com.Parameters["@location"].Value = location;
                com.Parameters.Add("@description", SqlDbType.NVarChar);
                com.Parameters["@description"].Value = description;
                if (string.IsNullOrEmpty(description))
                {
                    com.Parameters["@description"].Value = DBNull.Value;
                }              
                com.Parameters.Add("@fee", SqlDbType.Float);
                com.Parameters["@fee"].Value = fee;
                if (fee == null)
                {
                    com.Parameters["@fee"].Value = DBNull.Value;
                }
                com.Parameters.Add("@picture", SqlDbType.NVarChar);
                com.Parameters["@picture"].Value = picture;
                if (string.IsNullOrEmpty(picture))
                {
                    com.Parameters["@picture"].Value = DBNull.Value;
                }
                com.Parameters.Add("@note", SqlDbType.Text);
                com.Parameters["@note"].Value = note;
                if (string.IsNullOrEmpty(note))
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
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


        public bool editCurrentAccomodation(int current_accomodation_id, string type, string location, string description, double? fee, string picture, string note)
        {
            SqlConnection con = null;
            string sql = " update Current_Accommodation set [type]=@type,[location]=@location,[description]=@description,fee=@fee,note=@note,picture=@picture"
                       + " where current_accommodation_id=@current_accommodation_id";
            SqlCommand com = null;
            try
            {              
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);               
                com.Parameters.Add("@type", SqlDbType.NVarChar);
                com.Parameters["@type"].Value = type;
                com.Parameters.Add("@location", SqlDbType.NVarChar);
                com.Parameters["@location"].Value = location;
                com.Parameters.Add("@description", SqlDbType.NVarChar);
                com.Parameters["@description"].Value = description;
                if (string.IsNullOrEmpty(description))
                {
                    com.Parameters["@description"].Value = DBNull.Value;
                }
                com.Parameters.Add("@fee", SqlDbType.Float);
                com.Parameters["@fee"].Value = fee;
                if (fee == null)
                {
                    com.Parameters["@fee"].Value = DBNull.Value;
                }              
                com.Parameters.Add("@note", SqlDbType.Text);
                com.Parameters["@note"].Value = note;
                if (string.IsNullOrEmpty(note))
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
                com.Parameters.Add("@picture", SqlDbType.NVarChar);
                com.Parameters["@picture"].Value = picture;
                com.Parameters.Add("@current_accommodation_id", SqlDbType.Int);
                com.Parameters["@current_accommodation_id"].Value = current_accomodation_id;
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

        public List<CurrentAccomodation> GetCurrentAccomodations(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string account, string fullname, int? student_id, int? student_group_id, string type, string location, string description, string note)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<CurrentAccomodation> currentAccomodations = new List<CurrentAccomodation>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (degreeOrMobility.Equals("Degree"))
                {
                    if (!string.IsNullOrEmpty(account))
                    {
                        where += " and upper(b.account) like upper('%' + @account + '%')";
                        com.Parameters.Add("@account", SqlDbType.NVarChar);
                        com.Parameters["@account"].Value = account;
                    }
                    if (!string.IsNullOrEmpty(fullname))
                    {
                        where += " and upper(b.fullname) like upper('%' + @fullname + '%')";
                        com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                        com.Parameters["@fullname"].Value = fullname;
                    }
                    if (student_id != null)
                    {
                        where += " and a.student_id=@student_id";
                        com.Parameters.Add("@student_id", SqlDbType.Int);
                        com.Parameters["@student_id"].Value = student_id;
                    }
                }
                else if (degreeOrMobility.Equals("Mobility")){
                    if (student_group_id != null)
                    {
                        where += " and a.studentGroup_id=@studentGroup_id";
                        com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                        com.Parameters["@studentGroup_id"].Value = student_group_id;
                    }
                }
                if (!string.IsNullOrEmpty(type))
                {
                    where += " and upper(a.[type]) like upper('%' + @type + '%')";
                    com.Parameters.Add("@type", SqlDbType.NVarChar);
                    com.Parameters["@type"].Value = type;
                }
                if (!string.IsNullOrEmpty(location))
                {
                    where += " and upper(a.[location]) like upper('%' + @location + '%')";
                    com.Parameters.Add("@location", SqlDbType.NVarChar);
                    com.Parameters["@location"].Value = location;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    where += " and upper(a.[description]) like upper('%' + @description + '%')";
                    com.Parameters.Add("@description", SqlDbType.NVarChar);
                    com.Parameters["@description"].Value = description;
                }
                if (!string.IsNullOrEmpty(note))
                {
                    where += " and upper(a.note) like upper('%' + @note + '%')";
                    com.Parameters.Add("@note", SqlDbType.Text);
                    com.Parameters["@note"].Value = note;
                }            
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over(order by a.current_accommodation_id asc) rownumber, a.current_accommodation_id, a.studentGroup_id, c.program_id, c.[program_name], d.campus_id, d.campus_name, b.duration_start, b.duration_end, b.home_univercity, a.[type],a.[location],a.[description],a.fee,a.picture,a.note"
                            + " from Current_Accommodation a, Student_Group b, Programs c, Campus d"
                            + " where a.studentGroup_id=b.student_group_id and b.program_id=c.program_id and b.campus_id=d.campus_id" + where
                            + " ) as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over(order by a.current_accommodation_id asc) rownumber, a.current_accommodation_id, a.studentGroup_id, c.program_id, c.[program_name], d.campus_id, d.campus_name, b.duration_start, b.duration_end, b.home_univercity, a.[type],a.[location],a.[description],a.fee,a.picture,a.note"
                            + " from Current_Accommodation a, Student_Group b, Programs c, Campus d, Coordinators e"
                            + " where a.studentGroup_id=b.student_group_id and b.program_id=c.program_id and b.campus_id=d.campus_id and b.student_group_id=e.studentGroup_id and e.staff_id=@current_staff_id" + where
                            + " ) as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over(order by a.current_accommodation_id asc) rownumber, a.current_accommodation_id, a.student_id, b.account, b.fullname,a.[type],a.[location],a.[description],a.fee,a.picture,a.note"
                            + " from Current_Accommodation a, Users b"
                            + " where a.student_id=b.[user_id]" + where + ") as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return currentAccomodations;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    CurrentAccomodation ca = new CurrentAccomodation();
                    ca.current_accomodation_id = (int)reader.GetValue(reader.GetOrdinal("current_accommodation_id"));
                    ca.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    ca.location = (string)reader.GetValue(reader.GetOrdinal("location"));
                    if (!reader.IsDBNull(reader.GetOrdinal("description")))
                    {
                        ca.description = (string)reader.GetValue(reader.GetOrdinal("description"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("fee")))
                    {
                        ca.fee = (double)reader.GetValue(reader.GetOrdinal("fee"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        ca.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("note")))
                    {
                        ca.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                    }
                    if (degreeOrMobility.Equals("Degree"))
                    {
                        User student = new User();
                        student.user_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                        student.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                        student.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                        ca.student = student;
                    }
                    else if (degreeOrMobility.Equals("Mobility"))
                    {
                        StudentGroup studentGroup = new StudentGroup();
                        studentGroup.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                        studentGroup.program_id = (int)reader.GetValue(reader.GetOrdinal("program_id"));
                        studentGroup.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                        studentGroup.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                        studentGroup.campus_name = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                        studentGroup.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                        studentGroup.duration_end = (DateTime)reader.GetValue(reader.GetOrdinal("duration_end"));
                        if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                        {
                            studentGroup.home_university = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                        }
                        ca.student_group = studentGroup;
                    }
                    currentAccomodations.Add(ca);
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
            return currentAccomodations;
        }

        public List<RegisterAccomodation> getRegisterAccomodation(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string account, string fullname, string email, string home_univercity, string exchange_campus, string accomodation_option, string room_type, string other_request)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<RegisterAccomodation> registerAccomodations = new List<RegisterAccomodation>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(b.account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(b.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    where += " and upper(b.email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = email;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(c.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(exchange_campus))
                {
                    where += " and upper(a.exchange_campus) like upper('%' + @exchange_campus + '%')";
                    com.Parameters.Add("@exchange_campus", SqlDbType.NVarChar);
                    com.Parameters["@exchange_campus"].Value = exchange_campus;
                }
                if (!string.IsNullOrEmpty(accomodation_option))
                {
                    where += " and upper(a.accommodation_option) like upper('%' + @accommodation_option + '%')";
                    com.Parameters.Add("@accommodation_option", SqlDbType.NVarChar);
                    com.Parameters["@accommodation_option"].Value = accomodation_option;
                }
                if (!string.IsNullOrEmpty(room_type))
                {
                    where += " and upper(a.room_type) like upper('%' + @room_type + '%')";
                    com.Parameters.Add("@room_type", SqlDbType.NVarChar);
                    com.Parameters["@room_type"].Value = room_type;
                }
                if (!string.IsNullOrEmpty(other_request))
                {
                    where += " and upper(a.other_request) like upper('%' + @other_request + '%')";
                    com.Parameters.Add("@other_request", SqlDbType.NVarChar);
                    com.Parameters["@other_request"].Value = other_request;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over (order by a.register_accommodation_id asc) rownumber, a.register_accommodation_id,a.student_id,b.account,b.fullname,b.email,c.home_univercity,a.exchange_campus,a.accommodation_option,a.cost_per_month,a.room_size,a.room_type,a.distance,a.other_request,a.register_date"
                            + " from Register_Accommodation a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where
                            + " ) as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over (order by a.register_accommodation_id asc) rownumber, a.register_accommodation_id,a.student_id,b.account,b.fullname,b.email,c.home_univercity,a.exchange_campus,a.accommodation_option,a.cost_per_month,a.room_size,a.room_type,a.distance,a.other_request,a.register_date"
                            + " from Register_Accommodation a, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility' and c.student_group_id=e.studentGroup_id and e.staff_id=@current_staff_id" + where
                            + " ) as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over (order by a.register_accommodation_id asc) rownumber, a.register_accommodation_id,a.student_id,b.account,b.fullname,b.email,c.home_univercity,a.exchange_campus,a.accommodation_option,a.cost_per_month,a.room_size,a.room_type,a.distance,a.other_request,a.register_date"
                            + " from Register_Accommodation a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where
                            + " ) as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return registerAccomodations;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    RegisterAccomodation r = new RegisterAccomodation();
                    r.register_accomodation_id = (int)reader.GetValue(reader.GetOrdinal("register_accommodation_id"));
                    r.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    r.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    r.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    r.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        r.home_univercity = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    r.exchange_campus = (string)reader.GetValue(reader.GetOrdinal("exchange_campus"));
                    r.accomodation_option = (string)reader.GetValue(reader.GetOrdinal("accommodation_option"));
                    if (!reader.IsDBNull(reader.GetOrdinal("cost_per_month")))
                    {
                        r.cost_per_month = (double)reader.GetValue(reader.GetOrdinal("cost_per_month"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("room_size")))
                    {
                        r.room_size = (double)reader.GetValue(reader.GetOrdinal("room_size"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("room_type")))
                    {
                        r.room_type = (string)reader.GetValue(reader.GetOrdinal("room_type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("distance")))
                    {
                        r.distance = (double)reader.GetValue(reader.GetOrdinal("distance"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("other_request")))
                    {
                        r.other_request = (string)reader.GetValue(reader.GetOrdinal("other_request"));
                    }
                    r.register_date=(DateTime)reader.GetValue(reader.GetOrdinal("register_date"));
                    registerAccomodations.Add(r);
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
            return registerAccomodations;
        }

        public int getTotalCurrentAccomodations(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string account, string fullname, int? student_id, int? student_group_id, string type, string location, string description, string note)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "";
            int totalCurrentAccomodation = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (degreeOrMobility.Equals("Degree"))
                {
                    if (!string.IsNullOrEmpty(account))
                    {
                        where += " and upper(b.account) like upper('%' + @account + '%')";
                        com.Parameters.Add("@account", SqlDbType.NVarChar);
                        com.Parameters["@account"].Value = account;
                    }
                    if (!string.IsNullOrEmpty(fullname))
                    {
                        where += " and upper(b.fullname) like upper('%' + @fullname + '%')";
                        com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                        com.Parameters["@fullname"].Value = fullname;
                    }
                    if (student_id != null)
                    {
                        where += " and a.student_id=@student_id";
                        com.Parameters.Add("@student_id", SqlDbType.Int);
                        com.Parameters["@student_id"].Value = student_id;
                    }
                }
                else if (degreeOrMobility.Equals("Mobility")){
                    if (student_group_id != null)
                    {
                        where += " and a.studentGroup_id=@studentGroup_id";
                        com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                        com.Parameters["@studentGroup_id"].Value = student_group_id;
                    }
                }
                if (!string.IsNullOrEmpty(type))
                {
                    where += " and upper(a.[type]) like upper('%' + @type + '%')";
                    com.Parameters.Add("@type", SqlDbType.NVarChar);
                    com.Parameters["@type"].Value = type;
                }
                if (!string.IsNullOrEmpty(location))
                {
                    where += " and upper(a.[location]) like upper('%' + @location + '%')";
                    com.Parameters.Add("@location", SqlDbType.NVarChar);
                    com.Parameters["@location"].Value = location;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    where += " and upper(a.[description]) like upper('%' + @description + '%')";
                    com.Parameters.Add("@description", SqlDbType.NVarChar);
                    com.Parameters["@description"].Value = description;
                }
                if (!string.IsNullOrEmpty(note))
                {
                    where += " and upper(a.note) like upper('%' + @note + '%')";
                    com.Parameters.Add("@note", SqlDbType.Text);
                    com.Parameters["@note"].Value = note;
                }
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Current_Accommodation a, Student_Group b, Programs c, Campus d"
                            + " where a.studentGroup_id=b.student_group_id and b.program_id=c.program_id and b.campus_id=d.campus_id" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Current_Accommodation a, Student_Group b, Programs c, Campus d, Coordinators e"
                            + " where a.studentGroup_id=b.student_group_id and b.program_id=c.program_id and b.campus_id=d.campus_id and b.student_group_id=e.studentGroup_id and e.staff_id=@current_staff_id" + where;
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select count(*)"
                            + " from Current_Accommodation a, Users b"
                            + " where a.student_id=b.user_id" + where;
                    }
                    else
                    {
                        return totalCurrentAccomodation;
                    }
                }
                com.CommandText = sql;
                totalCurrentAccomodation = (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalCurrentAccomodation;
        }

        public int getTotalRegisterAccomodation(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string account, string fullname, string email, string home_univercity, string exchange_campus, string accomodation_option, string room_type, string other_request)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "";
            int totalRegisterAccomodations = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(b.account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(b.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    where += " and upper(b.email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = email;
                }
                if (!string.IsNullOrEmpty(home_univercity))
                {
                    where += " and upper(c.home_univercity) like upper('%' + @home_univercity + '%')";
                    com.Parameters.Add("@home_univercity", SqlDbType.NVarChar);
                    com.Parameters["@home_univercity"].Value = home_univercity;
                }
                if (!string.IsNullOrEmpty(exchange_campus))
                {
                    where += " and upper(a.exchange_campus) like upper('%' + @exchange_campus + '%')";
                    com.Parameters.Add("@exchange_campus", SqlDbType.NVarChar);
                    com.Parameters["@exchange_campus"].Value = exchange_campus;
                }
                if (!string.IsNullOrEmpty(accomodation_option))
                {
                    where += " and upper(a.accommodation_option) like upper('%' + @accommodation_option + '%')";
                    com.Parameters.Add("@accommodation_option", SqlDbType.NVarChar);
                    com.Parameters["@accommodation_option"].Value = accomodation_option;
                }
                if (!string.IsNullOrEmpty(room_type))
                {
                    where += " and upper(a.room_type) like upper('%' + @room_type + '%')";
                    com.Parameters.Add("@room_type", SqlDbType.NVarChar);
                    com.Parameters["@room_type"].Value = room_type;
                }
                if (!string.IsNullOrEmpty(other_request))
                {
                    where += " and upper(a.other_request) like upper('%' + @other_request + '%')";
                    com.Parameters.Add("@other_request", SqlDbType.NVarChar);
                    com.Parameters["@other_request"].Value = other_request;
                }      
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Register_Accommodation a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Register_Accommodation a, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility' and c.student_group_id=e.studentGroup_id and e.staff_id=@current_staff_id" + where;
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select count(*)"
                            + " from Register_Accommodation a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalRegisterAccomodations;
                    }
                }
                com.CommandText = sql;
                totalRegisterAccomodations = (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalRegisterAccomodations;
        }

        public bool isCurrentAccomodationExist(int? student_id, int? student_group_id)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "select count(*) from Current_Accommodation where ";
            bool isExist = true;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                if (student_id != null)
                {
                    sql += " student_id=@student_id";
                    com.Parameters.Add("@student_id", SqlDbType.Int);
                    com.Parameters["@student_id"].Value = student_id;
                }
                else if (student_group_id != null)
                {
                    sql += " studentGroup_id=@studentGroup_id";
                    com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                    com.Parameters["@studentGroup_id"].Value = student_group_id;
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
                DBUtils.closeAllResource(con, com, null, null);
            }
            return isExist;
        }
    }
}

