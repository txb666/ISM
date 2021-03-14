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
    public class PassportDAOImpl : PassportDAO
    {
        public int createPassport(int student_id, string passport_number, DateTime start_date, DateTime expried_date, string issuing_authority)
        {
            throw new NotImplementedException();
        }

        public void editPassport(int passport_id, string passport_number, DateTime start_date, DateTime expried_date, string issuing_authoriry)
        {
            SqlConnection con = null;
            string sql = "update Passports set passport_number=@passport_number,[start_date]=@start_date,expried_date=@expried_date,issuing_authoriry=@issuing_authoriry"
                        + "where passport_id=@passport_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@passport_id", SqlDbType.Int);
                com.Parameters["@passport_id"].Value = passport_id;
                com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                com.Parameters["@passport_number"].Value = passport_number;
                com.Parameters.Add("@start_date", SqlDbType.Date);
                com.Parameters["@start_date"].Value = start_date;
                com.Parameters.Add("@expried_date", SqlDbType.Date);
                com.Parameters["@expried_date"].Value = expried_date;
                com.Parameters.Add("@issuing_authoriry", SqlDbType.NVarChar);
                com.Parameters["@issuing_authoriry"].Value = issuing_authoriry;
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

        public Passport GetPassportById(int id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            Passport passport = new Passport();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (id != 0)
                {
                    where += " and a.passport_id=@id";
                    com.Parameters.Add("@id", SqlDbType.Int);
                    com.Parameters["@id"].Value = id;
                }
                sql = "select * from (select b.account,a.passport_id,b.fullname,a.passport_number,a.picture," +
                      "a.[start_date],a.expired_date,a.issuing_authority from Passports a, Users b " +
                      "where a.student_id = b.[user_id]" + where + ") as temp";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    passport.passport_id = (int)reader.GetValue(reader.GetOrdinal("passport_id"));
                    passport.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    passport.student_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    passport.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    passport.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    passport.start_date = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    passport.expried_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
                    passport.issuing_authority = (string)reader.GetValue(reader.GetOrdinal("issuing_authority"));
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
            return passport;
        }

        public List<Passport> GetPassports(int page, int pageSize, int id, string account, string picture, string student_name, string passport_number, DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expried_dateFrom, DateTime? expried_dateTo, string issuing_authority)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Passport> passports = new List<Passport>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (id != 0)
                {
                    where += " and a.passport_id=@id";
                    com.Parameters.Add("@id", SqlDbType.Int);
                    com.Parameters["@id"].Value = id;
                }
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
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(a.passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                if (!string.IsNullOrEmpty(issuing_authority))
                {
                    where += " and upper(a.issuing_authority) like upper('%' + @issuing_authority + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@issuing_authority"].Value = issuing_authority;
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
                if (expried_dateFrom != null)
                {
                    where += " and a.end_date>=@expried_dateFrom";
                    com.Parameters.Add("@expried_dateFrom", SqlDbType.Date);
                    com.Parameters["@expried_dateFrom"].Value = expried_dateFrom;
                }
                if (expried_dateTo != null)
                {
                    where += " and a.end_date<=@expried_dateTo";
                    com.Parameters.Add("@expried_dateTo", SqlDbType.Date);
                    com.Parameters["@expried_dateTo"].Value = expried_dateTo;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select b.account,a.passport_id,b.fullname,a.passport_number,a.picture,a.[start_date],a.expired_date,a.issuing_authority," +
                      "ROW_NUMBER() over (order by a.passport_id asc) as rownumber from Passports a, Users b where a.student_id = b.[user_id] " + where + ") " +
                      "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Passport passport = new Passport();
                    passport.passport_id = (int)reader.GetValue(reader.GetOrdinal("passport_id"));
                    passport.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    passport.student_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    passport.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    passport.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    passport.start_date = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    passport.expried_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
                    passport.issuing_authority = (string)reader.GetValue(reader.GetOrdinal("issuing_authority"));
                    passports.Add(passport);
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
            return passports;
        }

        public int GetTotalPassports(int id, string account, string picture, string student_name, string passport_number, DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expried_dateFrom, DateTime? expried_dateTo, string issuing_authority)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalPassports = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (id != 0)
                {
                    where += " and a.passport_id=@id";
                    com.Parameters.Add("@id", SqlDbType.Int);
                    com.Parameters["@id"].Value = id;
                }
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(student_name))
                {
                    where += " and upper(fullname) like upper('%' + @student_name + '%')";
                    com.Parameters.Add("@student_name", SqlDbType.NVarChar);
                    com.Parameters["@student_name"].Value = student_name;
                }
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                if (!string.IsNullOrEmpty(issuing_authority))
                {
                    where += " and upper(issuing_authority) like upper('%' + @issuing_authority + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@issuing_authority"].Value = issuing_authority;
                }
                if (start_dateFrom != null)
                {
                    where += " and start_date>=@start_dateFrom";
                    com.Parameters.Add("@start_dateFrom", SqlDbType.Date);
                    com.Parameters["@start_dateFrom"].Value = start_dateFrom;
                }
                if (start_dateTo != null)
                {
                    where += " and start_date<=@start_dateTo";
                    com.Parameters.Add("@start_dateTo", SqlDbType.Date);
                    com.Parameters["@start_dateTo"].Value = start_dateTo;
                }
                if (expried_dateFrom != null)
                {
                    where += " and expired_date>=@expried_dateFrom";
                    com.Parameters.Add("@expried_dateFrom", SqlDbType.Date);
                    com.Parameters["@expried_dateFrom"].Value = expried_dateFrom;
                }
                if (expried_dateTo != null)
                {
                    where += " and expired_date<=@expried_dateTo";
                    com.Parameters.Add("@expried_dateTo", SqlDbType.Date);
                    com.Parameters["@expried_dateTo"].Value = expried_dateTo;
                }
                sql = "select count(*) from (select a.passport_id,b.account,b.fullname,a.passport_number,a.picture," +
                      "a.[start_date],a.expired_date,a.issuing_authority from Passports a, Users b " +
                      "where a.student_id = b.[user_id] " + where + ") as temp";
                com.CommandText = sql;
                totalPassports = (int)com.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalPassports;
        }

        public bool isPassportAlreadyExist(string account, string passport_number)
        {
            SqlConnection con = null;
            string sql = "select count(*) from (select b.account,a.passport_number from Passports a, Users b " +
                         "where a.student_id = b.[user_id]) as temp where temp.account=@account or temp.passport_number=@passport_number";
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
                com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                com.Parameters["@passport_number"].Value = passport_number;
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
                DBUtils.closeAllResource(con, com, null, null);
            }
            return isExist;
        }
    }
}