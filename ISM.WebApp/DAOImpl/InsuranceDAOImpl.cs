using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ISM.WebApp.DAOImpl
{
    public class InsuranceDAOImpl : InsuranceDAO
    {
        public bool editInsurance(int insurance_id, DateTime startDate, DateTime expiryDate)
        {
            SqlConnection con = null;
            string sql = "update Insurances set [start_date]=@startDate,[expiry_date]=@expiryDate where insurance_id=@insurance_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@startDate", SqlDbType.Date);
                com.Parameters["@startDate"].Value = startDate;
                com.Parameters.Add("@expiryDate", SqlDbType.Date);
                com.Parameters["@expiryDate"].Value = expiryDate;
                com.Parameters.Add("@insurance_id", SqlDbType.Int);
                com.Parameters["@insurance_id"].Value = insurance_id;
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

        public List<Insurance> getInsurance(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string account, string fullname, DateTime? startDate, DateTime? expiryDate)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Insurance> insurances = new List<Insurance>();
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
                if (startDate != null)
                {
                    where += " and a.[start_date]=@startDate";
                    com.Parameters.Add("@startDate", SqlDbType.Date);
                    com.Parameters["@startDate"].Value = startDate;
                }          
                if (expiryDate != null)
                {
                    where += " and a.[expiry_date]=@expiryDate";
                    com.Parameters.Add("@expiryDate", SqlDbType.Date);
                    com.Parameters["@expiryDate"].Value = expiryDate;
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
                            + " (select ROW_NUMBER() over (order by insurance_id asc) rownumber, a.insurance_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.[expiry_date]"
                            + " from Insurances a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where + ")"
                            + " as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by insurance_id asc) rownumber, a.insurance_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.[expiry_date]"
                            + " from Insurances a, Users b, Student_Group c, Programs d, Coordinators e"
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
                            + " (select ROW_NUMBER() over (order by insurance_id asc) rownumber, a.insurance_id,a.student_id,b.fullname,b.account,a.picture,a.[start_date],a.[expiry_date]"
                            + " from Insurances a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where + ")"
                            + " as temp"
                            + " where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return insurances;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Insurance insurance = new Insurance();
                    insurance.insurance_id = (int)reader.GetValue(reader.GetOrdinal("insurance_id"));
                    insurance.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname"))) {
                        insurance.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    insurance.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    if (!reader.IsDBNull(reader.GetOrdinal("picture"))){
                        insurance.picture = (string)reader.GetValue(reader.GetOrdinal("picture"));
                    }
                    insurance.start_date = (DateTime)reader.GetValue(reader.GetOrdinal("start_date"));
                    insurance.expiry_date = (DateTime)reader.GetValue(reader.GetOrdinal("expiry_date"));
                    insurances.Add(insurance);
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
            return insurances;
        }
      
        public int getTotalInsurance(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string account, string fullname, DateTime? startDate, DateTime? expiryDate)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalInsurance = 0;
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
                if (startDate != null)
                {
                    where += " and a.[start_date]=@startDate";
                    com.Parameters.Add("@startDate", SqlDbType.Date);
                    com.Parameters["@startDate"].Value = startDate;
                }
                if (expiryDate != null)
                {
                    where += " and a.[expiry_date]=@expiryDate";
                    com.Parameters.Add("@expiryDate", SqlDbType.Date);
                    com.Parameters["@expiryDate"].Value = expiryDate;
                }
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Insurances a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'"+where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Insurances a, Users b, Student_Group c, Programs d, Coordinators e"
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
                            + " from Insurances a, Users b, Student_Group c, Programs d"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalInsurance;
                    }
                }
                com.CommandText = sql;
                totalInsurance = (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalInsurance;
        }
    }
}
