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
                sql = Login.GET_ALL_ACCOUNT;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Account account = new Account();
                    account.username = (string)reader.GetValue(reader.GetOrdinal("account"));
                    account.password = (string)reader.GetValue(reader.GetOrdinal("password"));
                    account.role_id = (int)reader.GetValue(reader.GetOrdinal("role_id"));
                    account.status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    account.isFirstLoggedIn = (bool)reader.GetValue(reader.GetOrdinal("isFirstLoggedIn"));
                    accounts.Add(account);
                }
                
                return accounts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return null;
        }
    }
}
