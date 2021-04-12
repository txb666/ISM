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
                    visaLetter.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
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

        public int GetTotalVisaLetter(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string fullname, string apply_receive, string visa_period, string visa_type, string nationality, string passport_number
           , DateTime? dob, DateTime? expired_date)
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
                if (expired_date != null)
                {
                    where += " and b.expired_date=@expired_date";
                    com.Parameters.Add("@expired_date", SqlDbType.Date);
                    com.Parameters["@expired_date"].Value = expired_date;
                }
                if (!string.IsNullOrEmpty(apply_receive))
                {
                    where += " and upper(a.apply_receive) like upper('%' + @apply_receive + '%')";
                    com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                    com.Parameters["@apply_receive"].Value = apply_receive;
                }
                if (!string.IsNullOrEmpty(visa_type))
                {
                    where += " and upper(a.visa_type) like upper('%' + @visa_type + '%')";
                    com.Parameters.Add("@visa_type", SqlDbType.NVarChar);
                    com.Parameters["@visa_type"].Value = visa_type;
                }
                if (!string.IsNullOrEmpty(visa_period))
                {
                    where += " and upper(a.visa_period) like upper('%' + @visa_period + '%')";
                    com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                    com.Parameters["@visa_period"].Value = visa_period;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(c.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(nationality))
                {
                    where += " and upper(c.nationality) like upper('%' + @nationality + '%')";
                    com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                    com.Parameters["@nationality"].Value = nationality;
                }
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(b.passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                if (dob != null)
                {
                    where += " and c.DOB=@dob";
                    com.Parameters.Add("@dob", SqlDbType.Date);
                    com.Parameters["@dob"].Value = dob;
                }             
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id inner join Users c on a.student_id=c.[user_id] inner join Student_Group d on c.studentGroup_id=d.student_group_id"
                            + " inner join Programs e on d.program_id=e.program_id"
                            + " where e.[type]='Mobility'" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id inner join Users c on a.student_id=c.[user_id] inner join Student_Group d on c.studentGroup_id=d.student_group_id"
                            + " inner join Programs e on d.program_id=e.program_id inner join Coordinators f on d.student_group_id=f.studentGroup_id"
                            + " where e.[type]='Mobility' and f.staff_id=@current_staff_id" + where;
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select count(*)"
                           + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id inner join Users c on a.student_id=c.[user_id] inner join Student_Group d on c.studentGroup_id=d.student_group_id"
                           + " inner join Programs e on d.program_id=e.program_id"
                           + " where e.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalVisaLetter;
                    }
                }
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
                    visaLetter.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expire_date"));
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

        public List<VisaLetter> GetVisaLetter(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pagesize, string fullname, string apply_receive, string visa_period, string visa_type, string nationality, string passport_number
           , DateTime? dob, DateTime? expired_date)
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
                if (expired_date != null)
                {
                    where += " and b.expired_date=@expired_date";
                    com.Parameters.Add("@expired_date", SqlDbType.Date);
                    com.Parameters["@expired_date"].Value = expired_date;
                }              
                if (!string.IsNullOrEmpty(apply_receive))
                {
                    where += " and upper(a.apply_receive) like upper('%' + @apply_receive + '%')";
                    com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                    com.Parameters["@apply_receive"].Value = apply_receive;
                }
                if (!string.IsNullOrEmpty(visa_type))
                {
                    where += " and upper(a.visa_type) like upper('%' + @visa_type + '%')";
                    com.Parameters.Add("@visa_type", SqlDbType.NVarChar);
                    com.Parameters["@visa_type"].Value = visa_type;
                }
                if (!string.IsNullOrEmpty(visa_period))
                {
                    where += " and upper(a.visa_period) like upper('%' + @visa_period + '%')";
                    com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                    com.Parameters["@visa_period"].Value = visa_period;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(c.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(nationality))
                {
                    where += " and upper(c.nationality) like upper('%' + @nationality + '%')";
                    com.Parameters.Add("@nationality", SqlDbType.NVarChar);
                    com.Parameters["@nationality"].Value = nationality;
                }
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(b.passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }                
                if (dob != null)
                {
                    where += " and c.DOB=@dob";
                    com.Parameters.Add("@dob", SqlDbType.Date);
                    com.Parameters["@dob"].Value = dob;
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
                            + " select ROW_NUMBER() over (order by a.pre_approval_visa_letter_id asc) rownumber, a.pre_approval_visa_letter_id,a.student_id,c.fullname,c.gender,c.DOB,c.nationality,d.home_univercity,b.passport_number,b.expired_date,a.visa_type,a.visa_period,a.apply_receive"
                            + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id inner join Users c on a.student_id=c.[user_id] inner join Student_Group d on c.studentGroup_id=d.student_group_id"
                            + " inner join Programs e on d.program_id=e.program_id"
                            + " where e.[type]='Mobility'" + where
                            + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from("
                            + " select ROW_NUMBER() over (order by a.pre_approval_visa_letter_id asc) rownumber, a.pre_approval_visa_letter_id,a.student_id,c.fullname,c.gender,c.DOB,c.nationality,d.home_univercity,b.passport_number,b.expired_date,a.visa_type,a.visa_period,a.apply_receive"
                            + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id inner join Users c on a.student_id=c.[user_id] inner join Student_Group d on c.studentGroup_id=d.student_group_id"
                            + " inner join Programs e on d.program_id=e.program_id inner join Coordinators f on d.student_group_id=f.studentGroup_id"
                            + " where e.[type]='Mobility' and f.staff_id=@current_staff_id" + where
                            + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select * from("
                           + " select ROW_NUMBER() over (order by a.pre_approval_visa_letter_id asc) rownumber, a.pre_approval_visa_letter_id,a.student_id,c.fullname,c.gender,c.DOB,c.nationality,d.home_univercity,b.passport_number,b.expired_date,a.visa_type,a.visa_period,a.apply_receive"
                           + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id inner join Users c on a.student_id=c.[user_id] inner join Student_Group d on c.studentGroup_id=d.student_group_id"
                           + " inner join Programs e on d.program_id=e.program_id"
                           + " where e.[type]='Degree'" + where
                           + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return visaLetters;
                    }
                }
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
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        visaLetter.home_univercity = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("passport_number")))
                    {
                        visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        visaLetter.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                    {
                        visaLetter.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("expired_date")))
                    {
                        visaLetter.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
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

        public VisaLetter GetVisaLetter(int student_id)
        {
            SqlConnection con = null;
            string sql = " select a.pre_approval_visa_letter_id,a.student_id,c.fullname,c.gender,c.DOB,c.nationality,d.home_univercity,b.passport_number,b.expired_date,a.visa_type,a.visa_period,a.apply_receive"
                       + " from Pre_Approval_Visa_Letter a left join Passports b on a.student_id=b.student_id right join Users c on a.student_id=c.[user_id]"
                       + " left join Student_Group d on c.studentGroup_id=d.student_group_id"
                       + " where c.[user_id]=@student_id";
            SqlDataReader reader = null;
            SqlCommand com = null;
            VisaLetter visaLetter = new VisaLetter();
            visaLetter.student_id = student_id;                    
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql,con);
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("pre_approval_visa_letter_id")))
                    {
                        visaLetter.pre_approval_visa_letter_id = (int)reader.GetValue(reader.GetOrdinal("pre_approval_visa_letter_id"));
                    }
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
                    if (!reader.IsDBNull(reader.GetOrdinal("home_univercity")))
                    {
                        visaLetter.home_univercity = (string)reader.GetValue(reader.GetOrdinal("home_univercity"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("passport_number")))
                    {
                        visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                    {
                        visaLetter.gender = (bool)reader.GetValue(reader.GetOrdinal("gender"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                    {
                        visaLetter.dob = (DateTime)reader.GetValue(reader.GetOrdinal("DOB"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("expired_date")))
                    {
                        visaLetter.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
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
            return visaLetter;
        }

        public List<VisaLetter> GetVisaLettersAdminToExcel()
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<VisaLetter> visaLetters = new List<VisaLetter>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select temp.student_id,temp.fullname,temp.email,e.passport_number,e.expired_date,temp.visa_type,temp.visa_period from" +
                      " (select a.student_id,b.fullname,b.email,a.visa_type,a.visa_period from Pre_Approval_Visa_Letter a, Users b where " +
                      "a.student_id = b.[user_id]) as temp FULL OUTER JOIN Passports e on e.student_id = temp.student_id where temp.student_id != ''";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    VisaLetter visaLetter = new VisaLetter();
                    visaLetter.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    visaLetter.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    visaLetter.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("passport_number")))
                    {
                        visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("expired_date")))
                    {
                        visaLetter.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("visa_type")))
                    {
                        visaLetter.visa_type = (string)reader.GetValue(reader.GetOrdinal("visa_type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("visa_period")))
                    {
                        visaLetter.visa_period = (string)reader.GetValue(reader.GetOrdinal("visa_period"));
                    }
                    visaLetters.Add(visaLetter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return visaLetters;
        }

        public List<VisaLetter> GetVisaLettersStaffToExcel(int staff_id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<VisaLetter> visaLetters = new List<VisaLetter>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select temp.student_id,temp.fullname,temp.email,e.passport_number,e.expired_date,temp.visa_type,temp.visa_period " +
                      "from (select a.student_id,b.fullname,b.email,a.visa_type,a.visa_period,d.staff_id from Pre_Approval_Visa_Letter a, " +
                      "Users b, Student_Group c, Coordinators d where a.student_id = b.[user_id] and b.studentGroup_id = " +
                      "c.student_group_id and c.student_group_id = d.studentGroup_id) as temp FULL OUTER JOIN Passports e " +
                      "on e.student_id = temp.student_id where temp.staff_id = @staff_id";
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    VisaLetter visaLetter = new VisaLetter();
                    visaLetter.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    visaLetter.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    visaLetter.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("passport_number")))
                    {
                        visaLetter.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("expired_date")))
                    {
                        visaLetter.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("visa_type")))
                    {
                        visaLetter.visa_type = (string)reader.GetValue(reader.GetOrdinal("visa_type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("visa_period")))
                    {
                        visaLetter.visa_period = (string)reader.GetValue(reader.GetOrdinal("visa_period"));
                    }
                    visaLetters.Add(visaLetter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return visaLetters;
        }

        public bool CreateOrEditVisaLetter(int student_id, int visa_letter_id, string visa_type, string visa_period, string apply_receive)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "";
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                sql = " select count(*) from Pre_Approval_Visa_Letter where student_id=@student_id";
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@student_id", SqlDbType.Int);
                com.Parameters["@student_id"].Value = student_id;
                int count = (int)com.ExecuteScalar();
                if (count == 0)
                {
                    sql = " insert into Pre_Approval_Visa_Letter(student_id,visa_type,visa_period,apply_receive)"
                        + " values(@student_id,@visa_type,@visa_period,@apply_receive)";
                    com = new SqlCommand(sql, con);
                    com.Parameters.Add("@student_id", SqlDbType.Int);
                    com.Parameters["@student_id"].Value = student_id;
                    com.Parameters.Add("@visa_type", SqlDbType.NVarChar);
                    com.Parameters["@visa_type"].Value = visa_type;
                    com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                    com.Parameters["@visa_period"].Value = visa_period;
                    com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                    com.Parameters["@apply_receive"].Value = apply_receive;
                    com.ExecuteNonQuery();
                }
                else
                {
                    sql = " update Pre_Approval_Visa_Letter set visa_type=@visa_type, visa_period=@visa_period, apply_receive=@apply_receive"
                        + " where pre_approval_visa_letter_id=@pre_approval_visa_letter_id";
                    com = new SqlCommand(sql, con);
                    com.Parameters.Add("@pre_approval_visa_letter_id", SqlDbType.Int);
                    com.Parameters["@pre_approval_visa_letter_id"].Value = visa_letter_id;
                    com.Parameters.Add("@visa_type", SqlDbType.NVarChar);
                    com.Parameters["@visa_type"].Value = visa_type;
                    com.Parameters.Add("@visa_period", SqlDbType.NVarChar);
                    com.Parameters["@visa_period"].Value = visa_period;
                    com.Parameters.Add("@apply_receive", SqlDbType.NVarChar);
                    com.Parameters["@apply_receive"].Value = apply_receive;
                    com.ExecuteNonQuery();
                }
                return true;
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }
    }
}
