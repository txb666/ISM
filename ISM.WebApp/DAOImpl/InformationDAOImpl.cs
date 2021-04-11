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
    public class InformationDAOImpl : InformationDAO
    {
        public bool ChangePassword(int user_id, string new_password)
        {
            SqlConnection con = null;
            string sql = "update Users set [password] = @new_password where [user_id] = @user_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@new_password", SqlDbType.NVarChar);
                com.Parameters["@new_password"].Value = new_password;
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

        public bool EditInformation(int user_id, string fullname, string nationality, DateTime dob, bool gender, string contact, string picture)
        {
            SqlConnection con = null;
            string sql = "";
            if (String.IsNullOrEmpty(picture))
            {
                sql = "update Users set fullname = @fullname, nationality = @nationality, DOB = @dob, " +
                      "gender = @gender, emergency_contact = @contact  where [user_id] = @user_id";
            }
            else
            {
                sql = "update Users set fullname = @fullname, nationality = @nationality, DOB = @dob, " +
                      "gender = @gender, emergency_contact = @contact, picture = @picture  where [user_id] = @user_id";
            }
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                com.Parameters["@fullname"].Value = fullname;
                com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                com.Parameters["@nationality"].Value = nationality;
                com.Parameters.Add("@dob", SqlDbType.Date);
                com.Parameters["@dob"].Value = dob;
                com.Parameters.Add("@gender", SqlDbType.Bit);
                com.Parameters["@gender"].Value = gender;
                com.Parameters.Add("@contact", SqlDbType.NVarChar);
                com.Parameters["@contact"].Value = contact;
                com.Parameters.Add("@picture", SqlDbType.NVarChar);
                com.Parameters["@picture"].Value = picture;
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

        public User GetInformation(int user_id)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            User user = new User();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.fullname,a.nationality,a.DOB,a.gender,a.emergency_contact,a.picture from Users a where a.[user_id] = @user_id";
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    user.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    if (!reader.IsDBNull(reader.GetOrdinal("nationality")))
                    {
                        user.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                    {
                        user.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        user.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("emergency_contact")))
                    {
                        user.emergency_contact = (string)reader.GetValue(reader.GetOrdinal("emergency_contact"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        user.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
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

        public bool isPasswordMatch(string current_password)
        {
            SqlConnection con = null;
            string sql = "select count(*) from Users a where a.[password] = @password";
            SqlDataReader reader = null;
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@password", SqlDbType.NVarChar);
                com.Parameters["@password"].Value = current_password;
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
