using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.Constant;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAOImpl
{
    public class AccountDAOImpl : AccountDAO
    {
        public bool checkAccountInactive(string username, string password)
        {
            SqlConnection con = null;
            String sql = "select count(*) from Users a where a.account = @account and a.[password] = @password and a.[status] = 1";
            SqlCommand com = null;
            bool result = false;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = username;
                com.Parameters.Add("@password", SqlDbType.NVarChar);
                com.Parameters["@password"].Value = password;
                int count = (int)com.ExecuteScalar();
                if (count > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return result;
        }

        public bool checkLogin(string username, string password)
        {
            SqlConnection con = null;
            String sql = "select count(*) from Users a where a.account = @account and a.[password] = @password";
            SqlCommand com = null;
            bool result = false;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = username;
                com.Parameters.Add("@password", SqlDbType.NVarChar);
                com.Parameters["@password"].Value = password;
                int count = (int)com.ExecuteScalar();
                if (count > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return result;
        }

        public Account GetAccount(string account, string password)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            Account user = new Account();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.[user_id],a.studentGroup_id,a.account,a.[password],a.[status],a.isFirstLoggedIn,b.role_name from Users a inner join Roles b on a.role_id = b.role_id where a.account = @account and a.[password] = @password";
                com.Parameters.Add("@account", SqlDbType.NVarChar);
                com.Parameters["@account"].Value = account;
                com.Parameters.Add("@password", SqlDbType.NVarChar);
                com.Parameters["@password"].Value = password;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    user.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("studentGroup_id")))
                    {
                        user.student_group_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                    }
                    user.username = (string)reader.GetValue(reader.GetOrdinal("account"));
                    user.password = (string)reader.GetValue(reader.GetOrdinal("password"));
                    user.role_name = (string)reader.GetValue(reader.GetOrdinal("role_name"));
                    user.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    user.isFirstLoggedIn = (bool)reader.GetValue(reader.GetOrdinal("isFirstLoggedIn"));
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
            return user;
        }

        public List<Account> GetAccounts()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Account> accounts = new List<Account>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = LoginConst.GET_ALL_ACCOUNT;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Account account = new Account();
                    account.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    account.username = (string)reader.GetValue(reader.GetOrdinal("account"));
                    account.password = (string)reader.GetValue(reader.GetOrdinal("password"));
                    account.role_name = (string)reader.GetValue(reader.GetOrdinal("role_name"));
                    account.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    account.isFirstLoggedIn = (bool)reader.GetValue(reader.GetOrdinal("isFirstLoggedIn"));
                    accounts.Add(account);
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
            return accounts;
        }

        public int GetTotalNotification(int user_id)
        {
            SqlConnection con = null;
            String sql = "";
            SqlCommand com = null;
            int total = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select count(*) from (select a.notification_information_id,a.[user_id],a.title,a.content from Notification_Information a where a.[user_id] = @user_id and a.isRead = 0) as temp";
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
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

        public List<WebNotification> GetWebNotifications(int user_id)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<WebNotification> webNotifications = new List<WebNotification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.notification_information_id,a.[user_id],a.title,a.content,a.isRead from Notification_Information a where a.[user_id] = @user_id order by a.created_date desc";
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    WebNotification notification = new WebNotification();
                    notification.noti_id = (int)reader.GetValue(reader.GetOrdinal("notification_information_id"));
                    notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    notification.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    notification.content = (string)reader.GetValue(reader.GetOrdinal("content"));
                    notification.isRead = (bool)reader.GetValue(reader.GetOrdinal("isRead"));
                    webNotifications.Add(notification);
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
            return webNotifications;
        }

        public bool haveDegree(int user_id)
        {
            SqlConnection con = null;
            String sql = " select count(*)"
                       + " from Users a, Coordinators b, Student_Group c, Programs d"
                       + " where b.staff_id=a.user_id and b.studentGroup_id=c.student_group_id and d.program_id=c.program_id and a.user_id=@user_id and d.type='Degree'";
            SqlCommand com = null;
            bool result = false;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                int count = (int)com.ExecuteScalar();
                if (count > 0)
                {
                    result = true;
                }
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return result;
        }

        public bool UpdateWebNotification(int noti_id, int user_id)
        {
            SqlConnection con = null;
            string sql = "update Notification_Information set isRead = 1  where notification_information_id = @notification_information_id and [user_id] = @user_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@notification_information_id", SqlDbType.Int);
                com.Parameters["@notification_information_id"].Value = noti_id;
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
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
    }
}
