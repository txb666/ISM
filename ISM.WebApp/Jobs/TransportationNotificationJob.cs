using ISM.WebApp.Helper;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.Constant;
using Microsoft.Data.SqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Jobs
{
    public class TransportationNotificationJob : IJob
    {
        private List<Notification> GetNotifications()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> notifications = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.[user_id],a.fullname,a.email,b.student_group_id,c.[type],c.hours_before,d.[date] " +
                      "as Bus_Date,d.[time] as Bus_Time from Users a, Student_Group b, Notification_Configuration " +
                      "c, Transportations d, Roles e where a.role_id = e.role_id and a.studentGroup_id = " +
                      "b.student_group_id and b.student_group_id = c.studentGroup_id and d.studentGroup_id = " +
                      "c.studentGroup_id and c.[type] = 'Transportation' and e.role_name = 'Mobility'";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification notification = new Notification();
                    notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    notification.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    notification.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("type")))
                    {
                        notification.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("hours_before")))
                    {
                        notification.hours_before = (int)reader.GetValue(reader.GetOrdinal("hours_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Bus_Date")))
                    {
                        notification.bus_date = (DateTime)reader.GetValue(reader.GetOrdinal("Bus_Date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Bus_Time")))
                    {
                        notification.bus_time = (TimeSpan)reader.GetValue(reader.GetOrdinal("Bus_Time"));
                    }
                    notifications.Add(notification);
                }
                return notifications;
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

        private void Insert(int id, DateTime bus_date, TimeSpan bus_time)
        {
            SqlConnection con = null;
            String sql = "begin tran if exists (select * from TransportationCheck with (updlock,serializable) " +
                         "where [user_id] = @user_id) begin update TransportationCheck set bus_date = @bus_date," +
                         "bus_time = @bus_time where [user_id] = @user_id end else begin insert into " +
                         "TransportationCheck([user_id],bus_date,bus_time,isSend) values (@user_id,@bus_date,@bus_time,1) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = id;
                com.Parameters.Add("@bus_date", SqlDbType.DateTime);
                com.Parameters["@bus_date"].Value = bus_date;
                com.Parameters.Add("@bus_time", SqlDbType.Time);
                com.Parameters["@bus_time"].Value = bus_time;
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
        }

        private List<Notification> GetChecks()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> notifications = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "SELECT [user_id],[bus_date],[bus_time],[isSend] FROM [ISM].[dbo].[TransportationCheck]";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification notification = new Notification();
                    if (!reader.IsDBNull(reader.GetOrdinal("user_id")))
                    {
                        notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("bus_date")))
                    {
                        notification.bus_date = (DateTime)reader.GetValue(reader.GetOrdinal("bus_date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("bus_time")))
                    {
                        notification.bus_time = (TimeSpan)reader.GetValue(reader.GetOrdinal("bus_time"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("isSend")))
                    {
                        notification.isSend = (bool)reader.GetValue(reader.GetOrdinal("isSend"));
                    }
                    notifications.Add(notification);
                }
                return notifications;
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

        private void InsertNotificationInformation(int user_id, string title, string content)
        {
            SqlConnection con = null;
            string sql = "insert into Notification_Information([user_id],title,content) values(@user_id,@title,@content)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                com.Parameters.Add("@title", SqlDbType.NVarChar);
                com.Parameters["@title"].Value = title;
                com.Parameters.Add("@content", SqlDbType.Text);
                com.Parameters["@content"].Value = content;
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now;
            DayOfWeek day = now.DayOfWeek;
            var notificationList = GetNotifications();
            var checkList = GetChecks();
            EmailHelper helper = new EmailHelper();
            try
            {
                foreach (var notification in notificationList)
                {
                    if (checkList.Count != 0)
                    {
                        foreach (var check in checkList)
                        {
                            if ((notification.user_id == check.user_id && check.isSend != true)
                                || (notification.user_id == check.user_id && ((notification.bus_date.Date != check.bus_date.Date)
                                                                                && (notification.bus_date.Month != check.bus_date.Month)
                                                                                && (notification.bus_date.Year != check.bus_date.Year)))
                                || (notification.user_id == check.user_id && ((notification.bus_date.Date == check.bus_date.Date)
                                                                                && (notification.bus_date.Month == check.bus_date.Month)
                                                                                && (notification.bus_date.Year == check.bus_date.Year))
                                                                          && ((notification.bus_time.Hours != check.bus_time.Hours)
                                                                                && notification.bus_time.Minutes != check.bus_time.Minutes))
                               )
                            {
                                if ((notification.bus_date.Date == now.Date)
                                    && (notification.bus_date.Month == now.Month)
                                    && (notification.bus_date.Year == now.Year))
                                {
                                    var totalhour = notification.bus_time.Hours - now.Hour;
                                    if (totalhour <= notification.hours_before && totalhour > 0)
                                    {
                                        string body = "Hello " + notification.fullname + ",\n\nToday " + now.ToString("yyyy-MMM-dd") + "" +
                                                      ", the departure time of the bus is " + notification.bus_time.ToString(@"hh\:mm\:ss") + "," +
                                                      " you have " + totalhour.ToString() + " hours left to prepare.";
                                        string student_notification = "Departure time of bus is " + notification.bus_time.ToString(@"hh\:mm\:ss") + ", you have " + totalhour.ToString() + " hours left to prepare." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                        helper.SendMail(notification.email, notificationConst.SUBJECT_TRANSPORTATION, body);
                                        InsertNotificationInformation(notification.user_id, notificationConst.SUBJECT_TRANSPORTATION, student_notification);
                                        Insert(notification.user_id, notification.bus_date, notification.bus_time);
                                    }
                                }
                            }
                            else if (notification.user_id != check.user_id)
                            {
                                if ((notification.bus_date.Date == now.Date)
                                    && (notification.bus_date.Month == now.Month)
                                    && (notification.bus_date.Year == now.Year))
                                {
                                    var totalhour = notification.bus_time.Hours - now.Hour;
                                    if (totalhour <= notification.hours_before && totalhour > 0)
                                    {
                                        string body = "Hello " + notification.fullname + ",\n\nToday " + now.ToString("yyyy-MMM-dd") + "" +
                                                      ", the departure time of the bus is " + notification.bus_time.ToString(@"hh\:mm\:ss") + "," +
                                                      " you have " + totalhour.ToString() + " hours left to prepare.";
                                        string student_notification = "Departure time of bus is " + notification.bus_time.ToString(@"hh\:mm\:ss") + ", you have " + totalhour.ToString() + " hours left to prepare." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                        helper.SendMail(notification.email, notificationConst.SUBJECT_TRANSPORTATION, body);
                                        InsertNotificationInformation(notification.user_id, notificationConst.SUBJECT_TRANSPORTATION, student_notification);
                                        Insert(notification.user_id, notification.bus_date, notification.bus_time);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((notification.bus_date.Date == now.Date)
                            && (notification.bus_date.Month == now.Month)
                            && (notification.bus_date.Year == now.Year))
                        {
                            var totalhour = notification.bus_time.Hours - now.Hour;
                            if (totalhour <= notification.hours_before && totalhour > 0)
                            {
                                string body = "Hello " + notification.fullname + ",\n\nToday " + now.ToString("yyyy-MMM-dd") + "" +
                                              ", the departure time of the bus is " + notification.bus_time.ToString(@"hh\:mm\:ss") + "," +
                                              " you have " + totalhour.ToString() + " hours left to prepare.";
                                string student_notification = "Departure time of bus is " + notification.bus_time.ToString(@"hh\:mm\:ss") + ", you have " + totalhour.ToString() + " hours left to prepare." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(notification.email, notificationConst.SUBJECT_TRANSPORTATION, body);
                                InsertNotificationInformation(notification.user_id, notificationConst.SUBJECT_TRANSPORTATION, student_notification);
                                Insert(notification.user_id, notification.bus_date, notification.bus_time);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }
    }
}
