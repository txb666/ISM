using ISM.WebApp.Constant;
using ISM.WebApp.Helper;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Jobs
{
    public class EmailNotificationJob : IJob
    {
        private List<Notification> GetNotificationsDegree()
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
                sql = NotificationConst.GET_ALL_NOTIFICATION_DEGREE;
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
                    if (!reader.IsDBNull(reader.GetOrdinal("days_before")))
                    {
                        notification.days_before = (int)reader.GetValue(reader.GetOrdinal("days_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Passport_Expired")))
                    {
                        notification.passport_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Passport_Expired"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Visa_Expired")))
                    {
                        notification.visa_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Visa_Expired"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("deadline")))
                    {
                        notification.deadline = (DateTime)reader.GetValue(reader.GetOrdinal("deadline"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("ORT_Date")))
                    {
                        notification.ort_date = (DateTime)reader.GetValue(reader.GetOrdinal("ORT_Date"));
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

        public List<Notification> GetNotificationsMobility()
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
                sql = NotificationConst.GET_ALL_NOTIFICATION_MOBILITY;
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
                    if (!reader.IsDBNull(reader.GetOrdinal("days_before")))
                    {
                        notification.days_before = (int)reader.GetValue(reader.GetOrdinal("days_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("hours_before")))
                    {
                        notification.hours_before = (int)reader.GetValue(reader.GetOrdinal("hours_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Passport_Expired")))
                    {
                        notification.passport_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Passport_Expired"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Visa_Expired")))
                    {
                        notification.visa_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Visa_Expired"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("duration_start")))
                    {
                        notification.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Detail_Agenda_Date")))
                    {
                        notification.detail_agenda_date = (DateTime)reader.GetValue(reader.GetOrdinal("Detail_Agenda_Date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Bus_Time")))
                    {
                        notification.bus_time = (TimeSpan)reader.GetValue(reader.GetOrdinal("Bus_Time"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Bus_Date")))
                    {
                        notification.bus_date = (DateTime)reader.GetValue(reader.GetOrdinal("Bus_Date"));
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

        public Task Execute(IJobExecutionContext context)
        {
            EmailHelper helper = new EmailHelper();
            var now = DateTime.Now;
            List<Notification> degree_notifications = GetNotificationsDegree();
            List<Notification> mobility_notifications = GetNotificationsMobility();
            try
            {
                //Degree Notification
                foreach (var item in degree_notifications)
                {
                    if (item.type.Equals("passport"))
                    {
                        var expiredDate = item.passport_expired;
                        var daysBefore = item.days_before;
                        var totalDays = expiredDate.Subtract(now).Days;
                        if (totalDays <= daysBefore && totalDays >= 0)
                        {
                            string subject = "Passport Notification";
                            string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                          "expires on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                          "" + totalDays.ToString() + " days left before it expires. Please renew!";
                            helper.SendMail(item.email, subject, body);
                        }
                    }
                    if (item.type.Equals("visa"))
                    {
                        var expiredDate = item.visa_expired;
                        var daysBefore = item.days_before;
                        var totalDays = expiredDate.Subtract(now).Days;
                        if (totalDays <= daysBefore && totalDays >= 0)
                        {
                            string subject = "Visa Notification";
                            string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                          "expires on " + item.visa_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                          "" + totalDays.ToString() + " days left before it expires. Please renew!";
                            helper.SendMail(item.email, subject, body);
                        }
                    }
                    if (item.type.Equals("insurance"))
                    {
                        var deadline = item.deadline;
                        var daysBefore = item.days_before;
                        var totalDays = deadline.Subtract(now).Days;
                        if (totalDays <= daysBefore && totalDays >= 0)
                        {
                            string subject = "Insurance Notification";
                            string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                          "dealine on " + item.deadline.ToString("yyyy-MMM-dd") + ", you have " +
                                          "" + totalDays.ToString() + " days left before dealine.";
                            helper.SendMail(item.email, subject, body);
                        }
                    }
                    if (item.type.Equals("flight"))
                    {
                        var deadline = item.deadline;
                        var daysBefore = item.days_before;
                        var totalDays = deadline.Subtract(now).Days;
                        if (totalDays <= daysBefore && totalDays >= 0)
                        {
                            string subject = "Flight Notification";
                            string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                          "dealine on " + item.deadline.ToString("yyyy-MMM-dd") + ", you have " +
                                          "" + totalDays.ToString() + " days left before dealine.";
                            helper.SendMail(item.email, subject, body);
                        }
                    }
                    if (item.type.Equals("ort_schedule"))
                    {
                        var ort_date = item.ort_date;
                        var daysBefore = item.days_before;
                        var totalDays = ort_date.Subtract(now).Days;
                        if (totalDays <= daysBefore && totalDays >= 0)
                        {
                            string subject = "Orientation Notification";
                            string body = "Hello " + item.fullname + ",\n\nYour orientation " +
                                          "dealine on " + item.ort_date.ToString("yyyy-MMM-dd") + ", you have " +
                                          "" + totalDays.ToString() + " days left before dealine.";
                            helper.SendMail(item.email, subject, body);
                        }
                    }
                }
                //Mobility Notification
                foreach (var item in mobility_notifications)
                {
                    if (item.type.Equals("passport"))
                    {
                        if (item.type.Equals("passport"))
                        {
                            var expiredDate = item.passport_expired;
                            var daysBefore = item.days_before;
                            var totalDays = expiredDate.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string subject = "Passport Notification";
                                string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                              "expires on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before it expires. Please renew!";
                                helper.SendMail(item.email, subject, body);
                            }
                        }
                        if (item.type.Equals("visa"))
                        {
                            var expiredDate = item.visa_expired;
                            var daysBefore = item.days_before;
                            var totalDays = expiredDate.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string subject = "Visa Notification";
                                string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                              "expires on " + item.visa_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before it expires. Please renew!";
                                helper.SendMail(item.email, subject, body);
                            }
                        }
                        if (item.type.Equals("insurance"))
                        {
                            var duration_start = item.duration_start;
                            var daysBefore = item.days_before;
                            var totalDays = duration_start.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string subject = "Insurance Notification";
                                string body = "Hello " + item.fullname + ",\n\nYour program " +
                                              "start on " + item.deadline.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left to submit your insurance information.";
                                helper.SendMail(item.email, subject, body);
                            }
                        }
                        if (item.type.Equals("flight"))
                        {
                            var duration_start = item.duration_start;
                            var daysBefore = item.days_before;
                            var totalDays = duration_start.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string subject = "Flight Notification";
                                string body = "Hello " + item.fullname + ",\n\nYour program " +
                                              "start on " + item.deadline.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left to submit flight ticket information.";
                                helper.SendMail(item.email, subject, body);
                            }
                        }
                        if (item.type.Equals("Detail_Agenda"))
                        {
                            var date = item.detail_agenda_date;
                            var daysBefore = item.days_before;
                            var totalDays = date.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string subject = "Detail Agenda Notification";
                                string body = "Hello " + item.fullname + ",\n\nThere are " +
                                              "an agenda on " + item.deadline.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left to submit flight ticket information.";
                                helper.SendMail(item.email, subject, body);
                            }
                        }
                        if (item.type.Equals("Transportation"))
                        {
                            var time_now = DateTime.Now.Hour;
                            var bus_date = item.bus_date;
                            var bus_time = item.bus_time;
                            var hoursBefore = item.hours_before;
                            if (DateTime.Compare(bus_date, now) == 0)
                            {
                                var totalHours = bus_time.Hours - time_now;
                                if (totalHours <= hoursBefore && totalHours > 0)
                                {
                                    string subject = "Transportation Notification";
                                    string body = "Hello " + item.fullname + ",\n\nToday " + now.ToString("yyyy-MMM-dd") + "" +
                                                  ", the departure time of the bus is " + item.bus_time.ToString("hh:mm tt") + "," +
                                                  " you have " + totalHours.ToString() + " hours left to prepare.";
                                    helper.SendMail(item.email, subject, body);
                                }
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
