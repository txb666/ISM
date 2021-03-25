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
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdatePassport")))
                    {
                        notification.isUpdatePassport = (bool)reader.GetValue(reader.GetOrdinal("isUpdatePassport"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdateVisa")))
                    {
                        notification.isUpdateVisa = (bool)reader.GetValue(reader.GetOrdinal("isUpdateVisa"));
                    }
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
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdatePassport")))
                    {
                        notification.isUpdatePassport = (bool)reader.GetValue(reader.GetOrdinal("isUpdatePassport"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdateVisa")))
                    {
                        notification.isUpdateVisa = (bool)reader.GetValue(reader.GetOrdinal("isUpdateVisa"));
                    }
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

        private List<InsuranceFlightNotification> GetAllStudentWith(string type)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<InsuranceFlightNotification> notifications = new List<InsuranceFlightNotification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                if (type.Equals("AllStudentWithout"))
                {
                    sql = "select a.duration_start,c.isUpdateFlight,c.isUpdateInsurance,c.[user_id],c.email,c.fullname,a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id";
                }
                if (type.Equals("AllStudentWithInsurance"))
                {
                    sql = "select a.duration_start,c.isUpdateFlight,c.isUpdateInsurance,c.[user_id],c.email,c.fullname,a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, Insurances d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                if (type.Equals("AllStudentWithFlight"))
                {
                    sql = "select a.duration_start,c.isUpdateFlight,c.isUpdateInsurance,c.[user_id],c.email,c.fullname,a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, Flights d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    InsuranceFlightNotification insuranceFlightNotification = new InsuranceFlightNotification();
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdateFlight")))
                    {
                        insuranceFlightNotification.isUpdateFlight = (bool)reader.GetValue(reader.GetOrdinal("isUpdateFlight"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdateInsurance")))
                    {
                        insuranceFlightNotification.isUpdateInsurance = (bool)reader.GetValue(reader.GetOrdinal("isUpdateInsurance"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("duration_start")))
                    {
                        insuranceFlightNotification.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    }
                    insuranceFlightNotification.student_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    insuranceFlightNotification.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    insuranceFlightNotification.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    insuranceFlightNotification.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    insuranceFlightNotification.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    insuranceFlightNotification.program_type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    notifications.Add(insuranceFlightNotification);
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

        private List<NotificationConfig> GetNotificationConfigs()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<NotificationConfig> notificationConfigs = new List<NotificationConfig>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "SELECT [config_id],[type],[days_before],[hours_before],[kind],[deadline] FROM [ISM].[dbo].[Config]";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    NotificationConfig config = new NotificationConfig();
                    config.config_id = (int)reader.GetValue(reader.GetOrdinal("config_id"));
                    config.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    if (!reader.IsDBNull(reader.GetOrdinal("days_before")))
                    {
                        config.days_before = (int)reader.GetValue(reader.GetOrdinal("days_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("hours_before")))
                    {
                        config.hours_before = (int)reader.GetValue(reader.GetOrdinal("hours_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("kind")))
                    {
                        config.kind = (string)reader.GetValue(reader.GetOrdinal("kind"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("deadline")))
                    {
                        config.deadline = (DateTime)reader.GetValue(reader.GetOrdinal("deadline"));
                    }
                    notificationConfigs.Add(config);
                }
                return notificationConfigs;
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
            List<InsuranceFlightNotification> allStudentWithout = GetAllStudentWith("AllStudentWithout");
            List<InsuranceFlightNotification> allStudentWithInsurance = GetAllStudentWith("AllStudentWithInsurance");
            List<InsuranceFlightNotification> allStudentWithFlight = GetAllStudentWith("AllStudentWithFlight");
            List<NotificationConfig> notificationConfigs = GetNotificationConfigs();
            try
            {
                #region Degree
                foreach (var item in degree_notifications)
                {
                    if (item.type.Equals("passport") && !item.isUpdatePassport == true)
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
                    if (item.type.Equals("visa") && !item.isUpdateVisa == true)
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
                //Insurance
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentInsurance in allStudentWithInsurance)
                    {
                        if (item.student_id != studentInsurance.student_id || item.isUpdateInsurance != true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals("insurance") && config.kind.Equals("degree"))
                                {
                                    var totalDays = config.deadline.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string subject = "Insurance Notification";
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your insurance document, " +
                                            "deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        helper.SendMail(item.email, subject, body);
                                    }
                                }
                            }
                        }
                    }
                }
                //Flight
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentFlight in allStudentWithFlight)
                    {
                        if (item.student_id != studentFlight.student_id || item.isUpdateFlight != true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals("flight") && config.kind.Equals("degree"))
                                {
                                    var totalDays = config.deadline.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string subject = "Flight Notification";
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your flight ticket document, " +
                                            "deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        helper.SendMail(item.email, subject, body);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Mobility
                foreach (var item in mobility_notifications)
                {
                    if (item.type.Equals("passport"))
                    {
                        if (item.type.Equals("passport") && !item.isUpdatePassport == true)
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
                        if (item.type.Equals("visa") && !item.isUpdateVisa == true)
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
                //Insurance
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentInsurance in allStudentWithInsurance)
                    {
                        if (item.student_id != studentInsurance.student_id || item.isUpdateInsurance != true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals("insurance") && config.kind.Equals("mobility"))
                                {
                                    var totalDays = item.duration_start.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string subject = "Insurance Notification";
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your insurance document, " +
                                            "deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        helper.SendMail(item.email, subject, body);
                                    }
                                }
                            }
                        }
                    }
                }
                //Flight
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentFlight in allStudentWithFlight)
                    {
                        if (item.student_id != studentFlight.student_id || item.isUpdateFlight != true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals("flight") && config.kind.Equals("mobility"))
                                {
                                    var totalDays = item.duration_start.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string subject = "Flight Notification";
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your flight ticket document, " +
                                            "deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        helper.SendMail(item.email, subject, body);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }
    }
}
