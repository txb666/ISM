﻿using ISM.WebApp.DAO;
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
        public bool CreateOrEdit(int days_before)
        {
            SqlConnection con = null;
            string sql = "begin tran if exists (select * from Config with (updlock,serializable) " +
                         "where [type] = 'passport') begin update Config set days_before = @days_before where " +
                         "[type] = 'passport' end else begin insert into Config([type],days_before) " +
                         "values ('passport',@days_before) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@days_before", SqlDbType.Int);
                com.Parameters["@days_before"].Value = days_before;
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

        public int createPassport(int student_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority)
        {
            throw new NotImplementedException();
        }

        public bool editPassport(int passport_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority)
        {
            SqlConnection con = null;
            string sql = "update Passports set passport_number=@passport_number,[start_date]=@start_date,expired_date=@expired_date,issuing_authority=@issuing_authority "
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
                com.Parameters.Add("@expired_date", SqlDbType.Date);
                com.Parameters["@expired_date"].Value = expired_date;
                com.Parameters.Add("@issuing_authority", SqlDbType.NVarChar);
                com.Parameters["@issuing_authority"].Value = issuing_authority;
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
                    passport.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    passport.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    passport.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    passport.start_date = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    passport.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
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

        public List<Passport> GetPassports(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string fullname, string account, string passport_number, DateTime? start_date, DateTime? expired_date, string issuing_authority)
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
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(a.passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                if (!string.IsNullOrEmpty(issuing_authority))
                {
                    where += " and upper(a.issuing_authority) like upper('%' + @issuing_authority + '%')";
                    com.Parameters.Add("@issuing_authority", SqlDbType.NVarChar);
                    com.Parameters["@issuing_authority"].Value = issuing_authority;
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
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by a.passport_id asc) rownumber, a.passport_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.expired_date,a.passport_number,a.issuing_authority"
                            + " from Passports a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where + ")"
                            + " as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by a.passport_id asc) rownumber, a.passport_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.expired_date,a.passport_number,a.issuing_authority"
                            + " from Passports a, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and c.student_group_id=e.studentGroup_id and d.[type]='Mobility' and e.staff_id=@current_staff_id" + where + ")"
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
                           + " (select ROW_NUMBER() over (order by a.passport_id asc) rownumber, a.passport_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.expired_date,a.passport_number,a.issuing_authority"
                           + " from Passports a, Users b, Student_Group c, Programs d"
                           + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where + ")"
                           + " as temp"
                           + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return passports;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Passport passport = new Passport();
                    passport.passport_id = (int)reader.GetValue(reader.GetOrdinal("passport_id"));
                    passport.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    passport.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    passport.passport_number = (string)reader.GetValue(reader.GetOrdinal("passport_number"));
                    if(!reader.IsDBNull(reader.GetOrdinal("picture")))
                    {
                        passport.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
                    passport.start_date = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    passport.expired_date = (DateTime)reader.GetValue(reader.GetOrdinal("expired_date"));
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

        public int GetTotalPassports(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string fullname, string account, string passport_number, DateTime? start_date, DateTime? expired_date, string issuing_authority)
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
                if (!string.IsNullOrEmpty(passport_number))
                {
                    where += " and upper(a.passport_number) like upper('%' + @passport_number + '%')";
                    com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
                    com.Parameters["@passport_number"].Value = passport_number;
                }
                if (!string.IsNullOrEmpty(issuing_authority))
                {
                    where += " and upper(a.issuing_authority) like upper('%' + @issuing_authority + '%')";
                    com.Parameters.Add("@issuing_authority", SqlDbType.NVarChar);
                    com.Parameters["@issuing_authority"].Value = issuing_authority;
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
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Passports a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Passports a, Users b, Student_Group c, Programs d, Coordinators e"
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
                            + " from Passports a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalPassports;
                    }
                }
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

        //public bool isPassportAlreadyExist(string passport_number)
        //{
        //    SqlConnection con = null;
        //    string sql = "select count(*) from Passports a.passport_number=@passport_number";
        //    SqlCommand com = null;
        //    bool isExist = true;
        //    int? count = null;
        //    try
        //    {
        //        con = DBUtils.GetConnection();
        //        con.Open();
        //        com = new SqlCommand(sql, con);
        //        com.Parameters.Add("@passport_number", SqlDbType.NVarChar);
        //        com.Parameters["@passport_number"].Value = passport_number;
        //        count = (int)com.ExecuteScalar();
        //        if (count == 0)
        //        {
        //            isExist = false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    finally
        //    {
        //        DBUtils.closeAllResource(con, com, null, null);
        //    }
        //    return isExist;
        //}
    }
}