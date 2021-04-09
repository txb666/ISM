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
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
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

        public List<MeetingSchedule> GetMeetingSchedule(int staff_id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<MeetingSchedule> meetingRegisters = new List<MeetingSchedule>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                sql = "select c.meeting_schedule_id,c.staff_id,temp.fullname as Staff_name,c.student_id,d.fullname as Student_name," +
                      "d.account,d.email,c.[date],c.start_time,c.end_time,c.IsAccepted,c.[note] from Meeting_Schedule c, Users d," +
                      "(select a.meeting_schedule_id,a.staff_id,b.fullname from Meeting_Schedule a, Users b where a.staff_id = b.[user_id]) " +
                      "as temp where c.student_id = d.[user_id] and c.meeting_schedule_id = temp.meeting_schedule_id and c.staff_id = @staff_id " +
                      "and c.IsAccepted = 1";
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    MeetingSchedule meetingSchedule = new MeetingSchedule();
                    meetingSchedule.ms_id_s = (int)reader.GetValue(reader.GetOrdinal("meeting_schedule_id"));
                    meetingSchedule.staff_id_s = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    meetingSchedule.staff_name_s = (string)reader.GetValue(reader.GetOrdinal("Staff_name"));
                    meetingSchedule.student_id_s = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    meetingSchedule.student_account_s = (string)reader.GetValue(reader.GetOrdinal("account"));
                    meetingSchedule.student_name_s = (string)reader.GetValue(reader.GetOrdinal("Student_name"));
                    meetingSchedule.student_email_s = (string)reader.GetValue(reader.GetOrdinal("email"));
                    meetingSchedule.note_s = (string)reader.GetValue(reader.GetOrdinal("note"));
                    meetingSchedule.date_MS_s = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    meetingSchedule.startTime_MS_s = (TimeSpan)reader.GetValue(reader.GetOrdinal("start_time"));
                    meetingSchedule.endTime_MS_s = (TimeSpan)reader.GetValue(reader.GetOrdinal("end_time"));
                    meetingSchedule.isAccepted_s = (bool)reader.GetValue(reader.GetOrdinal("IsAccepted"));
                    meetingRegisters.Add(meetingSchedule);
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
            return meetingRegisters;
        }

        public List<MeetingSchedule> GetMeetingRegister(int staff_id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<MeetingSchedule> meetingSchedules = new List<MeetingSchedule>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                sql = "select * from (select a.meeting_schedule_id,a.staff_id," +
                      "a.student_id,b.account,b.fullname,b.email,a.[note],a.[date],a.start_time,a.end_time,a.IsAccepted " +
                      "from Meeting_Schedule a, Users b where a.student_id = b.[user_id] and a.IsAccepted = 0 and " +
                      "a.staff_id = @staff_id) as temp";
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    MeetingSchedule meetingSchedule = new MeetingSchedule();
                    meetingSchedule.ms_id_r = (int)reader.GetValue(reader.GetOrdinal("meeting_schedule_id"));
                    meetingSchedule.staff_id_r = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    meetingSchedule.student_id_r = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    meetingSchedule.student_account_r = (string)reader.GetValue(reader.GetOrdinal("account"));
                    meetingSchedule.student_name_r = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    meetingSchedule.student_email_r = (string)reader.GetValue(reader.GetOrdinal("email"));
                    meetingSchedule.note_r = (string)reader.GetValue(reader.GetOrdinal("note"));
                    meetingSchedule.date_MS_r = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    meetingSchedule.startTime_MS_r = (TimeSpan)reader.GetValue(reader.GetOrdinal("start_time"));
                    meetingSchedule.endTime_MS_r = (TimeSpan)reader.GetValue(reader.GetOrdinal("end_time"));
                    meetingSchedule.isAccepted_r = (bool)reader.GetValue(reader.GetOrdinal("IsAccepted"));
                    meetingSchedules.Add(meetingSchedule);
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
            return meetingSchedules;
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
                com.Parameters.Add("@staff_id", SqlDbType.Int);
                com.Parameters["@staff_id"].Value = staff_id;
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

        public bool isSameTime(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time)
        {
            SqlConnection con = null;
            string sql = "select count(*) from Meeting_AvailableTime a where a.staff_id = @staff_id and " +
                         "a.[date] = @date and a.start_time = @start_time and a.end_time = @end_time";
            SqlDataReader reader = null;
            SqlCommand com = null;
            bool isExist = true;
            int? count = null;
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

        public bool AcceptMeetingRegister(int ms_id)
        {
            SqlConnection con = null;
            string sql = "update Meeting_Schedule set IsAccepted = 1 where meeting_schedule_id = @meeting_schedule_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@meeting_schedule_id", SqlDbType.Int);
                com.Parameters["@meeting_schedule_id"].Value = ms_id;
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

        public bool SetupNotification(int days_before)
        {
            SqlConnection con = null;
            string sql = "begin tran if exists (select * from Config with (updlock,serializable) " +
                         "where [type] = 'meeting_schedule') begin update Config set days_before = @days_before where " +
                         "[type] = 'meeting_schedule' end else begin insert into Config([type],days_before) " +
                         "values ('meeting_schedule',@days_before) end commit tran";
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
    }
}
