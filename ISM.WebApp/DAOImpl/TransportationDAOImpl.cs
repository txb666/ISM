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
    public class TransportationDAOImpl : TransportationDAO
    {
        public bool createTransportation(int studentGroup_id, DateTime date, TimeSpan time, string bus, string driver, string itinerary, string supporter, string note)
        {
            SqlConnection con = null;
            string sql = " insert into Transportations(studentGroup_id,[date],[time],bus,driver,itinerary,supporter,note)"
                       + " values (@studentGroup_id, @date, @time, @bus, @driver, @itinerary, @supporter, @note)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time", SqlDbType.Time);
                com.Parameters["@time"].Value = time;
                com.Parameters.Add("@bus", SqlDbType.NVarChar);
                com.Parameters["@bus"].Value = bus;
                com.Parameters.Add("@driver", SqlDbType.NVarChar);
                com.Parameters["@driver"].Value = driver;
                com.Parameters.Add("@itinerary", SqlDbType.NVarChar);
                com.Parameters["@itinerary"].Value = itinerary;
                com.Parameters.Add("@supporter", SqlDbType.NVarChar);
                com.Parameters["@supporter"].Value = supporter;
                com.Parameters.Add("@note", SqlDbType.NVarChar);
                com.Parameters["@note"].Value = note;
                if (string.IsNullOrEmpty(note))
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
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

        public bool DeleteTransportation(int transportation_id)
        {
            SqlConnection con = null;
            string sql = " delete from Transportations where transportations_id=@transportations_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);               
                com.Parameters.Add("@transportations_id", SqlDbType.Int);
                com.Parameters["@transportations_id"].Value = transportation_id;
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

        public bool editTransportation(int transportations_id, DateTime date, TimeSpan time, string bus, string driver, string itinerary, string supporter, string note)
        {
            SqlConnection con = null;
            string sql = " update Transportations set [date]=@date,[time]=@time,bus=@bus,driver=@driver,itinerary=@itinerary,supporter=@supporter,note=@note"
                       + " where transportations_id=@transportations_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time", SqlDbType.Time);
                com.Parameters["@time"].Value = time;
                com.Parameters.Add("@bus", SqlDbType.NVarChar);
                com.Parameters["@bus"].Value = bus;
                com.Parameters.Add("@driver", SqlDbType.NVarChar);
                com.Parameters["@driver"].Value = driver;
                com.Parameters.Add("@itinerary", SqlDbType.NVarChar);
                com.Parameters["@itinerary"].Value = itinerary;
                com.Parameters.Add("@supporter", SqlDbType.NVarChar);
                com.Parameters["@supporter"].Value = supporter;
                com.Parameters.Add("@note", SqlDbType.NVarChar);
                com.Parameters["@note"].Value = note;
                if (string.IsNullOrEmpty(note))
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
                com.Parameters.Add("@transportations_id", SqlDbType.Int);
                com.Parameters["@transportations_id"].Value = transportations_id;
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

        public int getTotalTransportation(int studentGroup_id, DateTime? date, string bus, string driver, string itinerary, string supporter)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalTransportation = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = " ";
                if (date != null)
                {
                    where += " and a.[date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (!string.IsNullOrEmpty(bus))
                {
                    where += " and upper(a.bus) like upper('%' + @bus + '%')";
                    com.Parameters.Add("@bus", SqlDbType.NVarChar);
                    com.Parameters["@bus"].Value = bus;
                }
                if (!string.IsNullOrEmpty(driver))
                {
                    where += " and upper(a.driver) like upper('%' + @driver + '%')";
                    com.Parameters.Add("@driver", SqlDbType.NVarChar);
                    com.Parameters["@driver"].Value = driver;
                }
                if (!string.IsNullOrEmpty(itinerary))
                {
                    where += " and upper(a.itinerary) like upper('%' + @itinerary + '%')";
                    com.Parameters.Add("@itinerary", SqlDbType.NVarChar);
                    com.Parameters["@itinerary"].Value = itinerary;
                }
                if (!string.IsNullOrEmpty(supporter))
                {
                    where += " and upper(a.supporter) like upper('%' + @supporter + '%')";
                    com.Parameters.Add("@supporter", SqlDbType.NVarChar);
                    com.Parameters["@supporter"].Value = supporter;
                }
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                sql = " select count(*) from Transportations a where a.[date]>=CAST(getdate() as date) and a.studentGroup_id=@studentGroup_id" + where;
                com.CommandText = sql;
                totalTransportation = (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalTransportation;
        }

        public List<Transportation> GetTransportations(int studentGroup_id, int page, int pageSize, DateTime? date, string bus, string driver, string itinerary, string supporter)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Transportation> transportations = new List<Transportation>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = " ";
                if (date != null)
                {
                    where += " and a.[date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (!string.IsNullOrEmpty(bus))
                {
                    where += " and upper(a.bus) like upper('%' + @bus + '%')";
                    com.Parameters.Add("@bus", SqlDbType.NVarChar);
                    com.Parameters["@bus"].Value = bus;
                }
                if (!string.IsNullOrEmpty(driver))
                {
                    where += " and upper(a.driver) like upper('%' + @driver + '%')";
                    com.Parameters.Add("@driver", SqlDbType.NVarChar);
                    com.Parameters["@driver"].Value = driver;
                }
                if (!string.IsNullOrEmpty(itinerary))
                {
                    where += " and upper(a.itinerary) like upper('%' + @itinerary + '%')";
                    com.Parameters.Add("@itinerary", SqlDbType.NVarChar);
                    com.Parameters["@itinerary"].Value = itinerary;
                }
                if (!string.IsNullOrEmpty(supporter))
                {
                    where += " and upper(a.supporter) like upper('%' + @supporter + '%')";
                    com.Parameters.Add("@supporter", SqlDbType.NVarChar);
                    com.Parameters["@supporter"].Value = supporter;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                sql = " select * from("
                    + " select ROW_NUMBER() over (order by a.transportations_id asc) rownumber,a.transportations_id,a.studentGroup_id,a.[date],a.[time],a.bus,a.driver,a.itinerary,a.supporter,a.note"
                    + " from Transportations a where a.[date]>=CAST(getdate() as date) and a.studentGroup_id=@studentGroup_id" + where + ") as temp"
                    + " where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Transportation transportation = new Transportation();
                    transportation.transportations_id = (int)reader.GetValue(reader.GetOrdinal("transportations_id"));
                    transportation.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                    transportation.date = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    transportation.time = (TimeSpan)reader.GetValue(reader.GetOrdinal("time"));
                    transportation.bus = (string)reader.GetValue(reader.GetOrdinal("bus"));
                    transportation.driver = (string)reader.GetValue(reader.GetOrdinal("driver"));
                    transportation.itinerary = (string)reader.GetValue(reader.GetOrdinal("itinerary"));
                    transportation.supporter = (string)reader.GetValue(reader.GetOrdinal("supporter"));
                    if (!reader.IsDBNull(reader.GetOrdinal("note")))
                    {
                        transportation.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                    }
                    transportations.Add(transportation);
                }

            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return transportations;
        }

        public bool setupNotification(int hours_before)
        {
            SqlConnection con = null;
            string sql = "begin tran if exists (select * from Config with (updlock,serializable) " +
                         "where [type] = 'Transportation') begin update Config set hours_before = @hours_before where " +
                         "[type] = 'Transportation' end else begin insert into Config([type],hours_before) " +
                         "values ('Transportation',@hours_before) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@hours_before", SqlDbType.Int);
                com.Parameters["@hours_before"].Value = hours_before;
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
