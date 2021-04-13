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
    public class ContactInformationDAOImpl : ContactInformationDAO
    {
        public bool CreateOrEdit(int user_id, string telephone, string position, string picture)
        {
            SqlConnection con = null;
            string sql = "";
            if (String.IsNullOrEmpty(picture))
            {
                sql = "begin tran if exists (select * from Contact_Information with (updlock,serializable) " +
                      "where staff_id = @staff_id) begin update Contact_Information set telephone = @telephone," +
                      " position = @position where staff_id = @staff_id end else begin insert " +
                      "into Contact_Information(staff_id,telephone,picture,position) values (@staff_id,@telephone,@picture," +
                      "@position) end commit tran";
            }
            else
            {
                sql = "begin tran if exists (select * from Contact_Information with (updlock,serializable) " +
                      "where staff_id = @staff_id) begin update Contact_Information set telephone = @telephone, " +
                      "picture = @picture, position = @position where staff_id = @staff_id end else begin insert " +
                      "into Contact_Information(staff_id,telephone,picture,position) values (@staff_id,@telephone,@picture," +
                      "@position) end commit tran";
            }
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = user_id;
                com.Parameters.Add("@telephone", SqlDbType.NVarChar);
                com.Parameters["@telephone"].Value = telephone;
                com.Parameters.Add("@position", SqlDbType.NVarChar);
                com.Parameters["@position"].Value = position;
                com.Parameters.Add("@picture", SqlDbType.NVarChar);
                com.Parameters["@picture"].Value = picture;
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

        public ContactInformation GetContact(int current_staff_id)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            ContactInformation contact = new ContactInformation();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.contact_information_id,a.staff_id,b.[user_id],b.account,b.fullname," +
                      "b.email,a.telephone,a.picture,a.position from Users b FULL OUTER JOIN Contact_Information a " +
                      "on a.staff_id = b.[user_id] where b.[user_id] = @staff_id";
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = current_staff_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("contact_information_id")))
                    {
                        contact.contact_id = (int)reader.GetValue(reader.GetOrdinal("contact_information_id"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("staff_id")))
                    {
                        contact.staff_id = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    }
                    contact.staff_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    contact.staff_account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    contact.staff_email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("telephone")))
                    {
                        contact.staff_telephone = (string)reader.GetValue(reader.GetOrdinal("telephone"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        contact.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("position")))
                    {
                        contact.staff_position = (string)reader.GetValue(reader.GetOrdinal("position"));
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
            return contact;
        }

        public List<ContactInformation> GetContactInformation(int page, int pageSize, string staff_name, string staff_account, string staff_email, string telephone, string position, bool? status)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<ContactInformation> contactInformationList = new List<ContactInformation>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(staff_name))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = staff_name;
                }
                if (!string.IsNullOrEmpty(staff_account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = staff_account;
                }
                if (!string.IsNullOrEmpty(staff_email))
                {
                    where += " and upper(email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = staff_email;
                }
                if (!string.IsNullOrEmpty(telephone))
                {
                    where += " and upper(telephone) like upper('%' + @telephone + '%')";
                    com.Parameters.Add("@telephone", SqlDbType.NVarChar);
                    com.Parameters["@telephone"].Value = telephone;
                }
                if (!string.IsNullOrEmpty(position))
                {
                    where += " and upper(position) like upper('%' + @position + '%')";
                    com.Parameters.Add("@position", SqlDbType.NVarChar);
                    com.Parameters["@position"].Value = position;
                }
                if (status != null)
                {
                    where += " and status=@status";
                    com.Parameters.Add("@status", SqlDbType.Bit);
                    com.Parameters["@status"].Value = status;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select ROW_NUMBER() over (order by a.staff_id asc) rownumber,a.contact_information_id," +
                      "a.staff_id,b.account,b.fullname,b.email,b.[status],a.telephone,a.picture,a.position from Users b full outer " +
                      "join Contact_Information a on a.staff_id = b.[user_id] where b.role_id = 2" + where +") as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ContactInformation contact = new ContactInformation();
                    if (!reader.IsDBNull(reader.GetOrdinal("contact_information_id")))
                    {
                        contact.contact_id = (int)reader.GetValue(reader.GetOrdinal("contact_information_id"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("staff_id")))
                    {
                        contact.staff_id = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    }
                    contact.staff_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    contact.staff_account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    contact.staff_email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    contact.staff_status = (bool)reader.GetValue(reader.GetOrdinal("status"));
                    if (!reader.IsDBNull(reader.GetOrdinal("telephone")))
                    {
                        contact.staff_telephone = (string)reader.GetValue(reader.GetOrdinal("telephone"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        contact.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("position")))
                    {
                        contact.staff_position = (string)reader.GetValue(reader.GetOrdinal("position"));
                    }
                    contactInformationList.Add(contact);
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
            return contactInformationList;
        }

        public List<ContactInformation> GetContactInformationForStudent(int page, int pageSize, int student_id, string staff_name, string staff_email, string telephone, string position)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<ContactInformation> contactInformationList = new List<ContactInformation>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(staff_name))
                {
                    where += " and upper(temp.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = staff_name;
                }
                if (!string.IsNullOrEmpty(staff_email))
                {
                    where += " and upper(temp.email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = staff_email;
                }
                if (!string.IsNullOrEmpty(telephone))
                {
                    where += " and upper(temp.telephone) like upper('%' + @telephone + '%')";
                    com.Parameters.Add("@telephone", SqlDbType.NVarChar);
                    com.Parameters["@telephone"].Value = telephone;
                }
                if (!string.IsNullOrEmpty(position))
                {
                    where += " and upper(temp.position) like upper('%' + @position + '%')";
                    com.Parameters.Add("@position", SqlDbType.NVarChar);
                    com.Parameters["@position"].Value = position;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select ROW_NUMBER() over (order by temp.[user_id] asc) rownumber,d.[user_id] as Student_id,d.fullname " +
                      "as Student_Name,temp.[user_id],temp.fullname,temp.email,temp.telephone,temp.picture,temp.position,temp.studentGroup_id " +
                      "from Users d, (select c.[user_id],c.fullname,c.email,a.telephone,a.picture,a.position,b.studentGroup_id " +
                      "from Contact_Information a, Coordinators b, Users c where a.staff_id = b.staff_id and a.staff_id = c.[user_id]) " +
                      "as temp where d.studentGroup_id = temp.studentGroup_id and d.[user_id] = @student_id"+ where +") as " +
                      "temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ContactInformation contact = new ContactInformation();
                    contact.staff_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    contact.staff_email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("telephone")))
                    {
                        contact.staff_telephone = (string)reader.GetValue(reader.GetOrdinal("telephone"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        contact.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("position")))
                    {
                        contact.staff_position = (string)reader.GetValue(reader.GetOrdinal("position"));
                    }
                    contactInformationList.Add(contact);
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
            return contactInformationList;
        }

        public int GetTotalContactInformation(string staff_name, string staff_account, string staff_email, string telephone, string position, bool? status)
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
                if (!string.IsNullOrEmpty(staff_name))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = staff_name;
                }
                if (!string.IsNullOrEmpty(staff_account))
                {
                    where += " and upper(account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = staff_account;
                }
                if (!string.IsNullOrEmpty(staff_email))
                {
                    where += " and upper(email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = staff_email;
                }
                if (!string.IsNullOrEmpty(telephone))
                {
                    where += " and upper(telephone) like upper('%' + @telephone + '%')";
                    com.Parameters.Add("@telephone", SqlDbType.NVarChar);
                    com.Parameters["@telephone"].Value = telephone;
                }
                if (!string.IsNullOrEmpty(position))
                {
                    where += " and upper(position) like upper('%' + @position + '%')";
                    com.Parameters.Add("@position", SqlDbType.NVarChar);
                    com.Parameters["@position"].Value = position;
                }
                if (status != null)
                {
                    where += " and status=@status";
                    com.Parameters.Add("@status", SqlDbType.Bit);
                    com.Parameters["@status"].Value = status;
                }
                sql = "select count(*) from (select a.contact_information_id," +
                      "a.staff_id,b.account,b.fullname,b.email,b.[status],a.telephone,a.picture,a.position from Users b full outer " +
                      "join Contact_Information a on a.staff_id = b.[user_id] where b.role_id = 2" + where + ") as temp";
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

        public int GetTotalContactInformationForStudent(int student_id, string staff_name, string staff_email, string telephone, string position)
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
                if (!string.IsNullOrEmpty(staff_name))
                {
                    where += " and upper(temp.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = staff_name;
                }
                if (!string.IsNullOrEmpty(staff_email))
                {
                    where += " and upper(temp.email) like upper('%' + @email + '%')";
                    com.Parameters.Add("@email", SqlDbType.NVarChar);
                    com.Parameters["@email"].Value = staff_email;
                }
                if (!string.IsNullOrEmpty(telephone))
                {
                    where += " and upper(temp.telephone) like upper('%' + @telephone + '%')";
                    com.Parameters.Add("@telephone", SqlDbType.NVarChar);
                    com.Parameters["@telephone"].Value = telephone;
                }
                if (!string.IsNullOrEmpty(position))
                {
                    where += " and upper(temp.position) like upper('%' + @position + '%')";
                    com.Parameters.Add("@position", SqlDbType.NVarChar);
                    com.Parameters["@position"].Value = position;
                }
                sql = "select count(*) from (select d.[user_id] as Student_id,d.fullname " +
                      "as Student_Name,temp.[user_id],temp.fullname,temp.email,temp.telephone,temp.picture,temp.position,temp.studentGroup_id " +
                      "from Users d, (select c.[user_id],c.fullname,c.email,a.telephone,a.picture,a.position,b.studentGroup_id " +
                      "from Contact_Information a, Coordinators b, Users c where a.staff_id = b.staff_id and a.staff_id = c.[user_id]) " +
                      "as temp where d.studentGroup_id = temp.studentGroup_id and d.[user_id] = @student_id" + where + ") as temp";
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
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
    }
}