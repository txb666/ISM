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
    public class VisaDAOImpl : VisaDAO
    {
        public bool editVisa(int visa_id, DateTime start_date, DateTime expired_date, DateTime entry_date, string entry_port)
        {
            SqlConnection con = null;
            string sql = "update Visa set [start_date]=@start_date,expired_date=@expired_date,date_entry=@entry_date,entry_port=@entry_port "
                        + "where visa_id=@visa_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@visa_id", SqlDbType.Int);
                com.Parameters["@visa_id"].Value = visa_id;
                com.Parameters.Add("@start_date", SqlDbType.Date);
                com.Parameters["@start_date"].Value = start_date;
                com.Parameters.Add("@expired_date", SqlDbType.Date);
                com.Parameters["@expired_date"].Value = expired_date;
                com.Parameters.Add("@entry_date", SqlDbType.Date);
                com.Parameters["@entry_date"].Value = entry_date;
                com.Parameters.Add("@entry_port", SqlDbType.NVarChar);
                com.Parameters["@entry_port"].Value = entry_port;
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

        public int GetTotalVisa(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string account, string fullname, DateTime? start_date, DateTime? expired_date, DateTime? date_entry, string entry_port)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "";
            int totalVisa = 0;
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
                if (!string.IsNullOrEmpty(entry_port))
                {
                    where += " and upper(a.entry_port) like upper('%' + @entry_port + '%')";
                    com.Parameters.Add("@entry_port", SqlDbType.NVarChar);
                    com.Parameters["@entry_port"].Value = entry_port;
                }
                if (start_date != null)
                {
                    where += " and a.[start_date]=@start_date";
                    com.Parameters.Add("@start_date", SqlDbType.Date);
                    com.Parameters["@start_date"].Value = start_date;
                }
                if (expired_date != null)
                {
                    where += " and a.expired_date=@expired_date";
                    com.Parameters.Add("@expired_date", SqlDbType.Date);
                    com.Parameters["@expired_date"].Value = expired_date;
                }
                if (date_entry != null)
                {
                    where += " and a.date_entry=@date_entry";
                    com.Parameters.Add("@date_entry", SqlDbType.Date);
                    com.Parameters["@date_entry"].Value = date_entry;
                }
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Visa a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Visa a, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and c.student_group_id=e.studentGroup_id and d.[type]='Mobility' and e.staff_id=@current_staff_id" + where;
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select count(*)"
                            + " from Visa a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalVisa;
                    }
                }
                com.CommandText = sql;
                totalVisa = (int)com.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalVisa;
        }

        public List<Visa> GetVisa(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string account, string fullname, DateTime? start_date, DateTime? expired_date, DateTime? date_entry, string entry_port)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Visa> visalist = new List<Visa>();
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
                if (!string.IsNullOrEmpty(entry_port))
                {
                    where += " and upper(a.entry_port) like upper('%' + @entry_port + '%')";
                    com.Parameters.Add("@entry_port", SqlDbType.NVarChar);
                    com.Parameters["@entry_port"].Value = entry_port;
                }
                if (start_date != null)
                {
                    where += " and a.[start_date]=@start_date";
                    com.Parameters.Add("@start_date", SqlDbType.Date);
                    com.Parameters["@start_date"].Value = start_date;
                }               
                if (expired_date != null)
                {
                    where += " and a.expired_date=@expired_date";
                    com.Parameters.Add("@expired_date", SqlDbType.Date);
                    com.Parameters["@expired_date"].Value = expired_date;
                }
                if (date_entry != null)
                {
                    where += " and a.date_entry=@date_entry";
                    com.Parameters.Add("@date_entry", SqlDbType.Date);
                    com.Parameters["@date_entry"].Value = date_entry;
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
                            + " (select ROW_NUMBER() over (order by a.visa_id asc) rownumber, a.visa_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.expired_date,a.date_entry,a.entry_port"
                            + " from Visa a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'"+where+")"
                            + " as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by a.visa_id asc) rownumber, a.visa_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.expired_date,a.date_entry,a.entry_port"
                            + " from Visa a, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and c.student_group_id=e.studentGroup_id and d.[type]='Mobility' and e.staff_id=@current_staff_id"+where+")"
                            + " as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by a.visa_id asc) rownumber, a.visa_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.expired_date,a.date_entry,a.entry_port"
                            + " from Visa a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where + ")"
                            + " as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return visalist;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Visa visa = new Visa();
                    visa.visa_id = (int)reader.GetValue(reader.GetOrdinal("visa_id"));
                    visa.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    visa.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    visa.entry_port = (string)reader.GetValue(reader.GetOrdinal("entry_port"));
                    visa.date_entry = (DateTime)reader.GetValue(reader.GetOrdinal("date_entry"));
                    visa.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    visa.start_date = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    visa.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
                    visalist.Add(visa);
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
            return visalist;
        }
    }
}
