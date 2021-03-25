using System;
using System.Collections.Generic;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.Data.SqlClient;
using ISM.WebApp.Utils;
using System.Data;



namespace ISM.WebApp.DAOImpl
{
    public class VisaLettercsDAOImpl : VisaLetterDAO
    {
        public bool editVisaLetter(int id, string visa_type, string visa_period, string apply_receive)
        {
            SqlConnection con = null;
            string sql = "update Pre_Approval_Visa_Letter set visa_type = @visa_type, visa_period=@visa_period, apply_receive = @apply_receive where pre_approval_visa_letter_id = @pre_approval_visa_letter_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@pre_approval_visa_letter_id", SqlDbType.Int);
                com.Parameters["@pre_approval_visa_letter_id"].Value = id;
                com.Parameters.Add("@visa_type", SqlDbType.NVarChar);
                com.Parameters["@visa_type"].Value = visa_type;
                com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                com.Parameters["@visa_period"].Value = visa_period;
                com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                com.Parameters["@apply_receive"].Value = apply_receive;
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

        public List<VisaLetter> getAllVisaLetter()
        {
            SqlConnection con = null;
            string sql = "select p.pre_approval_visa_letter_id,u.fullname,u.gender,u.DOB,u.nationality,ps.passport_number,ps.expired_date,p.visa_type,p.visa_period,p.apply_receive from Pre_Approval_Visa_Letter p, Users u, Passports ps where  u.[user_id] = p.student_id and ps.student_id = p.student_id";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<VisaLetter> visaLetters = new List<VisaLetter>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    VisaLetter visaLetter = new VisaLetter();
                    visaLetter.pre_approval_visa_letter_id = (int)reader.GetValue(reader.GetOrdinal("pre_approval_visa_letter_id"));
                    visaLetter.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    visaLetter.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    visaLetter.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    visaLetter.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                   
                    visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    visaLetter.expire_date = (DateTime)reader.GetValue(reader.GetOrdinal("expire_date"));
                    visaLetter.visa_type = (string)reader.GetValue(reader.GetOrdinal("visa_type"));
                    visaLetter.visa_period = (string)reader.GetValue(reader.GetOrdinal("visa_period"));
                    visaLetter.apply_receive = (string)reader.GetValue(reader.GetOrdinal("apply_receive"));
                   
                    visaLetters.Add(visaLetter);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return visaLetters;
        }

        public int GetTotalVisaLetter(string fullname, string apply_receive, string visa_period, string type_visa,  string nationality, string passport_number, DateTime? dob, DateTime? expired_dateFrom, DateTime? expired_dateTo)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalVisaLetter = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (expired_dateTo != null)
                {
                    where += " and expired_date<=@expired_dateTo";
                    com.Parameters.Add("@expired_dateTo", SqlDbType.Date);
                    com.Parameters["@expired_dateTo"].Value = expired_dateTo;
                }
                if (expired_dateFrom != null)
                {
                    where += " and expired_date>=@expired_dateFrom";
                    com.Parameters.Add("@expired_dateFrom", SqlDbType.Date);
                    com.Parameters["@expired_dateFrom"].Value = expired_dateFrom;
                }
                if (!string.IsNullOrEmpty(apply_receive))
                {
                    where += " and upper(apply_receive) like upper('%' + @apply_receive + '%')";
                    com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                    com.Parameters["@apply_receive"].Value = apply_receive;
                }
                if (!string.IsNullOrEmpty(type_visa))
                {
                    where += " and upper(type_visa) like upper('%' + @type_visa + '%')";
                    com.Parameters.Add("@type_visa", SqlDbType.NVarChar);
                    com.Parameters["@type_visa"].Value = type_visa;
                }
                if (!string.IsNullOrEmpty(visa_period))
                {
                    where += " and upper(visa_period) like upper('%' + @visa_period + '%')";
                    com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                    com.Parameters["@visa_period"].Value = visa_period;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(nationality))
                {
                    where += " and upper(nationality) like upper('%' + @nationality + '%')";
                    com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                    com.Parameters["@nationality"].Value = nationality;
                }
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                
                if (dob != null)
                {
                    where += " and DOB >=@dob";
                    com.Parameters.Add("@dob", SqlDbType.Date);
                    com.Parameters["@dob"].Value = dob;
                }
                sql = "select pre_approval_visa_letter_id,u.fullname,u.gender,u.DOB,u.nationality, ps.passport_number,ps.expired_date,p.visa_type,p.visa_period,p.apply_receive from Pre_Approval_Visa_Letter p, Users u, Passports ps  where  u.[user_id] = p.student_id and ps.student_id = p.student_id" + where;
                com.CommandText = sql;
                totalVisaLetter = (int)com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalVisaLetter;
        }
        public VisaLetter GetVisaLetterById(int id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            VisaLetter visaLetter = new VisaLetter();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (id != 0)
                {
                    where += " p.pre_approval_visa_letter_id = @id";
                    com.Parameters.Add("@id", SqlDbType.Int);
                    com.Parameters["@id"].Value = id;
                }
                sql = "select * from (select p.pre_approval_visa_letter_id,u.fullname,u.gender,u.DOB,u.nationality,ps.passport_number,ps.expired_date,p.visa_type,p.visa_period,p.apply_receive from Pre_Approval_Visa_Letter p, Users u, Passports ps"
                    + where + ") as temp";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    visaLetter.pre_approval_visa_letter_id = (int)reader.GetValue(reader.GetOrdinal("pre_approval_visa_letter_id"));
                    visaLetter.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    visaLetter.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    visaLetter.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    visaLetter.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                    visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    visaLetter.expire_date = (DateTime)reader.GetValue(reader.GetOrdinal("expire_date"));
                    visaLetter.visa_type = (string)reader.GetValue(reader.GetOrdinal("visa_type"));
                    visaLetter.visa_period = (string)reader.GetValue(reader.GetOrdinal("visa_period"));
                    visaLetter.apply_receive = (string)reader.GetValue(reader.GetOrdinal("apply_receive"));

                   
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
            return visaLetter;
        }

        public List<VisaLetter> GetVisaLetter(int page, int pagesize, string fullname, string apply_receive, string visa_period, string type_visa, string nationality, string passport_number
           , DateTime? dob, DateTime? expired_dateFrom, DateTime? expired_dateTo)
        {
            int from = page * pagesize - (pagesize - 1);
            int to = page * pagesize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<VisaLetter> visaLetters = new List<VisaLetter>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (expired_dateTo != null)
                {
                    where += " and expired_date<=@expired_dateTo";
                    com.Parameters.Add("@expired_dateTo", SqlDbType.Date);
                    com.Parameters["@expired_dateTo"].Value = expired_dateTo;
                }
                if (expired_dateFrom != null)
                {
                    where += " and expired_date>=@expired_dateFrom";
                    com.Parameters.Add("@expired_dateFrom", SqlDbType.Date);
                    com.Parameters["@expired_dateFrom"].Value = expired_dateFrom;
                }
                if (!string.IsNullOrEmpty(apply_receive))
                {
                    where += " and upper(apply_receive) like upper('%' + @apply_receive + '%')";
                    com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                    com.Parameters["@apply_receive"].Value = apply_receive;
                }
                if (!string.IsNullOrEmpty(type_visa))
                {
                    where += " and upper(type_visa) like upper('%' + @type_visa + '%')";
                    com.Parameters.Add("@type_visa", SqlDbType.NVarChar);
                    com.Parameters["@type_visa"].Value = type_visa;
                }
                if (!string.IsNullOrEmpty(visa_period))
                {
                    where += " and upper(visa_period) like upper('%' + @visa_period + '%')";
                    com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                    com.Parameters["@visa_period"].Value = visa_period;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(nationality))
                {
                    where += " and upper(nationality) like upper('%' + @nationality + '%')";
                    com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                    com.Parameters["@nationality"].Value = nationality;
                }
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                
                if (dob != null)
                {
                    where += " and DOB >=@dob";
                    com.Parameters.Add("@dob", SqlDbType.Date);
                    com.Parameters["@dob"].Value = dob;
                }
                sql = "select pre_approval_visa_letter_id,u.fullname,u.gender,u.DOB,u.nationality, ps.passport_number,ps.expired_date,p.visa_type,p.visa_period,p.apply_receive from Pre_Approval_Visa_Letter p, Users u, Passports ps  where  u.[user_id] = p.student_id and ps.student_id = p.student_id" + where;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    VisaLetter visaLetter = new VisaLetter();
                    visaLetter.pre_approval_visa_letter_id = (int)reader.GetValue(reader.GetOrdinal("pre_approval_visa_letter_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        visaLetter.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("visa_type")))
                    {
                        visaLetter.visa_type = (string)reader.GetValue(reader.GetOrdinal("visa_type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("visa_period")))
                    {
                        visaLetter.visa_period = (string)reader.GetValue(reader.GetOrdinal("visa_period"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("apply_receive")))
                    {
                        visaLetter.apply_receive = (string)reader.GetValue(reader.GetOrdinal("apply_receive"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("nationality")))
                    {
                        visaLetter.nationality = (string)reader.GetValue(reader.GetOrdinal("nationality"));
                    }
                   
                    if (!reader.IsDBNull(reader.GetOrdinal("passport_number")))
                    {
                        visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        visaLetter.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("dob")))
                    {
                        visaLetter.dob = (DateTime)reader.GetValue(reader.GetOrdinal("dob"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("expired_date")))
                    {
                        visaLetter.expire_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
                    }
                    visaLetters.Add(visaLetter);
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
            return visaLetters;
        }
    }
}
