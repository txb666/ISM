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
    public class MeetingDAOImpl : MeetingDAO
    {
        public bool CreateMAT(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            SqlConnection con = null;
            string sql = "insert into Meeting_AvailableTime(staff_id,[date],start_time,end_time) values(@staff_id,@date,@start_time,@end_time)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@start_time", SqlDbType.Time);
                com.Parameters["@start_time"].Value = start_time;
                com.Parameters.Add("@end_time", SqlDbType.Time);
                com.Parameters["@end_time"].Value = end_time;
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

        public bool DeleteMAT(int mat_id, int staff_id)
        {
            SqlConnection con = null;
            string sql = "delete from Meeting_AvailableTime where meeting_available_time_id = @meeting_available_time_id and staff_id = @staff_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@meeting_available_time_id", SqlDbType.Int);
                com.Parameters["@meeting_available_time_id"].Value = mat_id;
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
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

        public bool EditMAT(int mat_id, int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            SqlConnection con = null;
            string sql = "update Meeting_AvailableTime set [date] = @date, start_time = @start_time, end_time = @end_time " +
                         "where meeting_available_time_id = @meeting_available_time_id and staff_id = @staff_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@meeting_available_time_id", SqlDbType.Int);
                com.Parameters["@meeting_available_time_id"].Value = mat_id;
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@start_time", SqlDbType.Time);
                com.Parameters["@start_time"].Value = start_time;
                com.Parameters.Add("@end_time", SqlDbType.Time);
                com.Parameters["@end_time"].Value = end_time;
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

        public List<AvailableTime> GetAvailableTimes(int page, int pageSize, int staff_id, DateTime? date, TimeSpan? start_time, TimeSpan? end_time)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<AvailableTime> availableTimes = new List<AvailableTime>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (date != null)
                {
                    where += " and [date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (start_time != null)
                {
                    where += " and start_time=@start_time";
                    com.Parameters.Add("@start_time", SqlDbType.Time);
                    com.Parameters["@start_time"].Value = start_time;
                }
                if (end_time != null)
                {
                    where += " and end_time=@end_time";
                    com.Parameters.Add("@end_time", SqlDbType.Time);
                    com.Parameters["@end_time"].Value = end_time;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = "select * from (select ROW_NUMBER() over (order by a.[date] desc) rownumber,a.meeting_available_time_id,a.staff_id,b.fullname,b.email,a.[date]," +
                      "a.start_time,a.end_time from Meeting_AvailableTime a, Users b where a.staff_id = b.[user_id] and a.staff_id = @staff_id"+ where +") " +
                      "as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    AvailableTime availableTime = new AvailableTime();
                    availableTime.mat_id = (int)reader.GetValue(reader.GetOrdinal("meeting_available_time_id"));
                    availableTime.staff_id = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    availableTime.staff_name = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    availableTime.staff_email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    availableTime.date = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    availableTime.start_time = (TimeSpan)reader.GetValue(reader.GetOrdinal("start_time"));
                    availableTime.end_time = (TimeSpan)reader.GetValue(reader.GetOrdinal("end_time"));
                    availableTimes.Add(availableTime);
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
            return availableTimes;
        }

        public int GetTotalAvailableTime(int staff_id, DateTime? date, TimeSpan? start_time, TimeSpan? end_time)
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
                if (date != null)
                {
                    where += " and [date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (start_time != null)
                {
                    where += " and start_time=@start_time";
                    com.Parameters.Add("@start_time", SqlDbType.Time);
                    com.Parameters["@start_time"].Value = start_time;
                }
                if (end_time != null)
                {
                    where += " and end_time=@end_time";
                    com.Parameters.Add("@end_time", SqlDbType.Time);
                    com.Parameters["@end_time"].Value = end_time;
                }
                sql = "select count(*) from (select a.meeting_available_time_id,a.staff_id,b.fullname,b.email,a.[date]," +
                      "a.start_time,a.end_time from Meeting_AvailableTime a, Users b where a.staff_id = b.[user_id] and " +
                      "a.staff_id = @staff_id"+ where +") as temp";
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
