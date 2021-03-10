using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ISM.WebApp.Utils;
using System.Data;

namespace ISM.WebApp.DAOImpl
{
    public class UserDAOImpl : UserDAO
    {
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
                com.Parameters["@password"].Value = account;
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

        public void editStaff(int user_id, string fullname, string email, string account, DateTime? startDate, DateTime? endDate, bool status)
        {
            SqlConnection con = null;
            string sql = "update Users set fullname=@fullname, email=@email, account=@account,[start_date]=@start_date,end_date=@end_date,[status]=@status"
                        + "where user_id=@user_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                com.Parameters["@fullname"].Value = fullname;
                com.Parameters.Add("@email", SqlDbType.NVarChar);
                com.Parameters["@email"].Value = email;
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
                com.Parameters.Add("@start_date", SqlDbType.Date);
                if (startDate != null)
                {      
                    com.Parameters["@start_date"].Value = startDate;
                }
                else
                {
                    com.Parameters["@start_date"].Value = null;
                }
                com.Parameters.Add("@end_date", SqlDbType.Date);
                if (startDate != null)
                {
                    com.Parameters["@start_date"].Value = startDate;
                }
                else
                {
                    com.Parameters["@start_date"].Value = null;
                }
                com.Parameters.Add("@status", SqlDbType.Bit);
                com.Parameters["@status"].Value = status;
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

        public List<User> GetStaff(int page, int pageSize, string fullname, string email, string account, bool? status, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo)
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
                if (startDateFrom != null)
                {
                    where += " and start_date>=@startDateFrom";
                    com.Parameters.Add("@startDateFrom", SqlDbType.Date);
                    com.Parameters["@startDateFrom"].Value = startDateFrom;
                }
                if (startDateTo != null)
                {
                    where += " and start_date<=@startDateTo";
                    com.Parameters.Add("@startDateTo", SqlDbType.Date);
                    com.Parameters["@startDateTo"].Value = startDateTo;
                }
                if (endDateFrom != null)
                {
                    where += " and end_date>=@endDateFrom";
                    com.Parameters.Add("@endDateFrom", SqlDbType.Date);
                    com.Parameters["@endDateFrom"].Value = endDateFrom;
                }
                if (endDateTo != null)
                {
                    where += " and end_date<=@endDateTo";
                    com.Parameters.Add("@endDateTo", SqlDbType.Date);
                    com.Parameters["@endDateTo"].Value = endDateTo;
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

        public int getTotalStaff(string fullname, string email, string account, bool? status, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo)
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
                if (startDateFrom != null)
                {
                    where += " and start_date>=@startDateFrom";
                    com.Parameters.Add("@startDateFrom", SqlDbType.Date);
                    com.Parameters["@startDateFrom"].Value = startDateFrom;
                }
                if (startDateTo != null)
                {
                    where += " and start_date<=@startDateTo";
                    com.Parameters.Add("@startDateTo", SqlDbType.Date);
                    com.Parameters["@startDateTo"].Value = startDateTo;
                }
                if (endDateFrom != null)
                {
                    where += " and end_date>=@endDateFrom";
                    com.Parameters.Add("@endDateFrom", SqlDbType.Date);
                    com.Parameters["@endDateFrom"].Value = endDateFrom;
                }
                if (endDateTo != null)
                {
                    where += " and end_date<=@endDateTo";
                    com.Parameters.Add("@endDateTo", SqlDbType.Date);
                    com.Parameters["@endDateTo"].Value = endDateTo;
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
    }
}
