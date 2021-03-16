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
        public int GetTotalVisa(string account, string picture, string student_name,
            DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expired_dateFrom, DateTime? expired_dateTo,
            DateTime? entry_dateFrom, DateTime? entry_dateTo, string entry_port)
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
                if (!string.IsNullOrEmpty(student_name))
                {
                    where += " and upper(b.fullname) like upper('%' + @student_name + '%')";
                    com.Parameters.Add("@student_name", SqlDbType.NVarChar);
                    com.Parameters["@student_name"].Value = student_name;
                }
                if (!string.IsNullOrEmpty(entry_port))
                {
                    where += " and upper(a.entry_port) like upper('%' + @entry_port + '%')";
                    com.Parameters.Add("@entry_port", SqlDbType.NVarChar);
                    com.Parameters["@entry_port"].Value = entry_port;
                }
                if (start_dateFrom != null)
                {
                    where += " and a.[start_date]>=@start_dateFrom";
                    com.Parameters.Add("@start_dateFrom", SqlDbType.Date);
                    com.Parameters["@start_dateFrom"].Value = start_dateFrom;
                }
                if (start_dateTo != null)
                {
                    where += " and a.[start_date]<=@start_dateTo";
                    com.Parameters.Add("@start_dateTo", SqlDbType.Date);
                    com.Parameters["@start_dateTo"].Value = start_dateTo;
                }
                if (expired_dateFrom != null)
                {
                    where += " and a.expired_date>=@expired_dateFrom";
                    com.Parameters.Add("@expired_dateFrom", SqlDbType.Date);
                    com.Parameters["@expired_dateFrom"].Value = expired_dateFrom;
                }
                if (expired_dateTo != null)
                {
                    where += " and a.expired_date<=@expired_dateTo";
                    com.Parameters.Add("@expired_dateTo", SqlDbType.Date);
                    com.Parameters["@expired_dateTo"].Value = expired_dateTo;
                }
                if (entry_dateFrom != null)
                {
                    where += " and a.entry_date>=@entry_dateFrom";
                    com.Parameters.Add("@entry_dateFrom", SqlDbType.Date);
                    com.Parameters["@entry_dateFrom"].Value = entry_dateFrom;
                }
                if (entry_dateTo != null)
                {
                    where += " and a.entry_date<=@entry_dateTo";
                    com.Parameters.Add("@entry_dateTo", SqlDbType.Date);
                    com.Parameters["@entry_dateTo"].Value = entry_dateTo;
                }
                sql = "select * from(select b.account,b.fullname,a.visa_id,a.picture,a.entry_port,a.date_entry,a.[start_date],a.expired_date " +
                      "from Visa a, Users b where a.student_id = b.[user_id]"+where+") as temp";
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

        public List<Visa> GetVisa(int page, int pageSize, string account, string picture, string student_name,
            DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expired_dateFrom, DateTime? expired_dateTo,
            DateTime? entry_dateFrom, DateTime? entry_dateTo, string entry_port)
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
                if (!string.IsNullOrEmpty(student_name))
                {
                    where += " and upper(b.fullname) like upper('%' + @student_name + '%')";
                    com.Parameters.Add("@student_name", SqlDbType.NVarChar);
                    com.Parameters["@student_name"].Value = student_name;
                }
                if (!string.IsNullOrEmpty(entry_port))
                {
                    where += " and upper(a.entry_port) like upper('%' + @entry_port + '%')";
                    com.Parameters.Add("@entry_port", SqlDbType.NVarChar);
                    com.Parameters["@entry_port"].Value = entry_port;
                }
                if (start_dateFrom != null)
                {
                    where += " and a.[start_date]>=@start_dateFrom";
                    com.Parameters.Add("@start_dateFrom", SqlDbType.Date);
                    com.Parameters["@start_dateFrom"].Value = start_dateFrom;
                }
                if (start_dateTo != null)
                {
                    where += " and a.[start_date]<=@start_dateTo";
                    com.Parameters.Add("@start_dateTo", SqlDbType.Date);
                    com.Parameters["@start_dateTo"].Value = start_dateTo;
                }
                if (expired_dateFrom != null)
                {
                    where += " and a.expired_date>=@expired_dateFrom";
                    com.Parameters.Add("@expired_dateFrom", SqlDbType.Date);
                    com.Parameters["@expired_dateFrom"].Value = expired_dateFrom;
                }
                if (expired_dateTo != null)
                {
                    where += " and a.expired_date<=@expired_dateTo";
                    com.Parameters.Add("@expired_dateTo", SqlDbType.Date);
                    com.Parameters["@expired_dateTo"].Value = expired_dateTo;
                }
                if (entry_dateFrom != null)
                {
                    where += " and a.entry_date>=@entry_dateFrom";
                    com.Parameters.Add("@entry_dateFrom", SqlDbType.Date);
                    com.Parameters["@entry_dateFrom"].Value = entry_dateFrom;
                }
                if (entry_dateTo != null)
                {
                    where += " and a.entry_date<=@entry_dateTo";
                    com.Parameters.Add("@entry_dateTo", SqlDbType.Date);
                    com.Parameters["@entry_dateTo"].Value = entry_dateTo;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from(select b.account,b.fullname,a.visa_id,a.picture,a.entry_port,a.date_entry,a.[start_date],a.expired_date," +
                      "ROW_NUMBER() over (order by a.visa_id asc) as rownumber from Visa a, Users b where a.student_id = b.[user_id])"+where+" " +
                      "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Visa visa = new Visa();
                    visa.visa_id = (int)reader.GetValue(reader.GetOrdinal("visa_id"));
                    visa.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    visa.student_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
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
